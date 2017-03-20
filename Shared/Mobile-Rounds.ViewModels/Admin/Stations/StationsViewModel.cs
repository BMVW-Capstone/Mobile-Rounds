using System.Threading.Tasks;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System.Collections.Generic;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;
using System.Collections.ObjectModel;
using System.Linq;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared;
using System;
using Mobile_Rounds.ViewModels.Admin.Regions;
using Mobile_Rounds.ViewModels.Admin.Items;

namespace Mobile_Rounds.ViewModels.Admin.Stations
{
    public class StationsViewModel : Shared.BaseViewModel
    {
        public ObservableCollection<StationViewModel> Stations { get; set; }
        public ObservableCollection<RegionViewModel> Regions { get; set; }

        public AsyncCommand Save { get; private set; }
        public AsyncCommand Cancel { get; private set; }

        public AsyncCommand NavigateToItems { get; private set; }

        /// <summary>
        /// Gets or sets the currently selected region in the list.
        /// If set to null, it clears out the selection.
        /// </summary>
        public StationViewModel Selected
        {
            get
            {
                return this.selected;
            }

            set
            {
                this.selected = value;
                if (this.selected != null)
                {
                    this.CurrentStation = this.selected;
                }

                this.NavigateToItems.RaiseExecuteChanged();
                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        /// <summary>
        /// Gets or sets the region that is currently being modified or added. Used
        /// for data binding to the input fields.
        /// </summary>
        public StationViewModel CurrentStation
        {
            get
            {
                return this.currentStation;
            }

            set
            {
                if (this.currentStation == null && value != null)
                {
                    this.currentStation = value;
                }

                this.currentStation.Id = value.Id;
                this.currentStation.Name = value.Name;
                this.currentStation.IsDeleted = value.IsDeleted;
                this.currentStation.RegionId = value.RegionId;

                if (this.currentStation.Id == Guid.Empty)
                {
                    this.currentStation.SetModificationType(ModificationType.Create);
                }
                else
                {
                    this.currentStation.SetModificationType(ModificationType.Update);
                }

                this.RaisePropertyChanged(nameof(this.CurrentStation));
            }
        }


        public StationsViewModel()
        {
            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));
            this.Crumbs.Add(new BreadcrumbItemModel("Stations"));

            this.Stations = new ObservableCollection<StationViewModel>();
            this.Regions = new ObservableCollection<RegionViewModel>();
            this.selected = new StationViewModel(this.Save, this.Cancel);

            this.Save = new AsyncCommand(async (obj) =>
            {
                if (this.currentStation.Id == Guid.Empty)
                {
                    //new record
                    await SaveNew();
                    return;
                }
                else
                {
                    await SaveExisting();
                }
            }, this.CanSave);

            this.Cancel = new AsyncCommand(this.CancelStation, this.CanCancel);
            this.NavigateToItems = new AsyncCommand((obj) =>
            {
                var region = this.Regions.FirstOrDefault(r => r.Id == this.currentStation.RegionId);

                var vm = new ItemScreenViewModel(region, this.CurrentStation);
                Navigator.Navigate(Shared.Navigation.NavigationType.AdminItems, vm);
            }, this.CanNavigateToItems);

            this.currentStation = new StationViewModel(this.Save, this.Cancel);
        }

        protected override async Task FetchDataAsync()
        {
            var stations = await base.Api.GetAsync<List<StationModel>>(
                $"{Constants.Endpoints.Stations}?{Constants.ApiOptions.IncludeDeleted}");

            var regions = await base.Api.GetAsync<List<RegionModel>>(
                $"{Constants.Endpoints.Regions}?{Constants.ApiOptions.IncludeDeleted}");

            var castedStations = stations.Select(s => new StationViewModel(s, Save, Cancel));
            var castedRegions = regions.Select(r => new RegionViewModel(r, Save, Cancel));

            Stations.AddRange(castedStations);
            Regions.AddRange(castedRegions);

            var selectedStationId = Navigator.GetNavigationData<Guid>();
            if(selectedStationId != null && selectedStationId != Guid.Empty)
            {
                this.Selected = Stations.FirstOrDefault(s => s.Id == selectedStationId);
            }
        }

        private void CancelStation(object data)
        {
            this.currentStation.Model = new StationModel();
            this.Selected = null;
        }

        private async Task<bool> SaveNew()
        {
            var model = await base.Api.PostAsync<StationModel>(
                Constants.Endpoints.Stations, this.currentStation.Model);
            if(model == null)
            {
                return false;
            }

            this.currentStation.Model = model;
            return true;
        }

        private async Task<bool> SaveExisting()
        {
            var existing = Stations.FirstOrDefault(s => s.Id == this.currentStation.Id);
            if(existing != null)
            {
                var model = await base.Api.PutAsync<StationModel>(
                    Constants.Endpoints.Stations, this.currentStation.Model);

                if (model == null)
                {
                    return false;
                }

                existing.Model = model;
            }

            return true;
        }

        private bool CanSave(object data)
        {
            var basicCheck = !string.IsNullOrEmpty(currentStation.Name)
                && currentStation.RegionId != Guid.Empty;
            if (!basicCheck) return false;

            foreach (var station in Stations)
            {
                if (this.currentStation.Id == station.Id) continue;

                //cannot have more than one station in the same region with the same name.
                if (station.Name.Equals(this.currentStation.Name, StringComparison.CurrentCultureIgnoreCase)
                    && station.RegionId == this.currentStation.RegionId)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanCancel(object data)
        {
            return !string.IsNullOrEmpty(currentStation.Name)
                || currentStation.RegionId != Guid.Empty;
        }

        private bool CanNavigateToItems(object data)
        {
            return this.selected?.Id != null
                && this.selected?.Id != Guid.Empty;
        }

        private StationViewModel selected;
        private StationViewModel currentStation;
    }
}

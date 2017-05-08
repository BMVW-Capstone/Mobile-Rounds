using Mobile_Rounds.ViewModels.Admin.Regions;
using Mobile_Rounds.ViewModels.Admin.Stations;
using Mobile_Rounds.ViewModels.Admin.UnitOfMeasure;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Admin.Items
{
    public sealed class ItemScreenViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the list of items that are displayed to the user.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the list of items that are displayed to the user.
        /// </summary>
        public ObservableCollection<UnitOfMeasureModel> Units { get; set; }

        /// <summary>
        /// Gets the save method to call when the users taps save.
        /// </summary>
        public AsyncCommand Save { get; private set; }

        /// <summary>
        /// Gets or sets the currently selected unit in the list.
        /// If set to null, it clears out the selection.
        /// </summary>
        public ItemViewModel Selected
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
                    this.CurrentItem = this.selected;
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        /// <summary>
        /// Gets or sets the unit of measurement that is currently being modified or added. Used
        /// for data binding to the input fields.
        /// </summary>
        public ItemViewModel CurrentItem
        {
            get
            {
                return this.currentItem;
            }

            set
            {
                if (this.currentItem == null && value != null)
                {
                    this.currentItem = value;
                }

                this.currentItem.Id = value.Id;
                this.currentItem.LowerBound = value.LowerBound;
                this.currentItem.UpperBound = value.UpperBound;
                this.currentItem.Name = value.Name;
                this.currentItem.IsDeleted = value.IsDeleted;
                this.currentItem.ComparisonType = value.ComparisonType;
                this.currentItem.Units = value.Units;
                this.currentItem.Unit = value.Unit;
                this.currentItem.Meter = value.Meter;
                this.currentItem.Model = value.Model;

                if (this.currentItem.Id == Guid.Empty)
                {
                    this.currentItem.SetModificationType(ModificationType.Create);
                }
                else
                {
                    this.currentItem.SetModificationType(ModificationType.Update);
                }

                this.RaisePropertyChanged(nameof(this.CurrentItem));
            }
        }

        /// <summary>
        /// Gets the command to call when the user taps the cancel button.
        /// </summary>
        public AsyncCommand Cancel { get; private set; }

        public StationModel BelongsTo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasureScreenViewModel"/> class.
        /// This also sets up all commands and data objects in the view.
        /// </summary>
        public ItemScreenViewModel(RegionViewModel region, StationViewModel station)
        {
            this.Units = new ObservableCollection<UnitOfMeasureModel>();
            this.Items = new ObservableCollection<ItemViewModel>();

            this.Cancel = new AsyncCommand(
                (obj) =>
                {
                    this.Selected = null;
                    this.CurrentItem = new ItemViewModel(this.Save, this.Cancel, this.Units);
                }, this.CanCancel);

            this.Save = new AsyncCommand(
                async (obj) =>
                {
                    var model = new ItemModel
                    {
                        Id = this.currentItem.Id,
                        Name = this.currentItem.Name,
                        IsDeleted = this.currentItem.IsDeleted,
                        Meter = this.currentItem.Meter,
                        StationId = this.BelongsTo.Id,
                        Specification = new SpecificationModel
                        {
                            ComparisonType = this.currentItem.ComparisonType.Name,
                            LowerBound = this.currentItem.LowerBound,
                            UpperBound = this.currentItem.UpperBound,
                            UnitOfMeasure = this.currentItem.Unit
                        }
                    };

                    var existing = this.Items.FirstOrDefault(u => u.Id == this.currentItem.Id);
                    if (existing == null)
                    {
                        model = await base.Api.PostAsync<ItemModel>(
                            $"{Constants.Endpoints.Items}", model);
                        if(model == null)
                        {
                            return;
                        }

                        var newCopy = new ItemViewModel(model, Save, Cancel, Units);
                        this.Items.Add(newCopy);
                    }
                    else
                    {
                        model = await base.Api.PutAsync<ItemModel>(
                            $"{Constants.Endpoints.Items}", model);

                        if(model == null)
                        {
                            return;
                        }

                        existing.Name = model.Name;
                        existing.ComparisonType = ComparisonTypeViewModel.Locate(model.Specification.ComparisonType);
                        existing.IsDeleted = model.IsDeleted;
                        existing.UpperBound = model.Specification.UpperBound;
                        existing.LowerBound = model.Specification.LowerBound;
                        existing.Unit = model.Specification.UnitOfMeasure;
                        existing.Meter = model.Meter;
                        existing.SetModificationType(ModificationType.Update);
                    }

                    this.CurrentItem = new ItemViewModel(this.Save, this.Cancel, this.Units);
                    this.Selected = null;
                }, this.ValidateInput);

            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));

            this.CurrentItem = new ItemViewModel(this.Save, this.Cancel, this.Units);

            this.BelongsTo = station.Model;

            this.SetupRegionBreadcrumb(region);
            this.SetupStationBreadcrumb(station);
        }

        protected override async Task FetchDataAsync()
        {
            //get items using /api/stations/{stationid}/items
            var itemsData = await base.Api.GetAsync<List<ItemModel>>(
                $"{Constants.Endpoints.Stations}/{this.BelongsTo.Id}/items?{Constants.ApiOptions.IncludeDeleted}");

            //get units of measure
            var unitsData = await base.Api.GetAsync<List<UnitOfMeasureModel>>(
                $"{Constants.Endpoints.Units}?{Constants.ApiOptions.IncludeDeleted}");

            this.Units.AddRange(unitsData);

            var castedItems = itemsData.Select(i => new ItemViewModel(i, Save, Cancel, Units));
            this.Items.AddRange(castedItems);
        }

        private ItemViewModel currentItem;
        private ItemViewModel selected;
        
        private void SetupRegionBreadcrumb(RegionViewModel region)
        {
            var navigateToAdminRegion = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(Shared.Navigation.NavigationType.Regions, region?.Id);
            });

            this.Crumbs.Add(new BreadcrumbItemModel(region?.Name, navigateToAdminRegion));
        }

        private void SetupStationBreadcrumb(StationViewModel station)
        {
            var navigateToAdminStation = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(Shared.Navigation.NavigationType.AdminStations, station.Model.Id);
            });
            this.Crumbs.Add(new BreadcrumbItemModel(station.Name, navigateToAdminStation));
        }

        private bool ValidateInput(object input)
        {
            if (string.IsNullOrEmpty(this.CurrentItem?.Name)) return false;
            if (string.IsNullOrEmpty(this.CurrentItem?.Meter)) return false;
            if (string.IsNullOrEmpty(this.CurrentItem?.UpperBound)) return false;
            if (this.CurrentItem?.Unit == null) return false;

            if (this.CurrentItem?.ComparisonType == null) return false;

            if(this.CurrentItem.ComparisonType.UsesTwoInputs)
            {
                try
                {
                    return this.CurrentItem
                        .ComparisonType.ValidateBoundOrder(
                            this.CurrentItem.LowerBound, this.CurrentItem.UpperBound);
                }
                catch
                {
                    //error with validation, so not valid.
                    return false;
                }
            }

            return true;
        }

        private bool CanCancel(object input)
        {
            if (!string.IsNullOrEmpty(this.CurrentItem?.Name)) return true;
            if (!string.IsNullOrEmpty(this.CurrentItem?.Meter)) return true;
            if (!string.IsNullOrEmpty(this.CurrentItem?.LowerBound)) return true;
            if (!string.IsNullOrEmpty(this.CurrentItem?.UpperBound)) return true;
            if (this.CurrentItem?.Unit != null) return true;
            if (this.CurrentItem.ComparisonType != null) return true;

            return false;
        }

    }
}

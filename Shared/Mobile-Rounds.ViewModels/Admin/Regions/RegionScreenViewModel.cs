using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Admin.Regions
{
    public class RegionScreenViewModel : BaseViewModel
    {
        /// Gets or sets the list of regions that are displayed to the user.
        /// </summary>
        public ObservableCollection<RegionViewModel> Regions { get; set; }

        /// <summary>
        /// Gets the save method to call when the users taps save.
        /// </summary>
        public AsyncCommand Save { get; private set; }

        /// <summary>
        /// Gets or sets the currently selected region in the list.
        /// If set to null, it clears out the selection.
        /// </summary>
        public RegionViewModel Selected
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
                    this.CurrentRegion = this.selected;
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        /// <summary>
        /// Gets or sets the region that is currently being modified or added. Used
        /// for data binding to the input fields.
        /// </summary>
        public RegionViewModel CurrentRegion
        {
            get
            {
                return this.currentRegion;
            }

            set
            {
                if (this.currentRegion == null && value != null)
                {
                    this.currentRegion = value;
                }

                this.currentRegion.Id = value.Id;
                this.currentRegion.Name = value.Name;

                if (this.currentRegion.Id == Guid.Empty)
                {
                    this.currentRegion.SetModificationType(ModificationType.Create);
                }
                else
                {
                    this.currentRegion.SetModificationType(ModificationType.Update);
                }

                this.RaisePropertyChanged(nameof(this.CurrentRegion));
            }
        }

        /// <summary>
        /// Gets the command to call when the user taps the cancel button.
        /// </summary>
        public AsyncCommand Cancel { get; private set; }

        protected override async Task FetchDataAsync()
        {
            var regions = await base.Api.GetAsync<List<RegionModel>>(
                "http://localhost:1797/api/regions");

            var casted = regions.Select(r => new RegionViewModel(r, Save, Cancel));
            this.Regions.AddRange(casted);
        }

        public RegionScreenViewModel()
        {
            this.Cancel = new AsyncCommand(
                (obj) =>
                {
                    this.Selected = null;
                    this.CurrentRegion = new RegionViewModel(this.Save, this.Cancel);
                }, this.CanCancel);

            this.Save = new AsyncCommand(
                (obj) =>
                {
                    var existing = this.Regions.FirstOrDefault(u => u.Id == this.currentRegion.Id);
                    if (existing == null)
                    {
                        this.CurrentRegion.Id = Guid.NewGuid();
                        var newCopy = new RegionViewModel(this.CurrentRegion);
                        this.Regions.Add(newCopy);
                    }
                    else
                    {
                        existing.Name = this.currentRegion.Name;
                        existing.ModificationType = this.currentRegion.ModificationType;
                    }

                    this.CurrentRegion = new RegionViewModel(this.Save, this.Cancel);
                    this.Selected = null;
                }, this.ValidateInput);

            this.currentRegion = new RegionViewModel(this.Save, this.Cancel);
            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));
            this.Crumbs.Add(new BreadcrumbItemModel("Regions"));

            this.Regions = new ObservableCollection<RegionViewModel>();
        }

        private RegionViewModel currentRegion;
        private RegionViewModel selected;

        private bool ValidateInput(object input)
        {
            return !string.IsNullOrEmpty(this.currentRegion.Name);
        }

        private bool CanCancel(object input)
        {
            return !string.IsNullOrEmpty(this.currentRegion.Name);
        }
    }
}

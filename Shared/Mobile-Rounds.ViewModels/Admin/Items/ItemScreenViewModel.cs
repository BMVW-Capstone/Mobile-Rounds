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
        public IEnumerable<UnitOfMeasureModel> Units { get; set; }

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
                return this.currentUnit;
            }

            set
            {
                if (this.currentUnit == null && value != null)
                {
                    this.currentUnit = value;
                }

                this.currentUnit.Id = value.Id;
                this.currentUnit.LowerBound = value.LowerBound;
                this.currentUnit.UpperBound = value.UpperBound;
                this.currentUnit.Name = value.Name;
                this.currentUnit.IsDeleted = value.IsDeleted;
                this.currentUnit.ComparisonType = value.ComparisonType;
                this.currentUnit.ComparisonTypes = value.ComparisonTypes;
                this.currentUnit.Units = value.Units;
                this.currentUnit.Unit = value.Unit;

                if (this.currentUnit.Id == Guid.Empty)
                {
                    this.currentUnit.SetModificationType(ModificationType.Create);
                }
                else
                {
                    this.currentUnit.SetModificationType(ModificationType.Update);
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
        public ItemScreenViewModel(RegionModel region, StationModel station, IEnumerable<UnitOfMeasureModel> unitsOfMeasure, IEnumerable<ItemModel> items)
        {
            this.Cancel = new AsyncCommand(
                (obj) =>
                {
                    this.Selected = null;
                    this.CurrentItem = new ItemViewModel(this.Save, this.Cancel);
                }, this.CanCancel);

            this.Save = new AsyncCommand(
                (obj) =>
                {
                    //TODO: Implement disk storage
                    var existing = this.Items.FirstOrDefault(u => u.Id == this.currentUnit.Id);
                    if (existing == null)
                    {
                        this.CurrentItem.Id = Guid.NewGuid();
                        var newCopy = new ItemViewModel(this.CurrentItem);
                        this.Items.Add(newCopy);
                    }
                    else
                    {
                        existing.Name = this.currentUnit.Name;
                        existing.ComparisonType = this.currentUnit.ComparisonType;
                        existing.IsDeleted = this.currentUnit.IsDeleted;
                        existing.UpperBound = this.currentUnit.UpperBound;
                        existing.LowerBound = this.currentUnit.LowerBound;
                        existing.ModificationType = this.currentUnit.ModificationType;
                    }

                    this.CurrentItem = new ItemViewModel(this.Save, this.Cancel);
                    this.Selected = null;
                }, this.ValidateInput);


            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));

            this.CurrentItem = new ItemViewModel(this.Save, this.Cancel);

            this.BelongsTo = station;

            this.Crumbs.Add(new BreadcrumbItemModel(region.Name));
            this.Crumbs.Add(new BreadcrumbItemModel(this.BelongsTo.Name));

            this.Items = new ObservableCollection<ItemViewModel>();
            this.Units = unitsOfMeasure;

            foreach (var item in items)
            {
                var vm = new ItemViewModel(this.Save, this.Cancel)
                {
                    Id = item.Id,
                    Name = item.Name,
                    ComparisonType = item.Specification.ComparisonType,
                    IsDeleted = item.IsDeleted,
                    LowerBound = item.Specification.LowerBound,
                    UpperBound = item.Specification.UpperBound,
                    Unit = item.Specification.UnitOfMeasure,
                    Units = this.Units
                };
                Items.Add(vm);
            }
        }

        private ItemViewModel currentUnit;
        private ItemViewModel selected;

        private bool ValidateInput(object input)
        {
            return true;
            //return !string.IsNullOrEmpty(this.currentUnit.Abbreviation)
            //    && !string.IsNullOrEmpty(this.currentUnit.FullName)
            //    && !string.IsNullOrEmpty(this.currentUnit.UnitType);
        }

        private bool CanCancel(object input)
        {
            return true;
            //return !string.IsNullOrEmpty(this.currentUnit.Abbreviation)
            //    || !string.IsNullOrEmpty(this.currentUnit.FullName)
            //    || !string.IsNullOrEmpty(this.currentUnit.UnitType);
        }

    }
}

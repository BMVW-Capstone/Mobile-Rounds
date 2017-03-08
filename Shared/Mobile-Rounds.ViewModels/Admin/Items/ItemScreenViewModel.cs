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
        public ItemScreenViewModel(RegionModel region, StationModel station, IEnumerable<UnitOfMeasureModel> unitsOfMeasure, IEnumerable<ItemModel> items)
        {
            this.Units = new ObservableCollection<UnitOfMeasureModel>(unitsOfMeasure);

            this.Cancel = new AsyncCommand(
                (obj) =>
                {
                    this.Selected = null;
                    this.CurrentItem = new ItemViewModel(this.Save, this.Cancel, this.Units);
                }, this.CanCancel);

            this.Save = new AsyncCommand(
                (obj) =>
                {
                    //TODO: Implement disk storage
                    var existing = this.Items.FirstOrDefault(u => u.Id == this.currentItem.Id);
                    if (existing == null)
                    {
                        this.CurrentItem.Id = Guid.NewGuid();
                        var newCopy = new ItemViewModel(this.CurrentItem);
                        this.Items.Add(newCopy);
                    }
                    else
                    {
                        existing.Name = this.currentItem.Name;
                        existing.ComparisonType = this.currentItem.ComparisonType;
                        existing.IsDeleted = this.currentItem.IsDeleted;
                        existing.UpperBound = this.currentItem.UpperBound;
                        existing.LowerBound = this.currentItem.LowerBound;
                        existing.ModificationType = this.currentItem.ModificationType;
                        existing.Unit = this.currentItem.Unit;
                        existing.Meter = this.currentItem.Meter;
                    }

                    this.CurrentItem = new ItemViewModel(this.Save, this.Cancel, this.Units);
                    this.Selected = null;
                }, this.ValidateInput);

            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));

            this.CurrentItem = new ItemViewModel(this.Save, this.Cancel, this.Units);

            this.BelongsTo = station;

            this.Crumbs.Add(new BreadcrumbItemModel(region.Name));
            this.Crumbs.Add(new BreadcrumbItemModel(this.BelongsTo.Name));

            this.Items = new ObservableCollection<ItemViewModel>();

            foreach (var item in items)
            {
                if (item.StationId != station.Id) continue;

                var vm = new ItemViewModel(this.Save, this.Cancel, this.Units)
                {
                    Id = item.Id,
                    Name = item.Name,
                    ComparisonType = ComparisonTypeViewModel.Locate(item.Specification.ComparisonType),
                    IsDeleted = item.IsDeleted,
                    LowerBound = item.Specification.LowerBound,
                    UpperBound = item.Specification.UpperBound,
                    Unit = item.Specification.UnitOfMeasure,
                    Meter = item.Meter,
                    Model = item
                };

                //find the current unit in our already loaded list. This is so the bindings will work appropriately.
                vm.Unit = this.Units.FirstOrDefault(u => u.Id == item.Specification.UnitOfMeasure.Id);
                Items.Add(vm);
            }
        }

        private ItemViewModel currentItem;
        private ItemViewModel selected;

        private bool ValidateInput(object input)
        {
            if (string.IsNullOrEmpty(this.CurrentItem?.Name)) return false;
            if (string.IsNullOrEmpty(this.CurrentItem?.Meter)) return false;
            if (string.IsNullOrEmpty(this.CurrentItem?.UpperBound)) return false;

            if (this.CurrentItem?.ComparisonType == null) return false;

            if(this.CurrentItem.ComparisonType.UsesTwoInputs)
            {
                return this.CurrentItem
                    .ComparisonType.ValidateBoundOrder(
                        this.CurrentItem.LowerBound, this.CurrentItem.UpperBound);
            }

            return true;
        }

        private bool CanCancel(object input)
        {
            if (!string.IsNullOrEmpty(this.CurrentItem?.Name)) return true;
            if (!string.IsNullOrEmpty(this.CurrentItem?.Meter)) return true;
            if (!string.IsNullOrEmpty(this.CurrentItem?.LowerBound)) return true;
            if (!string.IsNullOrEmpty(this.CurrentItem?.UpperBound)) return true;
            if (this.CurrentItem.ComparisonType != null) return true;

            return false;
        }

    }
}

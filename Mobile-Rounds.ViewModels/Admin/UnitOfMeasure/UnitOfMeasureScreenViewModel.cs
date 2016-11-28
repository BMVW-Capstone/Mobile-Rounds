// <copyright file="UnitOfMeasureScreenViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;

namespace Mobile_Rounds.ViewModels.Admin.UnitOfMeasure
{
    /// <summary>
    /// Represents the data that is displayed in the list as well as
    /// the input section.
    /// </summary>
    public class UnitOfMeasureScreenViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the list of units that are displayed to the user.
        /// </summary>
        public ObservableCollection<UnitOfMeasure> Units { get; set; }

        /// <summary>
        /// Gets the save method to call when the users taps save.
        /// </summary>
        public AsyncCommand Save { get; private set; }

        /// <summary>
        /// Gets or sets the currently selected unit in the list.
        /// If set to null, it clears out the selection.
        /// </summary>
        public UnitOfMeasure Selected
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
                    this.CurrentUnit = this.selected;
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        /// <summary>
        /// Gets or sets the unit of measurement that is currently being modified or added. Used
        /// for data binding to the input fields.
        /// </summary>
        public UnitOfMeasure CurrentUnit
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
                this.currentUnit.Abbreviation = value.Abbreviation;
                this.currentUnit.FullName = value.FullName;

                if (this.currentUnit.Id == Guid.Empty)
                {
                    this.currentUnit.SetModificationType(ModificationType.Create);
                }
                else
                {
                    this.currentUnit.SetModificationType(ModificationType.Update);
                }

                this.RaisePropertyChanged(nameof(this.CurrentUnit));
            }
        }

        /// <summary>
        /// Gets the command to call when the user taps the cancel button.
        /// </summary>
        public AsyncCommand Cancel { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasureScreenViewModel"/> class.
        /// This also sets up all commands and data objects in the view.
        /// </summary>
        public UnitOfMeasureScreenViewModel()
        {
            this.Cancel = new AsyncCommand(
                (obj) =>
                {
                    this.Selected = null;
                    this.CurrentUnit = new UnitOfMeasure(this.Save, this.Cancel);
                }, this.CanCancel);

            this.Save = new AsyncCommand(
                (obj) =>
                {
                    var existing = this.Units.FirstOrDefault(u => u.Id == this.currentUnit.Id);
                    if (existing == null)
                    {
                        this.CurrentUnit.Id = Guid.NewGuid();
                        var newCopy = new UnitOfMeasure(this.CurrentUnit);
                        this.Units.Add(newCopy);
                        MockUnits.Add(newCopy);
                    }
                    else
                    {
                        existing.FullName = this.currentUnit.FullName;
                        existing.Abbreviation = this.currentUnit.Abbreviation;
                        existing.ModificationType = this.currentUnit.ModificationType;
                    }

                    this.CurrentUnit = new UnitOfMeasure(this.Save, this.Cancel);
                    this.Selected = null;
                }, this.ValidateInput);

            // Init test data
            if (MockUnits.Count == 0)
            {
                MockUnits.Add(new UnitOfMeasure(this.Save, this.Cancel)
                {
                    Id = Guid.NewGuid(),
                    FullName = "Pounds per square inch",
                    Abbreviation = "PSI"
                });

                MockUnits.Add(new UnitOfMeasure(this.Save, this.Cancel)
                {
                    Id = Guid.NewGuid(),
                    FullName = "Celcius",
                    Abbreviation = "C"
                });

                MockUnits.Add(new UnitOfMeasure(this.Save, this.Cancel)
                {
                    Id = Guid.NewGuid(),
                    FullName = "Fahrenheit",
                    Abbreviation = "F"
                });

                MockUnits.Add(new UnitOfMeasure(this.Save, this.Cancel)
                {
                    Id = Guid.NewGuid(),
                    FullName = "Percent",
                    Abbreviation = "%"
                });
            }

            this.Units = new ObservableCollection<UnitOfMeasure>(MockUnits);

            this.CurrentUnit = new UnitOfMeasure(this.Save, this.Cancel);
            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));
            this.Crumbs.Add(new BreadcrumbItemModel("Unit of Measure"));
        }

        private UnitOfMeasure currentUnit;
        private UnitOfMeasure selected;

        private bool ValidateInput(object input)
        {
            return !string.IsNullOrEmpty(this.currentUnit.Abbreviation)
                && !string.IsNullOrEmpty(this.currentUnit.FullName);
        }

        private bool CanCancel(object input)
        {
            return !string.IsNullOrEmpty(this.currentUnit.Abbreviation)
                || !string.IsNullOrEmpty(this.currentUnit.FullName);
        }
    }
}

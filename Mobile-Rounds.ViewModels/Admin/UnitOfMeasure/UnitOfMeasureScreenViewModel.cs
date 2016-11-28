using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Admin.UnitOfMeasure
{
    public class UnitOfMeasureScreenViewModel : BaseViewModel
    {

        public ObservableCollection<UnitOfMeasure> Units { get; set; }

        public AsyncCommand Save { get; set; }

        private UnitOfMeasure currentUnit;
        private UnitOfMeasure selected;

        public UnitOfMeasure Selected
        {
            get
            {
                return selected;
            }
            
            set
            {
                selected = value;
                if(selected != null)
                {
                    CurrentUnit = selected;
                }
                this.RaisePropertyChanged(nameof(Selected));
            }
        }

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

        public AsyncCommand Cancel { get; private set; }

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
    }
}

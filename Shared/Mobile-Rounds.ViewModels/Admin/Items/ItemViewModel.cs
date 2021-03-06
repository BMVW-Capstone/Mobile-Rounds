﻿using Mobile_Rounds.ViewModels.Admin.UnitOfMeasure;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Admin.Items
{
    public class ItemViewModel : NotificationBase
    {
        /// <summary>
        /// Gets or sets the unique id of the object.
        /// </summary>
        public Guid Id { get; set; }

        public ItemModel Model { get; set; }

        /// <summary>
        /// Gets or sets if the model is deleted in the database 
        /// or not.
        /// </summary>
        public bool IsDeleted
        {
            get { return this.isDeleted; }
            set
            {
                this.isDeleted = value;
                this.RaisePropertyChanged(nameof(this.IsDeleted));
            }
        }

        /// <summary>
        /// Gets or sets the abbreviation of the unit.
        /// </summary>
        public string LowerBound
        {
            get
            {
                return this.lowerBound;
            }

            set
            {
                this.lowerBound = value;
                this.RaisePropertyChanged(nameof(this.LowerBound));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the abbreviation of the unit.
        /// </summary>
        public string UpperBound
        {
            get
            {
                return this.upperBound;
            }

            set
            {
                this.upperBound = value;
                this.RaisePropertyChanged(nameof(this.UpperBound));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the full name of the unit. This would be akin to Kelvin,
        /// when the abbreviation is K.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.RaisePropertyChanged(nameof(this.Name));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }


        /// <summary>
        /// The name of the meter. This is actually like a product number, gauge type, etc...
        /// </summary>
        public string Meter
        {
            get
            {
                return this.meter;
            }

            set
            {
                this.meter = value;
                this.RaisePropertyChanged(nameof(this.Meter));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        public ComparisonTypeViewModel ComparisonType
        {
            get
            {
                return this.comparisonType;
            }

            set
            {
                this.comparisonType = value;
                this.RaisePropertyChanged(nameof(this.ComparisonType));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        public ObservableCollection<UnitOfMeasureModel> Units { get; set; }
        public IEnumerable<ComparisonTypeViewModel> ComparisonTypes { get { return ComparisonTypeViewModel.AllTypesAsViewModels; } }

        public UnitOfMeasureModel Unit
        {
            get { return this.unit; }
            set
            {
                this.unit = value;
                this.RaisePropertyChanged(nameof(this.Unit));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of modification that is currently taking place
        /// on the unit.
        /// </summary>
        public string ModificationType
        {
            get
            {
                return this.modificationType;
            }

            set
            {
                this.modificationType = value;
                this.RaisePropertyChanged(nameof(this.ModificationType));
            }
        }

        /// <summary>
        /// Sets the modification type for the unit.
        /// </summary>
        /// <param name="type">The type of modification taking place.</param>
        public void SetModificationType(ModificationType type)
        {
            const string New = "Create";
            const string Update = "Update";
            const string Delete = "Delete";

            switch (type)
            {
                case Shared.ModificationType.Create:
                    this.ModificationType = New;
                    break;
                case Shared.ModificationType.Update:
                    this.ModificationType = Update;
                    break;
                case Shared.ModificationType.Delete:
                    this.ModificationType = Delete;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasureModel"/> class.
        /// This then caches the save and cancel commands so that it can
        /// notify any views based on them.
        /// </summary>
        /// <param name="save">The save command that is based on this unit.</param>
        /// <param name="cancel">The cancel command that is based on this unit.</param>
        public ItemViewModel(AsyncCommand save, AsyncCommand cancel, ObservableCollection<UnitOfMeasureModel> units)
        {
            //Value types for readings. Indicate what type of value a new unit will be.
            //Volts for example might be a Number, while a text based readout would be Text
            this.ModificationType = "Create";
            this.Units = units;
            this.comparisonType = null;
            this.unit = null;
            this.Save = save;
            this.Cancel = cancel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasureModel"/> class.
        /// This then caches the save and cancel commands so that it can
        /// notify any views based on them.
        /// </summary>
        /// <param name="model">The model data to copy.</param>
        /// <param name="save">The save command that is based on this unit.</param>
        /// <param name="cancel">The cancel command that is based on this unit.</param>
        public ItemViewModel(ItemModel model, AsyncCommand save, AsyncCommand cancel, ObservableCollection<UnitOfMeasureModel> units)
            : this(save, cancel, units)
        {
            this.Model = model;

            this.Id = model.Id;
            this.name = model.Name;
            this.lowerBound = model.Specification.LowerBound;
            this.upperBound = model.Specification.UpperBound;
            this.unit = units.FirstOrDefault(u => u.Id == model.Specification.UnitOfMeasure.Id);
            this.isDeleted = model.IsDeleted;
            this.meter = model.Meter;
            this.comparisonType = ComparisonTypeViewModel.Locate(model.Specification.ComparisonType);
            this.SetModificationType(Shared.ModificationType.Update);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasureModel"/> class.
        /// This is a copy constructor so all values will get copied to the new copy.
        /// </summary>
        /// <param name="toCopy">The object to copy.</param>
        public ItemViewModel(ItemViewModel toCopy)
        {
            this.Save = toCopy.Save;
            this.Cancel = toCopy.Cancel;

            this.Name = toCopy.Name;
            this.ComparisonType = toCopy.ComparisonType;
            this.UpperBound = toCopy.UpperBound;
            this.LowerBound = toCopy.LowerBound;
            this.Unit = toCopy.Unit;
            this.Meter = toCopy.Meter;
            this.IsDeleted = toCopy.IsDeleted;
            this.Units = toCopy.Units;
            this.Id = toCopy.Id;
            this.modificationType = toCopy.modificationType;
        }

        private string lowerBound;
        private string upperBound;
        private string meter;
        private string name;
        private ComparisonTypeViewModel comparisonType;
        private string modificationType;
        private UnitOfMeasureModel unit;
        private bool isDeleted;

        private AsyncCommand Save { get; set; }

        private AsyncCommand Cancel { get; set; }
    }
}

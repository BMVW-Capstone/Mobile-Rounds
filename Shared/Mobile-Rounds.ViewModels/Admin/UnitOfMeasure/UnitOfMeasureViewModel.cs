// <copyright file="UnitOfMeasure.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;

namespace Mobile_Rounds.ViewModels.Admin.UnitOfMeasure
{
    /// <summary>
    /// Represents a basic Unit of Measurement model.
    /// </summary>
    public class UnitOfMeasureViewModel : NotificationBase
    {
        /// <summary>
        /// Gets or sets the unique id of the object.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets if the model is deleted in the database 
        /// or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation of the unit.
        /// </summary>
        public string Abbreviation
        {
            get
            {
                return this.abbreviation;
            }

            set
            {
                this.abbreviation = value;
                this.RaisePropertyChanged(nameof(this.Abbreviation));
                this.Save.RaiseExecuteChanged();
                this.Cancel.RaiseExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the full name of the unit. This would be akin to Kelvin,
        /// when the abbreviation is K.
        /// </summary>
        public string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                this.fullName = value;
                this.RaisePropertyChanged(nameof(this.FullName));
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
            const string New = "Save New Unit";
            const string Update = "Update Unit";
            const string Delete = "Delete Unit";

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
        public UnitOfMeasureViewModel(AsyncCommand save, AsyncCommand cancel)
        {
            this.ModificationType = "Save New Unit";
            this.Save = save;
            this.Cancel = cancel;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="UnitOfMeasureModel"/> class.
        ///// This is a copy constructor so all values will get copied to the new copy.
        ///// </summary>
        ///// <param name="toCopy">The object to copy.</param>
        //public UnitOfMeasureViewModel(UnitOfMeasureModel toCopy)
        //{
        //    this.fullName = toCopy.FullName;
        //    this.abbreviation = toCopy.Abbreviation;
        //    this.Id = toCopy.Id;
        //    this.Save = toCopy.Save;
        //    this.Cancel = toCopy.Cancel;
        //    this.modificationType = toCopy.modificationType;
        //}

        private string abbreviation;
        private string fullName;
        private string modificationType;

        private AsyncCommand Save { get; set; }

        private AsyncCommand Cancel { get; set; }
    }
}

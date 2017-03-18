// <copyright file="RegionsViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System;

namespace Mobile_Rounds.ViewModels.Admin.Regions
{
    public class RegionViewModel : NotificationBase
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
        /// Gets or sets the name of the region. 
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
        /// Gets or sets the type of modification that is currently taking place
        /// on the region
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
        /// Sets the modification type for the region.
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
        /// Initializes a new instance of the <see cref="RegionModel"/> class.
        /// This then caches the save and cancel commands so that it can
        /// notify any views based on them.
        /// </summary>
        /// <param name="save">The save command that is based on this region.</param>
        /// <param name="cancel">The cancel command that is based on this region.</param>
        public RegionViewModel(AsyncCommand save, AsyncCommand cancel)
        {
            SetModificationType(Shared.ModificationType.Create);
            this.Save = save;
            this.Cancel = cancel;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RegionModel"/> class.
        /// This is a copy constructor so all values will get copied to the new copy.
        /// </summary>
        /// <param name="toCopy">The object to copy.</param>
        public RegionViewModel(RegionModel toCopy, AsyncCommand save, AsyncCommand cancel)
        {
            this.name = toCopy.Name;
            this.Id = toCopy.Id;
            this.IsDeleted = toCopy.IsDeleted;
            this.Save = save;
            this.Cancel = cancel;
            SetModificationType(Shared.ModificationType.Create);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionModel"/> class.
        /// This is a copy constructor so all values will get copied to the new copy.
        /// </summary>
        /// <param name="toCopy">The object to copy.</param>
        public RegionViewModel(RegionViewModel toCopy)
        {
            this.name = toCopy.name;
            this.Id = toCopy.Id;
            this.Save = toCopy.Save;
            this.Cancel = toCopy.Cancel;
            this.modificationType = toCopy.modificationType;
        }

        private string name;
        private string modificationType;

        private AsyncCommand Save { get; set; }

        private AsyncCommand Cancel { get; set; }
    }
}

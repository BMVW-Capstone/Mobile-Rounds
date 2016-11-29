// <copyright file="ReadingInputListViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Rounds.ViewModels.Shared;

namespace Mobile_Rounds.ViewModels.Regular.ReadingInput
{
    public class ReadingInputListViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the list of units that are displayed to the user.
        /// </summary>
        public ObservableCollection<Meter> Meters { get; set; }

        /// <summary>
        /// Gets or sets the currently selected unit in the list.
        /// If set to null, it clears out the selection.
        /// </summary>
        public Meter Selected
        {
            get
            {
                return this.selectedMeter;
            }

            set
            {
                this.selectedMeter = value;
                if (this.selectedMeter != null)
                {
                    this.CurrentReading = new ReadingInput();
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        /// <summary>
        /// Gets or sets the unit of measurement that is currently being modified or added. Used
        /// for data binding to the input fields.
        /// </summary>
        public ReadingInput CurrentReading
        {
            get
            {
                return this.currentInput;
            }

            set
            {
                if (this.currentInput == null && value != null)
                {
                    this.currentInput = value;
                }

                //this.currentInput.Id = value.Id;
                //this.currentInput.Abbreviation = value.Abbreviation;
                //this.currentInput.FullName = value.FullName;

                //if (this.currentInput.Id == Guid.Empty)
                //{
                //    this.currentInput.SetModificationType(ModificationType.Create);
                //}
                //else
                //{
                //    this.currentInput.SetModificationType(ModificationType.Update);
                //}

                this.RaisePropertyChanged(nameof(this.CurrentReading));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputListViewModel"/> class.
        /// </summary>
        public ReadingInputListViewModel()
        {
            if (MockMeters.Count == 0)
            {
                MockMeters.Add(new Meter(Guid.NewGuid(), "Supply Air Receiver Tank"));
                MockMeters.Add(new Meter(Guid.NewGuid(), "Nitrogen Backup Valve"));
                MockMeters.Add(new Meter(Guid.NewGuid(), "Nitrogen Backup Pressure"));
            }

            this.Meters = new ObservableCollection<Meter>(MockMeters);
        }

        private ReadingInput currentInput;
        private Meter selectedMeter;
    }
}

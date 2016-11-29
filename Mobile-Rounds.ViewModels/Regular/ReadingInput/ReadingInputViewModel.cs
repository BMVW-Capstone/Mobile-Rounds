// <copyright file="ReadingInputViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.ReadingType;

namespace Mobile_Rounds.ViewModels.Regular.ReadingInput
{
    public class ReadingInputViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets a value indicating whether if the input type is not.
        /// </summary>
        public bool IsBooleanInput
        {
            get
            {
                return this.data.ValueBounds == BoundType.EitherOr;
            }
        }

        /// <summary>
        /// Gets a value indicating whether if the input type is not boolean.
        /// </summary>
        public bool IsNotBooleanInput
        {
            get
            {
                return !this.IsBooleanInput;
            }
        }

        /// <summary>
        /// Gets or sets the boolean value from the screen.
        /// </summary>
        public bool BooleanValue
        {
            get
            {
                return this.data.BooleanValue;
            }

            set
            {
                this.data.BooleanValue = value;
                this.RaisePropertyChanged(nameof(this.BooleanValue));
            }
        }

        public ReadingInputViewModel()
        {
            this.data = new ReadingInput()
            {
                ValueBounds = BoundType.EitherOr
            };
        }

        private ReadingInput data;
    }
}

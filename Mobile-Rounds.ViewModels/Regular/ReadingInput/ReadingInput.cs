// <copyright file="ReadingInput.cs" company="SolarWorld Capstone Team">
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
    public class ReadingInput : NotificationBase
    {
        /// <summary>
        /// Gets or sets the type of binding that the value
        /// is constrained to.
        /// </summary>
        public BoundType ValueBounds { get; set; }

        /// <summary>
        /// Gets or sets the notes for the given reading.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets a value that is iterpreted as a boolean.
        /// </summary>
        public bool BooleanValue { get; set; }

        /// <summary>
        /// Gets or sets a value that is iterpreted as an integer.
        /// </summary>
        public int IntegerValue { get; set; }

        /// <summary>
        /// Gets or sets a value that is interpreted as a string.
        /// </summary>
        public string StringValue { get; set; }
    }
}

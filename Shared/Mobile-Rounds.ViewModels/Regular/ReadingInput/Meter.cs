// <copyright file="Meter.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using Mobile_Rounds.ViewModels.Shared;

namespace Mobile_Rounds.ViewModels.Regular.ReadingInput
{
    /// <summary>
    /// Represents a meter station.
    /// </summary>
    public class Meter : NotificationBase
    {
        /// <summary>
        /// Gets or sets the unique id of the meter.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the meter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        public Meter()
            : this(Guid.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        /// <param name="name">The name of the record.</param>
        public Meter(string name)
            : this(Guid.Empty, name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        /// <param name="id">The id of the record.</param>
        /// <param name="name">The name of the record.</param>
        public Meter(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets todays reading.
        /// </summary>
        public ReadingInput TodaysReading { get; set; }

        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput YesterdaysReading { get; set; }
    }
}

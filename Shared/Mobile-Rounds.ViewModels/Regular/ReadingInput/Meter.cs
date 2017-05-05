// <copyright file="Meter.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Admin.Items;

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
        /// Gets or sets the name of the item. This class name actually represents an item...
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the meter.
        /// </summary>
        public string MeterName { get; set; }

        /// <summary>
        /// True if entry is completed
        /// </summary>
        public bool IsComplete { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        public Meter()
            : this(Guid.Empty, string.Empty, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        /// <param name="name">The name of the record.</param>
        public Meter(string name, ItemModel item)
            : this(Guid.Empty, name, item)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        /// <param name="id">The id of the record.</param>
        /// <param name="name">The name of the record.</param>
        public Meter(Guid id, string name, ItemModel item)
        {
            this.Id = id;
            this.Name = name;
            this.Item = item;
        }

        /// <summary>
        /// Gets or sets todays reading.
        /// </summary>
        public ReadingInput TodaysReading { get; set; }

        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput LastReading { get; set; }

        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput TwoReadingsAgo { get; set; }


        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput ThreeReadingsAgo { get; set; }


        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput FourReadingsAgo { get; set; }

        public ItemModel Item { get; private set; }

        public ComparisonTypeViewModel ComparisonType { get; set; }
    }
}

// <copyright file="Round.cs" company="SolarWorld Capstone Team">
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

namespace Mobile_Rounds.ViewModels.Models
{
    /// <summary>
    /// Represents a basic Round model.
    /// </summary>
    public class RoundModel
    {
        public RoundModel()
        {
            RoundHour = -1;
        }

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
        /// This is the date + hour. No minute. This is because a <see cref="Region"/>
        /// should only have one round per hour block.
        /// </summary>
        public int RoundHour { get; set; }

        /// <summary>
        /// Foreign key to the <see cref="Region"/> for a given round.
        /// </summary>
        public Guid RegionId { get; set; }

        /// <summary>
        /// User who is assigned the round. Decided when a person starts the round.
        /// </summary>
        public string AssignedTo { get; set; }

        /// <summary>
        /// UTC based start time of the round.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// UTC based end time of the round.
        /// </summary>
        public DateTime EndTime { get; set; }

    }
}

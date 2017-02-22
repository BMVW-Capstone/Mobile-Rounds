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

namespace Mobile_Rounds.ViewModels.Models
{
    /// <summary>
    /// Represents a basic Unit of Measurement model.
    /// </summary>
    public class UnitOfMeasureModel
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
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the full name of the unit. This would be akin to Kelvin,
        /// when the abbreviation is K.
        /// </summary>
        public string Name { get; set; }
    }
}

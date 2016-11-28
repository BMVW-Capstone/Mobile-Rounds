// <copyright file="ModificationType.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Shared
{
    /// <summary>
    /// Defines a type of modfification that is happening on an object.
    /// </summary>
    public enum ModificationType
    {
        /// <summary>
        /// An object is being created.
        /// </summary>
        Create,

        /// <summary>
        /// And object is being updated.
        /// </summary>
        Update,

        /// <summary>
        /// An object is being deleted.
        /// </summary>
        Delete
    }
}

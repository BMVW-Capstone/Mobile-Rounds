// <copyright file="NavigationType.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Shared.Navigation
{
    /// <summary>
    /// Represents the different types of screens.
    /// </summary>
    public enum NavigationType
    {
        /// <summary>
        /// Navigate to the Admin home page.
        /// </summary>
        AdminHome,

        /// <summary>
        /// Navigate to the Home page.
        /// </summary>
        Home,

        /// <summary>
        /// Navigate to the unit of measure page.
        /// </summary>
        UnitOfMeasure,

        /// <summary>
        /// Navigate to stations admin page.
        /// </summary>
        AdminStations,

        /// <summary>
        /// Navigate to the station select screen for a round.
        /// </summary>
        StationSelect,

        /// <summary>
        /// Navigate to regions page.
        /// </summary>
        Regions,

        /// <summary>
        /// Navigate to region input page.
        /// </summary>
        RegionSelect,

        /// <summary>
        /// Navigate to start rounds page
        /// </summary>
        StartRound,

        /// <summary>
        /// Navigate to the station input page.
        /// </summary>
        StationInput
    }
}

// <copyright file="BoundType.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;

namespace Mobile_Rounds.ViewModels.Shared.ReadingType
{
    /// <summary>
    /// Represents a type of bound that is acceptable.
    /// </summary>
    public enum BoundType
    {
        /// <summary>
        /// Represents an equal to value binding.
        /// </summary>
        EqualTo,

        /// <summary>
        /// Represents a less than value binding.
        /// </summary>
        LessThan,

        /// <summary>
        /// Represents a greater than value binding.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Represents a either or value binding.
        /// </summary>
        Between,

        /// <summary>
        /// Represents a binary either or value binding.
        /// </summary>
        EitherOr
    }
}

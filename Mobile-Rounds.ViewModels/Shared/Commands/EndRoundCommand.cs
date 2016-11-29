// <copyright file="EndRoundCommand.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Shared.Commands
{
    /// <summary>
    /// Represents a way to trigger the ending of a round.
    /// </summary>
    public sealed class EndRoundCommand : ICommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

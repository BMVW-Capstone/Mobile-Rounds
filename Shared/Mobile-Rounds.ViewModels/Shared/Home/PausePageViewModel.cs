// <copyright file="PausePageViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Shared.Commands;

namespace Mobile_Rounds.ViewModels.Shared.Home
{
    /// <summary>
    /// Represents the bsaic data for the home screens as exposed to XAML.
    /// </summary>
    public class PausePageViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets the property used to handle the resuming of rounds.
        /// </summary>
        public ICommand ResumeRound { get; private set; }

        /// <summary>
        /// Gets the property used to handle the ending of rounds.
        /// </summary>
        public ICommand EndRound { get; private set; }

        /// <summary>
        /// Gets the property used to handle the syncing of data.
        /// </summary>
        public ICommand Sync { get; private set; }

        private bool CanResume(object obj)
        {
            return IsRoundLocked;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageViewModel"/> class.
        /// Creates and sets defaults for the view model.
        /// </summary>
        public PausePageViewModel()
        {
#if DEBUG
            // only make us admin if debugging.
            this.IsAdmin = true;
#endif
            this.Sync = new SyncCommand();
            this.ResumeRound = new AsyncCommand(null, CanResume);
            this.EndRound = new ResumeRoundCommand();

        }
    }
}

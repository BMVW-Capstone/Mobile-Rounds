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
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;

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

        private async Task<RoundModel> UploadRoundAsync()
        {
            // Check if we have a round to save or not.
            if (RoundManager.CurrentRound != null)
            {
                //Tell the server that we completed the round.
                return await RoundManager.CompleteRoundAsync();

                //TODO: Do something with the didComplete. Perhaps this happens before the other syncs
                //so we can skip overwriting the existing files since the round couldn't be sync'ed.
            }

            return null;
        }

        private async Task UploadReadingsAsync(Guid roundId)
        {
            foreach (var reading in ReadingManager.Readings)
            {
                //skip anything that was added, but not really entered.
                if (string.IsNullOrEmpty(reading.Value)) continue;

                //set the round id for all the readings.
                reading.RoundId = roundId;

                //for each reading that we have, upload it to the server.
                var readingResult = await this.Api.PostAsync<ReadingModel>(Constants.Endpoints.Readings, reading);

                //TODO: Figure out what to do if one of the uploads fails...
            }

            //all good, so remove the items.
            await ReadingManager.Reset();
        }

        /// <summary>
        /// Stops the current Round
        /// </summary>
        /// <param name="obj"></param>
        private async Task StopRound(object obj)
        {
            //If round is locked, end round without uploading
            if (IsRoundLocked)
            {
                await RoundManager.CancelRound();
            }
            else
            {
                //upload the current round. This needs to hapen first, so we can set all RoundId values on the readings.
                if (RoundManager.CurrentRound?.RegionId != null && RoundManager.CurrentRound?.RegionId != Guid.Empty)
                {
                    var round = await UploadRoundAsync();
                    if (round != null && round.Id != Guid.Empty)
                    {
                        //update the RoundId on all the readings PRIOR to sending to the server.
                        await UploadReadingsAsync(round.Id);
                    }
                } else
                {
                    await RoundManager.CancelRound();
                }
            }
            Navigator.Navigate(Navigation.NavigationType.Home);
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
            this.GoHome = null;
            this.Sync = new SyncCommand();
            if (IsRoundLocked)
            {
                this.ResumeRound = null;
            }
            else
            {
                this.ResumeRound = new StartRoundCommand();
            }
            this.EndRound = new AsyncCommand(async(obj) => await StopRound(obj));

        }
    }
}

using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Shared
{
    /// <summary>
    /// A singleton class for operating on the current <see cref="RoundModel"/>.
    /// </summary>
    public static class RoundManager
    {
        private static RoundModel Singleton { get; set; }

        static RoundManager()
        {
           FileHandler = ServiceResolver.Resolve<IFileHandler>();
        }

        /// <summary>
        /// Returns the current round.
        /// </summary>
        public static RoundModel CurrentRound { get { return Singleton; } }

        /// <summary>
        /// Loads the current round from the file system. If none exists, returns null.
        /// </summary>
        public static async Task<RoundModel> LoadCurrentRoundAsync()
        {
            try
            {
                var currentRound = await FileHandler.GetFileAsync<RoundModel>(Constants.FileNames.CurrentRound);
                if (currentRound != null)
                {
                    Singleton = currentRound;
                }
                return Singleton;
            }
            catch
            {
                Singleton = null;
            }
            return Singleton;
        }

        /// <summary>
        /// Starts a new round. Populates the basic round info based on the settings and time.
        /// </summary>
        /// <returns>The newly started round.</returns>
        public static RoundModel StartNewRound()
        {
            if (Singleton != null) return null;
            var settings = ServiceResolver.Resolve<ISettings>();

            return Singleton = new RoundModel()
            {
                StartTime = DateTime.UtcNow,
                AssignedTo = settings.GetValue<string>(Constants.UserDomainName)
            };
        }

        public static async Task<bool> CompleteRoundAsync()
        {
            //Upload the current round to the server.
            var request = ServiceResolver.Resolve<IApiRequest>();

            // Note the time that the round is ending.
            CurrentRound.EndTime = DateTime.UtcNow;

            //upload the round to the server.
            var completedRound = await request.PostAsync<RoundModel>(
                    Constants.Endpoints.Rounds, CurrentRound);

            if(completedRound?.Id != Guid.Empty)
            {
                //TODO: Modify the logic here to NOT complete the round if we could not delete
                //the file.

                //means the upload was a success, so continue with the deletion of the round.
                return await DeleteCurrentRoundAsync();
            }

            //upload to the server failed, so return false.
            return false;
        }

        /// <summary>
        /// Deletes the currently stored round. This is a permenent thing, so make sure it
        /// is REALLY safe to delete.
        /// </summary>
        private static async Task<bool> DeleteCurrentRoundAsync()
        {
            var success = await FileHandler.DeleteFileAsync(Constants.FileNames.CurrentRound);
            if (!success) return success;

            Singleton = null;
            return success;
        }

        /// <summary>
        /// Saves the current round to disk. Should be called whenever you want to ensure
        /// that it persists per app instance.
        /// </summary>
        public static async Task SaveRoundToDiskAsync()
        {
            if(Singleton == null)
            {
                throw new InvalidOperationException("No round to save.");
            }

            await FileHandler.SaveFileAsync(Constants.FileNames.CurrentRound, Singleton);
        }

        private static IFileHandler FileHandler;
    }
}

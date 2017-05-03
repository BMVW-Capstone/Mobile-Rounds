using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Shared
{
    public static class ReadingManager
    {
        private class ReadingComparer : IComparer<ReadingModel>
        {
            public int Compare(ReadingModel x, ReadingModel y)
            {
                return x.ItemId.CompareTo(y.ItemId);
            }
        }

        private static List<ReadingModel> Singleton { get; set; }

        static ReadingManager()
        {
            FileHandler = ServiceResolver.Resolve<IFileHandler>();
            Reset();
        }

        public static void AddReading(ReadingModel newReading)
        {
            Singleton?.Add(newReading);
            Singleton = Singleton?.OrderBy(i => i.ItemId).ToList();
        }

        /// <summary>
        /// Returns the current round.
        /// </summary>
        public static List<ReadingModel> Readings { get { return Singleton; } }

        public static async Task Reset()
        {
            Singleton = new List<ReadingModel>();
            await DeleteCurrentReadingsAsync();
        }

        /// <summary>
        /// Finds the current reading for a given <see cref="ItemModel"/>. If none exists, returns null. 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static ReadingModel Find(Guid itemId)
        {
            var toFind = new ReadingModel
            {
                ItemId = itemId
            };

            // because our item list could potentially be large, just do a simple binary search.
            // this assumes that items are sorted always by the item id.
            var index = Readings.BinarySearch(toFind, new ReadingComparer());
            if(index > -1)
            {
                return Readings[index];
            }
            else
            {
                AddReading(toFind);
                return toFind;
            }
        }

        /// <summary>
        /// Loads the current round from the file system. If none exists, returns null.
        /// </summary>
        public static async Task<List<ReadingModel>> LoadCurrentReadingsAsync()
        {
            try
            {
                var readings = await FileHandler.GetFileAsync<ReadingHandler>(Constants.FileNames.CurrentReadings);
                if (readings != null)
                {
                    Singleton = readings.Readings.OrderBy(i => i.ItemId).ToList();
                }
                return Singleton;
            }
            catch
            {
            }
            return Singleton;
        }

        /// <summary>
        /// Saves the current round to disk. Should be called whenever you want to ensure
        /// that it persists per app instance.
        /// </summary>
        public static async Task SaveReadingsToDiskAsync()
        {
            if (Singleton == null)
            {
                throw new InvalidOperationException("No readings to save.");
            }
            await RoundManager.checkTimeout();
            var handler = new ReadingHandler() { Readings = Singleton };

            await FileHandler.SaveFileAsync(Constants.FileNames.CurrentReadings, handler);
        }

        /// <summary>
        /// Deletes the currently stored readings. This is a permenent thing, so make sure it
        /// is REALLY safe to delete.
        /// </summary>
        private static async Task<bool> DeleteCurrentReadingsAsync()
        {
            var success = await FileHandler.DeleteFileAsync(Constants.FileNames.CurrentReadings);
            if (!success) return success;

            Singleton = new List<ReadingModel>();
            return success;
        }

        private static IFileHandler FileHandler;
    }


}

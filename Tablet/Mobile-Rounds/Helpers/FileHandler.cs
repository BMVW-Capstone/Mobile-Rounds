using Mobile_Rounds.ViewModels.Platform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;

namespace Mobile_Rounds.Helpers
{
    class FileHandler : IFileHandler
    {
        public async Task<string> GetFileAsync(string fileName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(sampleFile);
        }

        public async Task<TOutput> GetFileAsync<TOutput>(string fileName)
            where TOutput : new()
        {
            var fromFile = await this.GetFileAsync(fileName);
            return JsonConvert.DeserializeObject<TOutput>(fromFile);
        }

        public async Task SaveFileAsync(string fileName, object toSave)
        {
            var jObject = Newtonsoft.Json.JsonConvert.SerializeObject(toSave);
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync(
                fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, jObject);
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var success = false;
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await storageFolder.GetFileAsync(fileName);
                // If this throws and doesn't delete, then the catch will return false.
                await file.DeleteAsync(StorageDeleteOption.Default);

                // delete worked, so return true
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }
    }
}
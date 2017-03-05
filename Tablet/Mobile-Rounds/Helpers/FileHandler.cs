using Mobile_Rounds.ViewModels.Platform;
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
        public async Task SaveFileAsync(string fileName, object toSave)
        {
            var jObject = Newtonsoft.Json.JsonConvert.SerializeObject(toSave);
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync(
                fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, jObject);
        }
    }
}
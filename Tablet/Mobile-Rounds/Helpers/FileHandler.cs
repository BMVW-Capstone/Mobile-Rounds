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
            /*
            string path = "ms-appx:///" + fileName;
            var file = await StorageFile.GetFileFromPathAsync(path);
            return await FileIO.ReadTextAsync(file);
            */
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(sampleFile);
        }
    }
}


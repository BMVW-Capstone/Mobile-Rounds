using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Platform
{
    public interface IFileHandler
    {
        Task<string> GetFileAsync(string fileName);
        Task<TOutput> GetFileAsync<TOutput>(string fileName) where TOutput: new();
        Task SaveFileAsync(string fileName, object toSave);
        //need to figure out proper local file deployment. this doesn't load out of assets
        //currently have to track down user\appdata\local\packages\thisAppGuid\LocalState and place them manually.
    }
}

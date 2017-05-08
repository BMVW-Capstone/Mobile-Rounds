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
        Task<bool> DeleteFileAsync(string fileName);
    }
}

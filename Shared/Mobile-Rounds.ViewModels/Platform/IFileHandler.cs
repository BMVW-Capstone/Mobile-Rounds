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
    }
}

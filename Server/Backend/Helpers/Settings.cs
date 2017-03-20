using Mobile_Rounds.ViewModels.Platform;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Backend.Helpers
{
    public class Settings : ISettings
    {
        public TReturn GetValue<TReturn>(string key)
        {
            return (TReturn)(ConfigurationManager.AppSettings[key] as object);
        }

        public void SaveValue(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}
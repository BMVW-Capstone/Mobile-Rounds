using Mobile_Rounds.ViewModels.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mobile_Rounds.Helpers
{
    /// <summary>
    /// Provides a way to access user specified application settings.
    /// </summary>
    public class Settings : ISettings
    {
        /// <summary>
        /// Gets the given setting from the application.
        /// </summary>
        /// <param name="key">The key to fetch.</param>
        /// <typeparam name="TReturn">The type of setting stored.</typeparam>
        /// <returns>The setting value.</returns>
        public TReturn GetValue<TReturn>(string key)
        {
            if (this.Config.Values.ContainsKey(key))
            {
                return (TReturn)this.Config.Values[key];
            }

            return default(TReturn);
        }

        /// <summary>
        /// Gets the given setting from the application.
        /// </summary>
        /// <param name="key">The key to save.</param>
        /// <param name="value">The value to save.</param>
        public void SaveValue(string key, object value)
        {
            this.Config.Values[key] = value;
        }

        private ApplicationDataContainer Config => ApplicationData.Current.LocalSettings;
    }
}

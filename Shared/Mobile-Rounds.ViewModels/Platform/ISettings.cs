using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Platform
{
    /// <summary>
    /// Provides a way to access user specified application settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Gets the given setting from the application.
        /// </summary>
        /// <param name="key">The key to fetch.</param>
        /// <typeparam name="TReturn">The type of setting stored.</typeparam>
        /// <returns>The setting value.</returns>
        TReturn GetValue<TReturn>(string key);

        /// <summary>
        /// Gets the given setting from the application.
        /// </summary>
        /// <param name="key">The key to save.</param>
        /// <param name="value">The value to save.</param>
        void SaveValue(string key, object value);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Shared.Navigation
{
    /// <summary>
    /// A service class that knows how to navigate between pages.
    /// </summary>
    public interface INavigator
    {
        /// <summary>
        /// Navigates to the specific type of screen.
        /// </summary>
        /// <param name="type">The screen to go to.</param>
        void Navigate(NavigationType type);
    }
}

using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Shared.Home
{
    /// <summary>
    /// Represents the bsaic data for the home screens as exposed to XAML.
    /// </summary>
    public class HomePageViewModel
    {
        /// <summary>
        /// The property used to handle the start of rounds.
        /// </summary>
        public ICommand StartRound { get; private set; }

        /// <summary>
        /// The property used to handle the syncing of data.
        /// </summary>
        public ICommand Sync { get; private set; }

        /// <summary>
        /// Creates and sets defaults for the view model.
        /// </summary>
        public HomePageViewModel()
        {
            Sync = new SyncCommand();
            StartRound = new StartRoundCommand();
        }
    }
}

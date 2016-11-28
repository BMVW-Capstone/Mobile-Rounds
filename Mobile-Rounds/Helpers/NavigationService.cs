using Mobile_Rounds.ViewModels.Shared.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Mobile_Rounds.Helpers
{
    public sealed class NavigationService : INavigator
    {
        /// <inheritdoc/>
        public void Navigate(NavigationType type)
        {
            var frame = Window.Current.Content as Frame;

            switch (type)
            {
                case NavigationType.AdminHome:
                    frame.Navigate(typeof(Screens.Admin.Home.Index));
                    break;
                case NavigationType.Home:
                    frame.Navigate(typeof(Screens.Regular.HomeScreen));
                    break;
                case NavigationType.UnitOfMeasure:
                    frame.Navigate(typeof(Screens.Admin.UnitOfMeasureScreen));
                    break;
                default:
                    break;
            }
        }
    }
}

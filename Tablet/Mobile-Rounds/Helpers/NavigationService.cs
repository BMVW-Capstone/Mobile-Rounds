// <copyright file="NavigationService.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Rounds.ViewModels.Shared.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Mobile_Rounds.Helpers
{
    /// <summary>
    /// Provides the functionality to move between screens in the application.
    /// </summary>
    public sealed class NavigationService : INavigator
    {
        /// <inheritdoc/>
        public void Navigate(NavigationType type)
        {
            this.Navigate(type, null);
        }

        /// <inheritdoc/>
        public void Navigate(NavigationType type, object constructorObject)
        {
            this.navigationData = constructorObject;
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
                case NavigationType.AdminStations:
                    frame.Navigate(typeof(Screens.Admin.Stations.Index));
                    break;
                case NavigationType.StartRound:
                    frame.Navigate(typeof(Screens.Regular.StartRoundScreen));
                    break;
                case NavigationType.Regions:
                    frame.Navigate(typeof(Screens.Admin.Regions.Index));
                    break;
                case NavigationType.RegionSelect:
                    frame.Navigate(typeof(Screens.Regular.RegionScreen));
                    break;
                case NavigationType.StationSelect:
                    frame.Navigate(typeof(Screens.Regular.StationScreen));
                    break;
                case NavigationType.StationInput:
                    frame.Navigate(typeof(Screens.Regular.ReadingInput));
                    break;
                case NavigationType.AdminItems:
                    frame.Navigate(typeof(Screens.Admin.Items.Index));
                    break;
                default:
                    break;
            }
        }

        public T GetNavigationData<T>()
            where T : class
        {
            if (this.navigationData == null)
            {
                return null;
            }

            return (T)this.navigationData;
        }

        private object navigationData;
    }
}

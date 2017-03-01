using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Regular.Region;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Home;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mobile_Rounds.Screens.Regular
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartRoundScreen : Page
    {

        public StartRoundScreen()
        {
            this.InitializeComponent();
        }

        private void TimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // to do: block button_click until selection is made
            // pass the time selection backend
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // placeholder for region select
            var file = ServiceResolver.Resolve<IFileHandler>();
            var reads = await file.GetFileAsync("regions_test.json");
            var vm = new RegionListViewModel(reads);
            BaseViewModel.Navigator.Navigate(ViewModels.Shared.Navigation.NavigationType.RegionSelect, vm);
        }

        private void timeTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}

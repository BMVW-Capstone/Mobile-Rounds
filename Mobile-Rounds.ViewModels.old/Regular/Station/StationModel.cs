using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Regular.Station
{
    public class StationModel : NotificationBase
    {
        public string Name { get; set; }

        public AsyncCommand Navigate { get; private set; }

        public StationModel()
        {
            this.Navigate = new AsyncCommand((obj) =>
            {
                BaseViewModel.Navigator.Navigate(Shared.Navigation.NavigationType.StationInput);
            });
        }
    }
}

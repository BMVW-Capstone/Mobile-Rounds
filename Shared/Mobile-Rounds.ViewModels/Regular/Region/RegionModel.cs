using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Regular.Region
{
    public class RegionModel : NotificationBase
    {
        public string Name { get; set; }

        public AsyncCommand Navigate { get; private set; }

        public RegionModel()
        {
            this.Navigate = new AsyncCommand((obj) =>
            {
                BaseViewModel.Navigator.Navigate(Shared.Navigation.NavigationType.StationSelect);
            });
        }
    }
}

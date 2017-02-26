using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Regular.Station;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Regular.Region
{
    public class RegionModelSource : NotificationBase
    {
        public string Name { get; set; }

        public AsyncCommand Navigate { get; private set; }

        public RegionModelSource()
        {
            this.Navigate = new AsyncCommand(async(obj) =>
            {
                var file = Platform.ServiceResolver.Resolve<IFileHandler>();
                var reads = await file.GetFileAsync("stations_test.json"); //.Result blocks UI thread apparently
                var vm = new StationListViewModel(reads, this.Name);
                BaseViewModel.Navigator.Navigate(Shared.Navigation.NavigationType.StationSelect, vm);
            });
        }
    }
}

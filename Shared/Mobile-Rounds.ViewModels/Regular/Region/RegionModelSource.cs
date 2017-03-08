using Mobile_Rounds.ViewModels.Models;
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
        public Guid Id { get; set; }

        public List<StationModel> Stations { get; set; }

        public string StationCountText
        {
            get
            {
                if (Stations.Count != 1)
                {
                    return $"{Stations.Count} Stations";
                }
                return $"{Stations.Count} Station";
            }
        }

        public RegionModelSource()
        {
            this.Navigate = new AsyncCommand((obj) =>
            {
                //var file = Platform.ServiceResolver.Resolve<IFileHandler>();
                //var reads = await file.GetFileAsync("stations.json");
                var vm = new StationListViewModel(this);
                BaseViewModel.Navigator.Navigate(Shared.Navigation.NavigationType.StationSelect, vm);
            });
        }
    }
}

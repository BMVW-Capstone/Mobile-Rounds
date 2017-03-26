using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Regular.Station;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Regular.Region
{
    public class RegionModelSource : NotificationBase
    {
        public string Name { get; set; }

        public AsyncCommand Navigate { get; private set; }
        public AsyncCommand NavigateRoot { get; private set; }
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

        public RegionModelSource(AsyncCommand navBack)
        {
            this.NavigateRoot = navBack;

            this.Navigate = new AsyncCommand(async (obj) =>
            {
                if(RoundManager.CurrentRound?.Id == Guid.Empty && this.Id != Guid.Empty)
                {
                    // only set id if the round has yet to begin
                    RoundManager.CurrentRound.RegionId = this.Id;
                    await RoundManager.SaveRoundToDiskAsync();
                }

                var file = ServiceResolver.Resolve<IFileHandler>();
                var stations = await file.GetFileAsync<StationHandler>("stations.json");
                var filtered = stations.Stations.Where(s => s.RegionId == RoundManager.CurrentRound?.RegionId);

                var vm = new StationListViewModel(this, filtered);
                BaseViewModel.Navigator.Navigate(Shared.Navigation.NavigationType.StationSelect, vm);
            });
        }
    }
}

using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Regular.Region;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Regular.StartRounds
{
    public class StartRoundViewModel : BaseViewModel
    {
        public AsyncCommand Navigate { get; set; }
        public StartRoundViewModel()
        {
            // this.Crumbs.Add(new BreadcrumbItemModel("Home", this.GoHome));
            this.Crumbs.Add(new BreadcrumbItemModel("Start Round"));
            RoundTimes = new List<RoundTimeViewModel>();

            Navigate = new AsyncCommand(async (obj) =>
            {
                //start the new round.
                StartRound();

                var file = ServiceResolver.Resolve<IFileHandler>();
                var reads = await file.GetFileAsync(Constants.FileNames.Regions);
                var vm = new RegionListViewModel(this, reads);
                Navigator.Navigate(ViewModels.Shared.Navigation.NavigationType.RegionSelect, vm);
            });

            if(RoundManager.CurrentRound != null)
            {
                //round already running, so skip this screen and go to the next.
                this.Navigate.Execute(this);
            }

            var currentHour = DateTime.Now.Hour;

            RoundTimes.Clear();

            if (currentHour >= 0 && currentHour <= 4)
            {
                // 2 round
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "2:00" });
            }
            else if (currentHour > 4 && currentHour <= 10)
            {
                // 8 round
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "8:00" });
            }
            else if (currentHour > 10 && currentHour <= 16)
            {
                //14 round
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "14:00" });
            } 
            else if (currentHour > 16 && currentHour <= 22)
            {
                // 20 round
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "20:00" });
            }
            else if (currentHour == 23)
            {
                // 2 round
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "2:00" });
            }
        }

        public List<RoundTimeViewModel> RoundTimes { get; set; }

        public RoundTimeViewModel SelectedTime
        {
            get { return selected; }
            set
            {
                selected = value;
                this.RaisePropertyChanged(nameof(SelectedTime));
            }
        }

        private void StartRound()
        {
            var newRound = RoundManager.StartNewRound();
            if(newRound == null)
            {
                //previous round didn't get removed, which probably means we shouldn't
                //be in this spot...
                //throw new InvalidOperationException(
                //    "Previous round was not completed prior to this screen.");
                return;
            }

            newRound.RoundHour = SelectedTime.RoundHourAsInt;
        }

        private RoundTimeViewModel selected;
    }
}

using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Controls;
using System;
using System.Collections.Generic;

namespace Mobile_Rounds.ViewModels.Regular.StartRounds
{
    public class StartRoundViewModel : BaseViewModel
    {
        public StartRoundViewModel()
        {
            // this.Crumbs.Add(new BreadcrumbItemModel("Home", this.GoHome));
            this.Crumbs.Add(new BreadcrumbItemModel("Start Round"));
            RoundTimes = new List<RoundTimeViewModel>();

            var currentHour = DateTime.Now.Hour;

            //Determine which round is selectable
            if (currentHour < 8)
            {
                //enable 2 and 8
                RoundTimes.Clear();
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "2:00" });
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "8:00" });
            }
            else if (currentHour > 8 && currentHour < 14)
            {
                //enable 8 and 14
                RoundTimes.Clear();
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "8:00" });
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "14:00" });
            }
            else if (currentHour > 14 && currentHour < 20)
            {
                //enable 14 and 20
                RoundTimes.Clear();
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "14:00" });
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "20:00" });
            }
            else if (currentHour > 20 || currentHour < 2)
            {
                //enable 20 and 2
                RoundTimes.Clear();
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "20:00" });
                RoundTimes.Add(new RoundTimeViewModel() { RoundHour = "2:00" });
            }
        }

        public List<RoundTimeViewModel> RoundTimes { get; set; }

        public RoundTimeViewModel SelectedTime { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Regular.StartRounds
{
    public class RoundTimeViewModel
    {
        public RoundTimeViewModel()
        {

        }

        public string RoundHour { get; set; }
        public int RoundHourAsInt
        {
            get
            {
                return int.Parse(RoundHour.Split(':')[0]);
            }
        }
    }
}

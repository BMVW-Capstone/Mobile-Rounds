using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Models
{
    public sealed class ReportModel
    {
        public string Region { get; set; }
        public string Station { get; set; }

        public string Item { get; set; }
        public string ItemMeter { get; set; }

        public RoundModel Round { get; set; }

        public ReadingModel Reading { get; set; }
    }
}

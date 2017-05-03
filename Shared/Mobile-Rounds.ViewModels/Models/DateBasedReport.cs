using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Models
{
    public sealed class DateBasedReport
    {
        public IEnumerable<ReportModel> Readings { get; set; }

        public IEnumerable<int> HoursMissed { get; set; }
    }
}

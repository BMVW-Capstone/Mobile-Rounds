using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Admin.Stations
{
    public class StationsViewModel : Shared.BaseViewModel
    {
        public StationsViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Admin", this.GoToAdmin));
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Stations"));
        }
    }
}

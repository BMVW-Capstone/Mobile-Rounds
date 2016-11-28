using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_Rounds.ViewModels.Admin.AdminHome
{
    public class AdminHomeViewModel : BaseViewModel
    {
        public ICommand GoToUnits { get; private set; }

        public AdminHomeViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Admin"));
            this.GoToUnits = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(Shared.Navigation.NavigationType.UnitOfMeasure);
            });
        }
    }
}

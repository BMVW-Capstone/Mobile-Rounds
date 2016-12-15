using Mobile_Rounds.ViewModels.Shared.Controls;

namespace Mobile_Rounds.ViewModels.Admin.Stations
{
    public class StationsViewModel : Shared.BaseViewModel
    {
        public StationsViewModel()
        {
            this.Crumbs.Add(new BreadcrumbItemModel("Admin", this.GoToAdmin));
            this.Crumbs.Add(new BreadcrumbItemModel("My Region"));
        }
    }
}

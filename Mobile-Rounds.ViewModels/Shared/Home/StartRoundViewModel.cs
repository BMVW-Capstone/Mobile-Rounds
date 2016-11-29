using Mobile_Rounds.ViewModels.Shared.Controls;

namespace Mobile_Rounds.ViewModels.Shared.Home
{
    public class StartRoundViewModel : BaseViewModel
    {
        public StartRoundViewModel()
        {
            // this.Crumbs.Add(new BreadcrumbItemModel("Home", this.GoHome));
            this.Crumbs.Add(new BreadcrumbItemModel("Start Round"));
        }
    }
}
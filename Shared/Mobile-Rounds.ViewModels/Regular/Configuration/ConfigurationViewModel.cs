using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Regular.Configuration
{
    public class ConfigurationViewModel : BaseViewModel
    {
        public AsyncCommand Save { get; private set; }
        public string ApiHost
        {
            get
            {
                return this.apiHost;
            }
            set
            {
                this.apiHost = value;
                this.RaisePropertyChanged(nameof(this.ApiHost));
                this.Save.RaiseExecuteChanged();
            }
        }

        public ConfigurationViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Settings"));
            settings = ServiceResolver.Resolve<ISettings>();
            apiHost = settings.GetValue<string>(Constants.APIHostConfigKey);
            Save = new AsyncCommand(SaveCommand, CanSave);
        }

        private void SaveCommand(object data)
        {
            settings.SaveValue(Constants.APIHostConfigKey, this.ApiHost);
        }

        private bool CanSave(object data)
        {
            return !string.IsNullOrEmpty(this.ApiHost);
        }

        private ISettings settings;
        private string apiHost;
    }
}

using Mobile_Rounds.ViewModels.Models;
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
        public AsyncCommand TestConnection { get; private set; }
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
                this.testResult = false;
                this.Save.RaiseExecuteChanged();
                this.TestConnection.RaiseExecuteChanged();
            }
        }

        public ConfigurationViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Settings"));
            settings = ServiceResolver.Resolve<ISettings>();
            apiHost = settings.GetValue<string>(Constants.APIHostConfigKey);
            Save = new AsyncCommand(async (obj) => await this.SaveCommand(), CanSave);
            TestConnection = new AsyncCommand(async (obj) => await this.TestApiConnection(), CanTest);
        }

        private async Task SaveCommand()
        {
            //save the api config.
            settings.SaveValue(Constants.APIHostConfigKey, this.ApiHost);

            //now get the users metadata.
            var userInfo = await base.Api.GetAsync<UserModel>(Constants.Endpoints.Users);
            if (userInfo  != null)
                settings.SaveValue(Constants.UserAdminKey, userInfo.IsAdministrator);
        }

        private async Task TestApiConnection()
        {
            //now get the users metadata.
            try
            {
                settings.SaveValue(Constants.APIHostConfigKey, this.ApiHost);
                var userInfo = await base.Api.GetAsync<UserModel>(Constants.Endpoints.Users);
                if (userInfo != null)
                    testResult = true;
                //throw new NullReferenceException("API Could not be contacted");
            }
            catch(Exception ex)
            {
                testResult = false;
            }
            this.Save.RaiseExecuteChanged();
        }

        private bool CanSave(object data)
        {
            return (!string.IsNullOrEmpty(this.ApiHost) && this.testResult);
        }
        private bool CanTest(object data)
        {
            return !string.IsNullOrEmpty(this.ApiHost);
        }

        private ISettings settings;
        private string apiHost;
        private bool testResult;
    }
}

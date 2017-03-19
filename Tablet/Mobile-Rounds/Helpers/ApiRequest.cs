using Mobile_Rounds.ViewModels.Platform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Mobile_Rounds.Helpers
{
    public class ApiRequest : IApiRequest
    {

        private ISettings config = ServiceResolver.Resolve<ISettings>();

        public async Task<TResult> PutAsync<TResult>(string url, object data)
            where TResult : class, new()
        {
            using (var client = this.GetClient())
            {
                var uri = this.BuildHostUrl(url);
                try
                {
                    var content = this.AsJsonContent(data);
                    var response = await client.PutAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    var serverJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(serverJson);
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }

                return null;
            }
        }

        public async Task<TResult> PostAsync<TResult>(string url, object data)
            where TResult : class, new()
        {
            using (var client = this.GetClient())
            {
                var uri = this.BuildHostUrl(url);
                try
                {
                    var content = this.AsJsonContent(data);
                    var response = await client.PostAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    var serverJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(serverJson);
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }

                return null;
            }
        }

        public async Task<TResult> GetAsync<TResult>(string url)
            where TResult : class, new()
        {
            using (var client = this.GetClient())
            {
                var uri = this.BuildHostUrl(url);

                try
                {
                    var data = await client.GetStringAsync(uri);
                    var results = JsonConvert.DeserializeObject<TResult>(data);
                    return results;
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }
            }

            return null;
        }

        private IHttpContent AsJsonContent(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
        }

        private HttpClient GetClient()
        {
            var handler = new HttpBaseProtocolFilter
            {
                AllowUI = false
            };
            return new HttpClient(handler);
        }

        private Uri BuildHostUrl(string endpoint)
        {
            var host = this.config.GetValue<string>(Constants.APIHostConfigKey);
            return new Uri(new Uri(host), endpoint);
        }
    }
}

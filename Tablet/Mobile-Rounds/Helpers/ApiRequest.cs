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
        public async Task<TResult> PutAsync<TResult>(string url, object data)
        {
            using (var client = this.GetClient())
            {
                var uri = new Uri(url);
                try
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new HttpStringContent(json);
                    var response = await client.PutAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    var serverJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(serverJson);
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }

                return default(TResult);
            }
        }

        public async Task<TResult> PostAsync<TResult>(string url, object data)
        {
            using (var client = this.GetClient())
            {
                var uri = new Uri(url);
                try
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new HttpStringContent(json);
                    var response = await client.PostAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    var serverJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(serverJson);
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }

                return default(TResult);
            }
        }

        public async Task<TResult> GetAsync<TResult>(string url)
        {
            using (var client = this.GetClient())
            {
                var uri = new Uri(url);

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

            return default(TResult);
        }

        private HttpClient GetClient()
        {
            var handler = new HttpBaseProtocolFilter
            {
                AllowUI = false
            };
            return new HttpClient(handler);
        }

    }
}

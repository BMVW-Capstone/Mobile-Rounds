using Mobile_Rounds.ViewModels.Platform;
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
        public async Task<TResult> GetAsync<TResult>(string url)
        {
            var handler = new HttpBaseProtocolFilter
            {
                AllowUI = false
            };
            using (var client = new HttpClient(handler))
            {
                var uri = new Uri(url);

                try
                {
                    
                    var data = await client.GetStringAsync(uri);
                    Debugger.Break();
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }

            }
            return default(TResult);
        }
    }
}

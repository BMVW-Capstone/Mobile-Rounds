using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Platform
{
    /// <summary>
    /// Provides a way to send and obtain data from a server.
    /// </summary>
    public interface IApiRequest
    {
        /// <summary>
        /// Performs a GET operation on a server endpoint.
        /// </summary>
        /// <typeparam name="TResult">The type of data returned from the GET request.</typeparam>
        /// <param name="uri">The API url to request.</param>
        /// <returns>The requested data.</returns>
        Task<TResult> GetAsync<TResult>(string uri)
            where TResult: class, new();

        /// <summary>
        /// Performs a POST (i.e. insert) request on the server.
        /// </summary>
        /// <typeparam name="TResult">The expected data from the server.</typeparam>
        /// <param name="uri">The url to send the data to.</param>
        /// <param name="dataToSend">The data to send to the server (the fields for a new record).</param>
        /// <returns>The inserted record, with any new property values populated by the server.</returns>
        Task<TResult> PostAsync<TResult>(string uri, object dataToSend)
            where TResult: class, new();

        /// <summary>
        /// Performs a PUT (i.e. update) request on the server.
        /// </summary>
        /// <typeparam name="TResult">The type of data to return from the server.</typeparam>
        /// <param name="uri">The url to perform the request on.</param>
        /// <param name="updatedData">The updated values to use on the server.</param>
        /// <returns>The resulting object returned by the server.</returns>
        Task<TResult> PutAsync<TResult>(string uri, object updatedData)
            where TResult : class, new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Abstractions
{
    /// <summary>
    /// Represents an object that supports read operations on a given
    /// model type.
    /// </summary>
    /// <typeparam name="TModel">The model type the repository has read operations for.</typeparam>
    public interface IReadableDataSource<TModel> 
        where TModel : class, new()
    {
        /// <summary>
        /// Fetches a list of models from an internal source.
        /// </summary>
        /// <returns>A list of models.</returns>
        IQueryable<TModel> Get();

        /// <summary>
        /// Fetches a list of models from an internal source.
        /// </summary>
        /// <returns>A list of models.</returns>
        IOrderedQueryable<TModel> GetOrdered();

        /// <summary>
        /// Gets a single model object.
        /// </summary>
        /// <returns>The model object.</returns>
        Task<TModel> GetSingleAsync();
    }
}

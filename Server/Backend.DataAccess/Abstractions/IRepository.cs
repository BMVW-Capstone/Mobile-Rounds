using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.DataAccess.Abstractions
{
    /// <summary>
    /// Represents an object that has readonly access to a model resource.
    /// </summary>
    /// <typeparam name="TModel">The type of model the object can utilize.</typeparam>
    public interface IRepository<TModel>
        : IWriteableDataSource<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// Fetches a list of models from an internal source.
        /// </summary>
        /// <returns>A list of models.</returns>
        Task<IEnumerable<TModel>> GetAsync();

        /// <summary>
        /// Gets a single model object.
        /// </summary>
        /// <returns>The model object.</returns>
        Task<TModel> GetSingleAsync();
    }
}

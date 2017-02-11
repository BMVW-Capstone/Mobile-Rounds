using System.Threading.Tasks;

namespace Backend.DataAccess.Abstractions
{
    /// <summary>
    /// Represents an object that supports CRUD operations on a given
    /// model type.
    /// </summary>
    /// <typeparam name="TModel">The model type the repository has CRUD operations for.</typeparam>
    public interface IWriteableDataSource<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// Inserts a new model object and updates any relevant properties prior to returning.
        /// </summary>
        /// <param name="toCreate">The model to insert.</param>
        /// <returns>The newly inserted model with any updated properties.</returns>
        Task<TModel> InsertAsync(TModel toCreate);

        /// <summary>
        /// Updates a model to match the given <see cref="TModel"/> parameter.
        /// </summary>
        /// <param name="toUpdate">The model with the new values.</param>
        /// <returns>The newly updated model.</returns>
        Task<TModel> UpdateAsync(TModel toUpdate);

        /// <summary>
        /// Deletes a given model object.
        /// </summary>
        /// <param name="toDelete">The model object to delete.</param>
        /// <returns>The deleted model if successfull, otherwise null.</returns>
        Task<TModel> DeleteAsync(TModel toDelete);
    }
}

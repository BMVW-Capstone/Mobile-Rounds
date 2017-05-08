using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Abstractions
{
    /// <summary>
    /// Repesents a mapping between view model CRUD and model CRUD datasources.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <typeparam name="TModel">The model type.</typeparam>
    public abstract class AbstractRepository<TViewModel, TModel> : IRepository<TViewModel>
        where TViewModel : class, new()
        where TModel : class, new()
    {
        /// <summary>
        /// Gets or sets the internal data source for this view model repository.
        /// </summary>
        internal virtual IDataSource<TModel> DataSource { get; private set; }

        /// <summary>
        /// Creates a new view model repository for use with a datasource.
        /// </summary>
        /// <param name="datasource">The datasource to use.</param>
        internal AbstractRepository(IDataSource<TModel> datasource)
        {
            DataSource = datasource;
        }

        /// <summary>
        /// Builds a new view model from a given model. This is essentially a copy constructor to
        /// a domain specific class.
        /// </summary>
        /// <param name="model">The model type to copy values from.</param>
        /// <returns>A newly populated view model with the given properties copied.</returns>
        protected virtual TViewModel BuildViewModel(TModel model)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Builds a new model from a given view model. This is essentially a copy constructor to
        /// a database specific class.
        /// </summary>
        /// <param name="viewModel">The view model type to copy values from.</param>
        /// <returns>A newly populated model with the given properties copied.</returns>
        protected virtual TModel BuildModel(TViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual Task<IEnumerable<TViewModel>> GetAsync(bool includeDeleted)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual TViewModel GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual Task<TViewModel> DeleteAsync(TViewModel toDelete)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual Task<TViewModel> InsertAsync(TViewModel toCreate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual Task<TViewModel> UpdateAsync(TViewModel toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

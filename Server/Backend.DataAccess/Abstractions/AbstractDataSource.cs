using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Abstractions
{
    /// <summary>
    /// Represents a repository with CRUD operations for a given database model type.
    /// </summary>
    /// <typeparam name="TModel">The model type to perform operations on.</typeparam>
    internal abstract class AbstractDataSource<TModel> : IDataSource<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// Gets or sets a database object to use for all CRUD operations internally.
        /// </summary>
        protected DatabaseContext Database { get; set; }

        /// <summary>
        /// Creates a new repository for performing CRUD operations on the given database.
        /// </summary>
        /// <param name="ctx">The database to act on.</param>
        protected AbstractDataSource(DatabaseContext ctx)
        {
            Database = ctx;
        }

        /// <inheritdoc />
        public abstract IQueryable<TModel> Get();

        /// <inheritdoc />
        public abstract IOrderedQueryable<TModel> GetOrdered();

        /// <inheritdoc />
        public abstract Task<TModel> GetSingleAsync();

        /// <inheritdoc />
        public abstract Task<TModel> InsertAsync(TModel toCreate);

        /// <inheritdoc />
        public abstract Task<TModel> UpdateAsync(TModel toUpdate);

        /// <inheritdoc />
        public abstract Task<TModel> DeleteAsync(TModel toDelete);

        protected async Task<bool> SaveAsync()
        {
            var success = true;
            try
            {
                await Database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                success = false;
            }
            return success;
        }
    }
}

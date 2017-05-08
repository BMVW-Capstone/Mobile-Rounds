using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Abstractions
{
    /// <summary>
    /// Represents a data source object.
    /// </summary>
    /// <typeparam name="TModel">The type of model to act on.</typeparam>
    internal interface IDataSource<TModel>
        : IReadableDataSource<TModel>, IWriteableDataSource<TModel>
        where TModel : class, new()
    {
    }
}

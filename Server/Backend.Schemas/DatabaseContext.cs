using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    /// <summary>
    /// Represents the database tables.
    /// </summary>
    public sealed class DatabaseContext : DbContext
    {
        /// <summary>
        /// Creates a new database instance using the given connection string 
        /// or name of the connection string to obtain from the web.config file.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name of the connection string in the web.config or the 
        /// connection string itself.
        /// </param>
        public DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

        public DatabaseContext() : this("DevDatabase") { }

        public IDbSet<Region> Regions { get; set; }
    }
}

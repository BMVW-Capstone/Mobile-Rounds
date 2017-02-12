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
        public DatabaseContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }

        public DatabaseContext() : this("DevDatabase") { }

        /// <summary>
        /// The <see cref="Region"/>'s table.
        /// </summary>
        public IDbSet<Region> Regions { get; set; }

        /// <summary>
        /// The <see cref="Station"/>'s table.
        /// </summary>
        public IDbSet<Station> Stations { get; set; }

        /// <summary>
        /// The <see cref="Round"/>'s table.
        /// </summary>
        public IDbSet<Round> Rounds { get; set; }

        /// <summary>
        /// The <see cref="Item"/>'s table.
        /// </summary>
        public IDbSet<Item> Items { get; set; }

        /// <summary>
        /// The <see cref="UnitOfMeasure"/>'s table.
        /// </summary>
        public IDbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

        /// <summary>
        /// The <see cref="ComparisonType"/>'s table.
        /// </summary>
        public IDbSet<ComparisonType> ComparisonTypes { get; set; }

        /// <summary>
        /// The <see cref="Specification"/>'s table.
        /// </summary>
        public IDbSet<Specification> Specifications { get; set; }

        /// <summary>
        /// The <see cref="Reading"/>'s table.
        /// </summary>
        public IDbSet<Reading> Readings { get; set; }
    }
}

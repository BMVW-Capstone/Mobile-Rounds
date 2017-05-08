using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    /// <summary>
    /// Represents the database tables.
    /// </summary>
    public class DatabaseContext : DbContext
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DatabaseContext() : this("Database") { }

        /// <summary>
        /// The <see cref="Region"/>'s table.
        /// </summary>
        public virtual IDbSet<Region> Regions { get; set; }

        /// <summary>
        /// The <see cref="Station"/>'s table.
        /// </summary>
        public virtual IDbSet<Station> Stations { get; set; }

        /// <summary>
        /// The <see cref="Round"/>'s table.
        /// </summary>
        public virtual IDbSet<Round> Rounds { get; set; }

        /// <summary>
        /// The <see cref="Item"/>'s table.
        /// </summary>
        public virtual IDbSet<Item> Items { get; set; }

        /// <summary>
        /// The <see cref="UnitOfMeasure"/>'s table.
        /// </summary>
        public virtual IDbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

        /// <summary>
        /// The <see cref="ComparisonType"/>'s table.
        /// </summary>
        public virtual IDbSet<ComparisonType> ComparisonTypes { get; set; }

        /// <summary>
        /// The <see cref="Specification"/>'s table.
        /// </summary>
        public virtual IDbSet<Specification> Specifications { get; set; }

        /// <summary>
        /// The <see cref="Reading"/>'s table.
        /// </summary>
        public virtual IDbSet<Reading> Readings { get; set; }
    }
}

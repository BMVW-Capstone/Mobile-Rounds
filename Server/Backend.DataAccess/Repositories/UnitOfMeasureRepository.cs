using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories.DataSources;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories
{
    /// <summary>
    /// Represents a way to interface with Regions in the database.
    /// </summary>
    public sealed class UnitOfMeasureRepository 
        : AbstractRepository<UnitOfMeasureModel, UnitOfMeasure>
    {
        /// <summary>
        /// Creates a new instance for working with database based regions.
        /// </summary>
        /// <param name="database">The database to use.</param>
        public UnitOfMeasureRepository(DatabaseContext database)
            : base (new UnitOfMeasureDataSource(database))
        {
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<UnitOfMeasureModel>> GetAsync(bool includeDeleted)
        {
            return await DataSource
                //Get the records in order
                .GetOrdered(includeDeleted)
                //convert records to view models 
                .Select(u => new UnitOfMeasureModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Abbreviation = u.Abbreviation,
                    IsDeleted = u.IsMarkedAsDeleted
                })
                //load the data
                .ToListAsync();

        }

        /// <inheritdoc />
        public override async Task<UnitOfMeasureModel> InsertAsync(UnitOfMeasureModel toCreate)
        {
            var model = BuildModel(toCreate);
            var result = await DataSource.InsertAsync(model);
            return BuildViewModel(result);
        }

        /// <inheritdoc />
        public override async Task<UnitOfMeasureModel> UpdateAsync(UnitOfMeasureModel toUpdate)
        {
            var model = BuildModel(toUpdate);

            var result = await DataSource.UpdateAsync(model);
            return BuildViewModel(result);
        }

        protected override UnitOfMeasureModel BuildViewModel(UnitOfMeasure model)
        {
            if (model == null) return null;
            return new UnitOfMeasureModel
            {
                Id = model.Id,
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                IsDeleted = model.IsMarkedAsDeleted
            };
        }

        protected override UnitOfMeasure BuildModel(UnitOfMeasureModel model)
        {
            if (model == null) return null;
            return new UnitOfMeasure
            {
                Id = model.Id,
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                IsMarkedAsDeleted = model.IsDeleted
            };
        }
    }
}

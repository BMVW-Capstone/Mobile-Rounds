using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories.DataSources;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories
{
    /// <summary>
    /// Represents a way to interface with Regions in the database.
    /// </summary>
    public sealed class StationRepository 
        : AbstractRepository<StationModel, Station>
    {
        /// <summary>
        /// Creates a new instance for working with database based regions.
        /// </summary>
        /// <param name="database">The database to use.</param>
        public StationRepository(DatabaseContext database)
            : base (new StationDataSource(database))
        {
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<StationModel>> GetAsync()
        {
            return await DataSource
                //Get the records in order
                .GetOrdered()
                //convert records to view models 
                .Select(r => new StationModel
                {
                    Id = r.Id,
                    RegionId = r.RegionId,
                    Name = r.Name
                })
                //load the data
                .ToListAsync();
        }

        /// <inheritdoc />
        public override async Task<StationModel> InsertAsync(StationModel toCreate)
        {
            var model = BuildModel(toCreate);
            var result = await DataSource.InsertAsync(model);
            return BuildViewModel(result);
        }

        /// <inheritdoc />
        public override async Task<StationModel> UpdateAsync(StationModel toUpdate)
        {
            var model = BuildModel(toUpdate);

            var result = await DataSource.UpdateAsync(model);
            return BuildViewModel(result);
        }

        protected override StationModel BuildViewModel(Station model)
        {
            if (model == null) return null;
            return new StationModel
            {
                Id = model.Id,
                RegionId = model.RegionId,
                Name = model.Name,
                IsDeleted = model.IsMarkedAsDeleted
            };
        }

        protected override Station BuildModel(StationModel model)
        {
            if (model == null) return null;
            return new Station
            {
                Id = model.Id,
                RegionId = model.RegionId,
                Name = model.Name,
                IsMarkedAsDeleted = model.IsDeleted
            };
        }
    }
}

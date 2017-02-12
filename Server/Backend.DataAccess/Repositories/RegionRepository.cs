using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories.DataSources;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Admin.Regions;
using Mobile_Rounds.ViewModels.Regular.Region;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories
{
    /// <summary>
    /// Represents a way to interface with Regions in the database.
    /// </summary>
    public sealed class RegionRepository 
        : AbstractRepository<RegionModel, Region>
    {
        /// <summary>
        /// Creates a new instance for working with database based regions.
        /// </summary>
        /// <param name="database">The database to use.</param>
        public RegionRepository(DatabaseContext database)
            : base (new RegionDataSource(database))
        {
        }

        /// <inheritdoc />
        public override Task<RegionModel> DeleteAsync(RegionModel toDelete)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<RegionModel>> GetAsync()
        {
            //TODO: Remove this sample implementation with a real one.
            return await DataSource
                //Get the records in order
                .GetOrdered()
                //convert records to view models 
                .Select(r => new RegionModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                //load the data
                .ToListAsync();

        }

        /// <inheritdoc />
        public override Task<RegionModel> GetSingleAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override async Task<RegionModel> InsertAsync(RegionModel toCreate)
        {
            var model = BuildModel(toCreate);
            var result = await DataSource.InsertAsync(model);
            return BuildViewModel(result);
        }

        /// <inheritdoc />
        public override async Task<RegionModel> UpdateAsync(RegionModel toUpdate)
        {
            var model = BuildModel(toUpdate);

            var result = await DataSource.UpdateAsync(model);
            return BuildViewModel(result);
        }

        protected override RegionModel BuildViewModel(Region model)
        {
            if (model == null) return null;
            return new RegionModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override Region BuildModel(RegionModel model)
        {
            if (model == null) return null;
            return new Region
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}

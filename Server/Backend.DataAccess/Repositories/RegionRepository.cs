using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories.DataSources;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Admin.Regions;
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
        : AbstractRepository<RegionsViewModel, Region>
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
        public override Task<RegionsViewModel> DeleteAsync(RegionsViewModel toDelete)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<RegionsViewModel>> GetAsync()
        {
            //TODO: Remove this sample implementation with a real one.
            return await DataSource
                //Get the records in order
                .GetOrdered()
                //convert records to view models 
                .Select(r => new RegionsViewModel
                {
                    IsAdmin = true
                })
                //load the data
                .ToListAsync();

        }

        /// <inheritdoc />
        public override Task<RegionsViewModel> GetSingleAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Task<RegionsViewModel> InsertAsync(RegionsViewModel toCreate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Task<RegionsViewModel> UpdateAsync(RegionsViewModel toUpdate)
        {
            throw new NotImplementedException();
        }

        protected override RegionsViewModel BuildViewModel(Region model)
        {
            /*
             * This is where we will actually convert from a 
             * region object to a region view model object.
             * 
             */

            //TODO: Build out actual implementation.
            return new RegionsViewModel
            {
                IsAdmin = true,
            };
        }

        protected override Region BuildModel(RegionsViewModel model)
        {
            /*
             * This is where we will actually convert from a 
             * region view model object to a region database object.
             * 
             */

            //TODO: Build out actual implementation.
            throw new NotImplementedException();
        }
    }
}

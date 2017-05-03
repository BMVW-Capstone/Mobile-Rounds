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
    /// Represents a way to interface with Rounds in the database.
    /// </summary>
    public sealed class RoundRepository : AbstractRepository<RoundModel, Round>
    {
        /// <summary>
        /// Creates a new instance for working with database based rounds.
        /// </summary>
        /// <param name="database">The database to use</param>
        public RoundRepository(DatabaseContext database)
            : base (new RoundDataSource(database))
        {
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<RoundModel>> GetAsync(bool includeDeleted)
        {
            return await DataSource
                //Get the records in order
                .GetOrdered(includeDeleted)
                //convert records to view models 
                .Select(u => new RoundModel
                {
                    Id = u.Id,
                    RegionId = u.RegionId,
                    AssignedTo = u.AssignedTo,
                    RoundHour = u.RoundHour,
                    StartTime = u.StartTime,
                    EndTime = u.EndTime,
                    IsDeleted = u.IsMarkedAsDeleted
                    
                })
                //load the data
                .ToListAsync();
        }

        /// <inheritdoc />
        public override async Task<RoundModel> InsertAsync(RoundModel toCreate)
        {
            var model = BuildModel(toCreate);
            var result = await DataSource.InsertAsync(model);
            return BuildViewModel(result);
        }

        /// <inheritdoc />
        public override async Task<RoundModel> UpdateAsync(RoundModel toUpdate)
        {
            var model = BuildModel(toUpdate);

            var result = await DataSource.UpdateAsync(model);
            return BuildViewModel(result);
        }

        protected override RoundModel BuildViewModel(Round model)
        {
            if (model == null) return null;
            return new RoundModel
            {
                Id = model.Id,
                RegionId = model.RegionId,
                AssignedTo = model.AssignedTo,
                RoundHour = model.RoundHour,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                IsDeleted = model.IsMarkedAsDeleted
            };
        }

        protected override Round BuildModel(RoundModel model)
        {
            if (model == null) return null;
            return new Round
            {
                Id = model.Id,
                RegionId = model.RegionId,
                AssignedTo = model.AssignedTo,
                RoundHour = model.RoundHour,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                IsMarkedAsDeleted = model.IsDeleted
            };
        }
    }
}

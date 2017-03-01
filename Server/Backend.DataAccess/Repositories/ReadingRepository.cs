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
    /// Represents a way to interface with Readings in the database.
    /// </summary>
    public sealed class ReadingRepository 
        : AbstractRepository<ReadingModel, Reading>
    {
        /// <summary>
        /// Creates a new instance for working with database based Readings.
        /// </summary>
        /// <param name="database">The database to use.</param>
        public ReadingRepository(DatabaseContext database)
            : base (new ReadingDataSource(database))
        {
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<ReadingModel>> GetAsync()
        {
            return await DataSource
                //Get the records in order
                .GetOrdered()
                //convert records to view models 
                .Select(r => new ReadingModel
                {
                    Id = r.Id,
                    ItemId = r.ItemId,
                    RoundId = r.RoundId,
                    TimeTaken = r.TimeTaken,
                    Value = r.Value,
                    IsOutOfSpec = r.IsOutOfSpec,
                })
                //load the data
                .ToListAsync();
        }

        /// <inheritdoc />
        public override async Task<ReadingModel> InsertAsync(ReadingModel toCreate)
        {
            var model = BuildModel(toCreate);
            var result = await DataSource.InsertAsync(model);
            return BuildViewModel(result);
        }

        /// <inheritdoc />
        public override async Task<ReadingModel> UpdateAsync(ReadingModel toUpdate)
        {
            var model = BuildModel(toUpdate);

            var result = await DataSource.UpdateAsync(model);
            return BuildViewModel(result);
        }

        protected override ReadingModel BuildViewModel(Reading model)
        {
            if (model == null) return null;
            return new ReadingModel
            {
                Id = model.Id,
                ItemId = model.ItemId,
                RoundId = model.RoundId,
                TimeTaken = model.TimeTaken,
                Value = model.Value,
                IsOutOfSpec = model.IsOutOfSpec
            };
        }

        protected override Reading BuildModel(ReadingModel model)
        {
            if (model == null) return null;
            return new Reading
            {
                Id = model.Id,
                ItemId = model.ItemId,
                RoundId = model.RoundId,
                TimeTaken = model.TimeTaken,
                Value = model.Value,
                IsOutOfSpec = model.IsOutOfSpec
            };
        }
    }
}

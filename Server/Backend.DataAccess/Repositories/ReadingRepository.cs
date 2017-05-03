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
        public async Task<DateBasedReport> GetReportAsync(DateTime reportDate)
        {
            var theDaysReadings = await DataSource
                //Get the records in order
                .GetOrdered(false)
                .Where(r => r.TimeTaken.Date == reportDate.Date)
                //convert records to view models 
                .Select(r => new ReportModel
                {
                    Region = r.Item.Station.Region.Name,
                    Station = r.Item.Station.Name,
                    Item = r.Item.Name,
                    ItemMeter = r.Item.Meter,

                    Round = new RoundModel
                    {
                        AssignedTo = r.Round.AssignedTo,
                        EndTime = r.Round.EndTime,
                        StartTime = r.Round.StartTime,
                        RoundHour = r.Round.RoundHour
                    },
                    Reading = new ReadingModel
                    {
                        Id = r.Id,
                        ItemId = r.ItemId,
                        RoundId = r.RoundId,
                        TimeTaken = r.TimeTaken,
                        Value = r.Value,
                        IsOutOfSpec = r.IsOutOfSpec,
                        Comments = r.Comments
                    }
                })
                //load the data
                .ToListAsync();

            var roundHours = theDaysReadings
                    .Select(h => h.Round.RoundHour.Hour)
                    .Distinct();

            var hoursMissed = new List<int> { 2, 8, 14, 20 }.Except(roundHours);

            return new DateBasedReport
            {
                Readings = theDaysReadings,
                HoursMissed = hoursMissed
            };
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<ReadingModel>> GetAsync(bool includeDeleted)
        {
            return await DataSource
                //Get the records in order
                .GetOrdered(includeDeleted)
                //convert records to view models 
                .Select(r => new ReadingModel
                {
                    Id = r.Id,
                    ItemId = r.ItemId,
                    RoundId = r.RoundId,
                    TimeTaken = r.TimeTaken,
                    Value = r.Value,
                    IsOutOfSpec = r.IsOutOfSpec,
                    Comments = r.Comments
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
                IsOutOfSpec = model.IsOutOfSpec,
                Comments = model.Comments
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
                IsOutOfSpec = model.IsOutOfSpec,
                Comments = model.Comments
            };
        }
    }
}

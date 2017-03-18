using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Admin.Items;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class ReadingDataSource : AbstractDataSource<Reading>
    {
        private ItemDataSource Items { get; set; }

        public ReadingDataSource(DatabaseContext ctx) : base(ctx)
        {
            Items = new ItemDataSource(ctx);
        }

        public override IQueryable<Reading> Get()
        {
            return Database.Readings;
        }

        public override IOrderedQueryable<Reading> GetOrdered(bool includeDeleted)
        {
            return Database.Readings
                .Where(r => includeDeleted || r.IsMarkedAsDeleted == false)
                .OrderBy(r => r.TimeTaken);
        }

        public override async Task<Reading> InsertAsync(Reading toCreate)
        {
            toCreate.Id = Guid.NewGuid();

            //validate the item is within spec.
            var item = Items.GetSingle(toCreate.ItemId);

            //used to get the validator object.
            var validator = ComparisonTypeViewModel.Locate(item.Specification.ComparisonType.Name);

            toCreate.IsOutOfSpec = validator.Validate(toCreate.Value,
                min: item.Specification.LowerBoundValue,
                max: item.Specification.UpperBoundValue);

            var tracked = Database.Readings.Add(toCreate);
            if(await SaveAsync())
            {
                return tracked;
            }
            return null;
        }

        public override async Task<Reading> UpdateAsync(Reading toUpdate)
        {
            var tracked = Database.Readings.Find(toUpdate.Id);

            if (tracked == null) return null;

            tracked.Value = toUpdate.Value;
            tracked.IsOutOfSpec = toUpdate.IsOutOfSpec;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

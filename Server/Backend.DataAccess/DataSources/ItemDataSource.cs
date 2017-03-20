using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class ItemDataSource : AbstractDataSource<Item>
    {
        public ItemDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override IQueryable<Item> Get()
        {
            return Database.Items
                .Where(r => r.IsMarkedAsDeleted == false);
        }

        public override IOrderedQueryable<Item> GetOrdered(bool includeDeleted)
        {
            return Database.Items
                .Where(r => includeDeleted || r.IsMarkedAsDeleted == false)
                .OrderBy(r => r.IsMarkedAsDeleted)
                .ThenBy(r => r.Name);
        }

        public override Item GetSingle(Guid id)
        {
            return Database.Items.FirstOrDefault(i => i.ItemId == id);
        }


        public override async Task<Item> InsertAsync(Item toCreate)
        {
            toCreate.ItemId = Guid.NewGuid();
            toCreate.Specification.ItemId = toCreate.ItemId;

            var tracked = Database.Items.Add(toCreate);
            if(await SaveAsync())
            {
                tracked = Database.Items.Attach(tracked);
                tracked.Specification = Database.Specifications.Attach(tracked.Specification);
                tracked.Specification.Unit = Database.UnitsOfMeasure.Find(tracked.Specification.UnitId);
                tracked.Specification.ComparisonType = Database.ComparisonTypes.Find(tracked.Specification.ComparisonTypeName);
                return tracked;
            }
            return null;
        }

        public override async Task<Item> UpdateAsync(Item toUpdate)
        {
            var tracked = Database.Items.Find(toUpdate.ItemId);

            if (tracked == null) return null;

            var trackedSpec = Database.Specifications.Attach(tracked.Specification);
            if (trackedSpec == null) return null;

            tracked.Name = toUpdate.Name;
            tracked.IsMarkedAsDeleted = toUpdate.IsMarkedAsDeleted;
            tracked.Meter = toUpdate.Meter;
            tracked.StationId = toUpdate.StationId;

            trackedSpec.UpperBoundValue = toUpdate.Specification.UpperBoundValue;
            trackedSpec.LowerBoundValue = toUpdate.Specification.LowerBoundValue;
            trackedSpec.UnitId = toUpdate.Specification.UnitId;
            trackedSpec.ComparisonTypeName = toUpdate.Specification.ComparisonTypeName;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

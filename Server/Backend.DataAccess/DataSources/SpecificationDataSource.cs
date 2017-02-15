using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class SpecificationDataSource 
        : AbstractDataSource<Specification>
    {
        public SpecificationDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override IQueryable<Specification> Get()
        {
            return Database.Specifications
                .Where(r => r.IsMarkedAsDeleted == false);
        }

        public override async Task<Specification> InsertAsync(Specification toCreate)
        {
            // the add method throws exceptions on multiplicity constraints.
            // so we need check for an existing one first, then add if one
            // is missing.
            var existing = Database.Specifications.Find(toCreate.ItemId);

            if (existing != null) return null;

            var tracked = Database.Specifications.Add(toCreate);
            if(await SaveAsync())
            {
                return tracked;
            }
            return null;
        }

        public override async Task<Specification> UpdateAsync(Specification toUpdate)
        {
            var tracked = Database.Specifications.Find(toUpdate.ItemId);

            if (tracked == null) return null;

            tracked.ComparisonTypeName = toUpdate.ComparisonTypeName;
            tracked.LowerBoundValue = toUpdate.LowerBoundValue;
            tracked.UpperBoundValue = toUpdate.UpperBoundValue;
            tracked.UnitId = toUpdate.UnitId;
            tracked.IsMarkedAsDeleted = toUpdate.IsMarkedAsDeleted;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

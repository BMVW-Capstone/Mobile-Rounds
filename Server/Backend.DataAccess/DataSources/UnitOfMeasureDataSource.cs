using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class UnitOfMeasureDataSource
        : AbstractDataSource<UnitOfMeasure>
    {
        public UnitOfMeasureDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override IQueryable<UnitOfMeasure> Get()
        {
            return Database.UnitsOfMeasure;
            //return Database.UnitsOfMeasure
            //    .Where(s => s.IsMarkedAsDeleted == false);
        }

        public override IOrderedQueryable<UnitOfMeasure> GetOrdered(bool includeDeleted)
        {
            return Get()
                .Where(u => includeDeleted || u.IsMarkedAsDeleted == false)
                .OrderBy(r => r.Name);
        }

        public override async Task<UnitOfMeasure> InsertAsync(UnitOfMeasure toCreate)
        {
            toCreate.Id = Guid.NewGuid();

            var tracked = Database.UnitsOfMeasure.Add(toCreate);
            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }

        public override async Task<UnitOfMeasure> UpdateAsync(UnitOfMeasure toUpdate)
        {
            var tracked = Database.UnitsOfMeasure.Find(toUpdate.Id);

            if (tracked == null) return null;

            tracked.Name = toUpdate.Name;
            tracked.Abbreviation = toUpdate.Abbreviation;
            tracked.IsMarkedAsDeleted = toUpdate.IsMarkedAsDeleted;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

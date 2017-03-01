using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class ReadingDataSource : AbstractDataSource<Reading>
    {
        public ReadingDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override IQueryable<Reading> Get()
        {
            return Database.Readings;
        }

        public override IOrderedQueryable<Reading> GetOrdered()
        {
            return Database.Readings
                .OrderBy(r => r.TimeTaken);
        }

        public override Reading GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        public override async Task<Reading> InsertAsync(Reading toCreate)
        {
            toCreate.Id = Guid.NewGuid();

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

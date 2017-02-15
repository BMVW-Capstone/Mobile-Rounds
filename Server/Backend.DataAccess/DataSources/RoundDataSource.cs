using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class RoundDataSource : AbstractDataSource<Round>
    {
        public RoundDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override IQueryable<Round> Get()
        {
            return Database.Rounds
                .Where(s => s.IsMarkedAsDeleted == false);
        }

        public override IOrderedQueryable<Round> GetOrdered()
        {
            // BY ID FOR NOW
            return Get().OrderBy(r => r.Id);
        }

        public override async Task<Round> InsertAsync(Round toCreate)
        {
            toCreate.Id = Guid.NewGuid();

            var tracked = Database.Rounds.Add(toCreate);
            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }

        public override async Task<Round> UpdateAsync(Round toUpdate)
        {
            var tracked = Database.Rounds.Find(toUpdate.Id);
            if (tracked == null) return null;

            tracked.AssignedTo = toUpdate.AssignedTo;
            tracked.Region = toUpdate.Region;
            tracked.RegionId = toUpdate.RegionId;
            tracked.RoundHour = toUpdate.RoundHour;
            tracked.StartTime = toUpdate.StartTime;
            tracked.EndTime = toUpdate.EndTime;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

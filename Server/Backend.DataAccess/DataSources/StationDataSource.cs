using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class StationDataSource : AbstractDataSource<Station>
    {
        public StationDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override Task<Station> DeleteAsync(Station toDelete)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Station> Get()
        {
            return Database.Stations
                .Where(s => s.IsMarkedAsDeleted == false);
        }

        public override IOrderedQueryable<Station> GetOrdered(bool includeDeleted)
        {
            return Database.Stations
                .Where(s => includeDeleted || s.IsMarkedAsDeleted == false)
                .OrderBy(r => r.Name);
        }

        public override Station GetSingle(Guid id)
        {
            return Database.Stations.Find(id);
        }


        public override async Task<Station> InsertAsync(Station toCreate)
        {
            toCreate.Id = Guid.NewGuid();

            var exists = await Database.Stations
                .Where(s => s.Name == toCreate.Name)
                .Where(s => s.RegionId == toCreate.RegionId)
                .AnyAsync();

            if (exists) return null;

            var tracked = Database.Stations.Add(toCreate);
            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }

        public override async Task<Station> UpdateAsync(Station toUpdate)
        {
            var tracked = Database.Stations.Find(toUpdate.Id);

            if (tracked == null) return null;

            tracked.Name = toUpdate.Name;
            tracked.IsMarkedAsDeleted = toUpdate.IsMarkedAsDeleted;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

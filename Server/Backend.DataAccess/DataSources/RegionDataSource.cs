using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAccess.Repositories.DataSources
{
    internal sealed class RegionDataSource : AbstractDataSource<Region>
    {
        public RegionDataSource(DatabaseContext ctx) : base(ctx)
        {
        }

        public override Task<Region> DeleteAsync(Region toDelete)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Region> Get()
        {
            return Database.Regions;
        }

        public override IOrderedQueryable<Region> GetOrdered()
        {
            return Database.Regions.OrderBy(r => r.Name);
        }

        public override Task<Region> GetSingleAsync()
        {
            throw new NotImplementedException();
        }


        public override async Task<Region> InsertAsync(Region toCreate)
        {
            toCreate.Id = Guid.NewGuid();

            var tracked = Database.Regions.Add(toCreate);
            if(await SaveAsync())
            {
                return tracked;
            }
            return null;
        }

        public override async Task<Region> UpdateAsync(Region toUpdate)
        {
            var tracked = Database.Regions.Find(toUpdate.Id);

            if (tracked == null) return null;

            tracked.Name = toUpdate.Name;
            if(await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

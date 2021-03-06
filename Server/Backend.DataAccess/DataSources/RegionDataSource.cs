﻿using Backend.DataAccess.Abstractions;
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
            return Database.Regions
                .Where(r => r.IsMarkedAsDeleted == false);
        }

        public override IOrderedQueryable<Region> GetOrdered(bool includeDeleted)
        {
            return Database.Regions
                .Where(r => includeDeleted || r.IsMarkedAsDeleted == false)
                .OrderBy(r => r.IsMarkedAsDeleted)
                .ThenBy(r => r.Name);
        }

        public override Region GetSingle(Guid id)
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
            tracked.IsMarkedAsDeleted = toUpdate.IsMarkedAsDeleted;

            if (await SaveAsync())
            {
                return tracked;
            }
            return null;
        }
    }
}

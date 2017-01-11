using Backend.DataAccess.Abstractions;
using Backend.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Mobile_Rounds.ViewModels.Admin.Regions;

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
            //TODO: Replace with actual implementation.
            return Database.Regions;
        }

        public override IOrderedQueryable<Region> GetOrdered()
        {
            //TODO: Replace with actual implementation.
            return Database.Regions.OrderBy(r => r.Name);
        }

        public override Task<Region> GetSingleAsync()
        {
            throw new NotImplementedException();
        }


        public override Task<Region> InsertAsync(Region toCreate)
        {
            throw new NotImplementedException();
        }

        public override Task<Region> UpdateAsync(Region toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

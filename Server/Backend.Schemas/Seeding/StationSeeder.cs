using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    internal class StationSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            if (!ctx.Stations.Any())
            {
                var region1 = ctx.Regions.First().Id;
                var region2 = ctx.Regions.Last().Id;

                ctx.Stations.AddOrUpdate(s => s.Id,
                    new Station
                    {
                        Id = Guid.NewGuid(),
                        Name = "Compressor Room",
                        RegionId = region1
                    },
                    new Station
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vent Room",
                        RegionId = region2
                    }
                );

                ctx.SaveChanges();
            }
        }
    }
}

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
                ctx.Stations.AddOrUpdate(s => new { s.Name, s.RegionName },
                    new Station { Name = "Compressor Room", RegionName = "North Side" },
                    new Station { Name = "Vent Room", RegionName = "South Side" }
                );

                ctx.SaveChanges();
            }
        }
    }
}

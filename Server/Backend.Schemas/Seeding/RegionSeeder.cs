using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    internal class RegionSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            if (!ctx.Regions.Any())
            {
                ctx.Regions.AddOrUpdate(r => r.Name,
                    new Region { Id = Guid.NewGuid(), Name = "North Side" },
                    new Region { Id = Guid.NewGuid(), Name = "South Side" }
                );
                ctx.SaveChanges();
            }
            
        }
    }
}

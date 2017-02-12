using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    internal class ItemSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            if (!ctx.Items.Any())
            {
                ctx.Items.AddOrUpdate(i => new { i.Name, i.Meter },
                    new Item
                    {
                        Name = "Tank 1",
                        Meter = "Temperature 1",
                        StationName = "Compressor Room",
                        RegionName = "North Side"
                    },
                    new Item
                    {
                        Name = "Tank 1",
                        Meter = "Temperature 2",
                        StationName = "Compressor Room",
                        RegionName = "North Side"
                    },
                    new Item
                    {
                        Name = "Tank 2",
                        Meter = "Valve",
                        StationName = "Compressor Room",
                        RegionName = "North Side"
                    },
                    new Item
                    {
                        Name = "Vent A",
                        Meter = "Is Open",
                        StationName = "Vent Room",
                        RegionName = "South Side"
                    },
                    new Item
                    {
                        Name = "Vent B",
                        Meter = "Is Open",
                        StationName = "Vent Room",
                        RegionName = "South Side"
                    });

                ctx.SaveChanges();
            }
            
        }
    }
}

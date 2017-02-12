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
                var firstStation = ctx.Stations.First();
                var secondStation = ctx.Stations.Last();

                ctx.Items.AddOrUpdate(i => i.Id,
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tank 1",
                        Meter = "Temperature 1",
                        StationId = firstStation.Id
                    },
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tank 1",
                        Meter = "Temperature 2",
                        StationId = firstStation.Id
                    },
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tank 2",
                        Meter = "Valve",
                        StationId = firstStation.Id
                    },
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vent A",
                        Meter = "Is Open",
                        StationId = secondStation.Id
                    },
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vent B",
                        Meter = "Is Open",
                        StationId = secondStation.Id
                    });

                ctx.SaveChanges();
            }
            
        }
    }
}

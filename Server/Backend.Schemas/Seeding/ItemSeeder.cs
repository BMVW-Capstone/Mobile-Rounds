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
            var firstStation = ctx.Stations.First();
            var secondStation = ctx.Stations.ToList().Last();
            var units = ctx.UnitsOfMeasure.ToList();
            var compType = ctx.ComparisonTypes.ToList();

            ctx.Items.AddOrUpdate(i => i.ItemId,
                new Item
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Tank 1",
                    Meter = "Temperature 1",
                    StationId = firstStation.Id,
                    Specification = new Specification
                    {
                        ComparisonTypeName = ComparisonType.LessThan,
                        LowerBoundValue = "0",
                        UpperBoundValue = "10",
                        UnitId = units[0].Id
                    }
                },
                new Item
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Tank 1",
                    Meter = "Temperature 2",
                    StationId = firstStation.Id,
                    Specification = new Specification
                    {
                        ComparisonTypeName = ComparisonType.EqualTo,
                        LowerBoundValue = "10",
                        UpperBoundValue = "10",
                        UnitId = units[1].Id
                    }
                },
                new Item
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Tank 2",
                    Meter = "Valve",
                    StationId = firstStation.Id,
                    Specification = new Specification
                    {
                        ComparisonTypeName = ComparisonType.Either,
                        LowerBoundValue = "false",
                        UpperBoundValue = "true",
                        UnitId = units[3].Id
                    }
                },
                new Item
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Vent A",
                    Meter = "Is Open",
                    StationId = secondStation.Id,
                    Specification = new Specification
                    {
                        ComparisonTypeName = ComparisonType.NotApplicable,
                        LowerBoundValue = "N/A",
                        UpperBoundValue = "N/A",
                        UnitId = units[4].Id
                    }
                },
                new Item
                {
                    ItemId = Guid.NewGuid(),
                    Name = "Vent B",
                    Meter = "Is Open",
                    StationId = secondStation.Id,
                    Specification = new Specification
                    {
                        ComparisonTypeName = ComparisonType.Either,
                        LowerBoundValue = "Yes",
                        UpperBoundValue = "No",
                        UnitId = units[4].Id
                    }
                });

            ctx.SaveChanges();
        }
    }
}

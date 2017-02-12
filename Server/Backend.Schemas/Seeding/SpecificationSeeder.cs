using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    internal class SpecificationSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            if (!ctx.Specifications.Any())
            {
                ctx.Specifications.AddOrUpdate(s => new { s.ItemName, s.ItemMeter },
                    new Specification
                    {
                        ItemName = "Tank 1",
                        ItemMeter = "Temperature 1",
                        ComparisionTypeName = ComparisonType.LessThan,
                        LowerBoundValue = "0",
                        UpperBoundValue = "10",
                        UnitName = "Celcius"
                    },
                    new Specification
                    {
                        ItemName = "Tank 1",
                        ItemMeter = "Temperature 2",
                        ComparisionTypeName = ComparisonType.EqualTo,
                        LowerBoundValue = "10",
                        UpperBoundValue = "10",
                        UnitName = "Kelvin"
                    },
                    new Specification
                    {
                        ItemName = "Vent A",
                        ItemMeter = "Is Open",
                        ComparisionTypeName = ComparisonType.Either,
                        LowerBoundValue = "false",
                        UpperBoundValue = "true",
                        UnitName = "Open or Closed"
                    },
                    new Specification
                    {
                        ItemName = "Vent B",
                        ItemMeter = "Is Open",
                        ComparisionTypeName = ComparisonType.NotApplicable,
                        LowerBoundValue = "N/A",
                        UpperBoundValue = "N/A",
                        UnitName = "Open or Closed"
                    });
                ctx.SaveChanges();
            }
            
        }
    }
}

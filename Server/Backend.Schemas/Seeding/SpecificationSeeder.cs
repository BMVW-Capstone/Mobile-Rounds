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
                var items = ctx.Items.ToList();
                var units = ctx.UnitsOfMeasure.ToList();
                var compType = ctx.ComparisonTypes.ToList();

                ctx.Specifications.AddOrUpdate(s => s.ItemId,
                    new Specification
                    {
                        ItemId = items[0].Id,
                        ComparisionTypeName = ComparisonType.LessThan,
                        LowerBoundValue = "0",
                        UpperBoundValue = "10",
                        UnitId = units[0].Id,
                    },
                    new Specification
                    {
                        ItemId = items[1].Id,
                        ComparisionTypeName = ComparisonType.EqualTo,
                        LowerBoundValue = "10",
                        UpperBoundValue = "10",
                        UnitId = units[1].Id
                    },
                    new Specification
                    {
                        ItemId = items[2].Id,
                        ComparisionTypeName = ComparisonType.Either,
                        LowerBoundValue = "false",
                        UpperBoundValue = "true",
                        UnitId = units[3].Id
                    },
                    new Specification
                    {
                        ItemId = items[4].Id,
                        ComparisionTypeName = ComparisonType.NotApplicable,
                        LowerBoundValue = "N/A",
                        UpperBoundValue = "N/A",
                        UnitId = units[4].Id
                    });
                ctx.SaveChanges();
            }
            
        }
    }
}

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
                /*
                 * NOTE: We do not save the specs here. We do this
                 * because the Items table is in charge of actually
                 * inserting them since it is a one-to-one mapping. 
                 */
                var items = ctx.Items.ToList();
                var units = ctx.UnitsOfMeasure.ToList();
                var compType = ctx.ComparisonTypes.ToList();

                //ctx.Specifications.AddOrUpdate(s => s.ItemId,
                //    ,
                //    ,
                //    ,
                //    new Specification
                //    {
                //        ItemId = items[4].ItemId,
                //        ComparisionTypeName = ComparisonType.NotApplicable,
                //        LowerBoundValue = "N/A",
                //        UpperBoundValue = "N/A",
                //        UnitId = units[4].Id
                //    });
            }
            
        }
    }
}

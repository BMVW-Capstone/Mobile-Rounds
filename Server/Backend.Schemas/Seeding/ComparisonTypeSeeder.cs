using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    internal class ComparisonTypeSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            ctx.ComparisonTypes.AddOrUpdate(t => t.Name,
                new ComparisonType { Name = ComparisonType.Between },
                new ComparisonType { Name = ComparisonType.Either },
                new ComparisonType { Name = ComparisonType.EqualTo },
                new ComparisonType { Name = ComparisonType.GreaterThan },
                new ComparisonType { Name = ComparisonType.GreaterThanOrEqual },
                new ComparisonType { Name = ComparisonType.LessThan },
                new ComparisonType { Name = ComparisonType.LessThanOrEqual },
                new ComparisonType { Name = ComparisonType.NotApplicable }
            );
            ctx.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    public class DatabaseSeeder : ISeeder
    {
        private readonly List<ISeeder> Seeders = new List<ISeeder>
        {
            new ComparisonTypeSeeder(),
            new UnitOfMeasureSeeder(),
            new RegionSeeder(),
            new RoundSeeder(),
            new StationSeeder(),
            new ItemSeeder(),
            new SpecificationSeeder(),
            new ReadingSeeder()
        };

        public void Seed(DatabaseContext ctx)
        {
            foreach (var seeder in Seeders)
            {
                try
                {
                    seeder.Seed(ctx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    Debugger.Break();
                }
            }
        }
    }
}

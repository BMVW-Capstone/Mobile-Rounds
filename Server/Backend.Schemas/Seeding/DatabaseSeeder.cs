using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                    //since this runs under the Package Manager Console,
                    //if there are any exceptions we want to know about
                    //we need to attach a debugger to actually catch it.
                    //Otherwise, it just says that Visual Studio crashed,
                    //which is no good...
                    if (Debugger.IsAttached == false)
                    {
                        Debugger.Launch();
                        Debugger.Break();
                    }
                }
            }

            //Insert their actual data.
            ctx.Regions.AddOrUpdate(i => i.Id, new Region
            {
                Id = Guid.Parse("{D3DE1F34-E12A-4151-8AC2-EBC3B4A87F75}"),
                Name = "East Side",
                Stations = new List<Station>
                {
                    new Station
                    {
                        Name = "SA4 NOx 1st Stage",
                        Items = new List<Item>
                        {
                            new Item
                            {
                                ItemId = Guid.NewGuid(),
                                Name = "Inlet Duct Pressure",
                                Meter = "K4-PDIT-143-01-01-1A",
                                Specification = new Specification
                                {
                                    ComparisonTypeName = ComparisonType.Between,
                                    LowerBoundValue = "3.5",
                                    UpperBoundValue = "4.5",
                                    Unit = new UnitOfMeasure
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Inch Water Column",
                                        Abbreviation = "\" WC"
                                    }
                                }
                            },
                            new Item
                            {
                                ItemId = Guid.NewGuid(),
                                Name = "Recirculation Flow Rate",
                                Meter = "K4-FIT-143-01-01-1A",
                                Specification = new Specification
                                {
                                    ComparisonTypeName = ComparisonType.GreaterThan,
                                    UpperBoundValue = "330",
                                    Unit = new UnitOfMeasure
                                    {
                                        Id = Guid.Parse("{696E7DA2-FD7B-40E6-A6FD-579F7B7F0AE0}"),
                                        Name = "Gallon per Minute",
                                        Abbreviation = "GPM"
                                    }
                                }
                            },
                            new Item
                            {
                                ItemId = Guid.NewGuid(),
                                Name = "Online Recirculation Pump Pressure",
                                Meter = "Analog Gauge",
                                Specification = new Specification
                                {
                                    ComparisonTypeName = ComparisonType.Between,
                                    LowerBoundValue = "15",
                                    UpperBoundValue = "30",
                                    //PSI
                                    UnitId = Guid.Parse("{FAACC888-25D3-4D07-BDF3-B0374334B949}")
                                }
                            },
                            new Item
                            {
                                ItemId = Guid.NewGuid(),
                                Name = "Stage #1 ORP",
                                Meter = "K4-AIT-143-01-02-1A",
                                Specification = new Specification
                                {
                                    ComparisonTypeName = ComparisonType.Between,
                                    LowerBoundValue = "-450",
                                    UpperBoundValue = "-20",
                                    //PSI
                                    Unit = new UnitOfMeasure
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Millivolts",
                                        Abbreviation = "mV"
                                    }
                                }
                            },
                            new Item
                            {
                                ItemId = Guid.NewGuid(),
                                Name = "Stage #1 pH",
                                Meter = "K4-AIT-143-01-01-1A",
                                Specification = new Specification
                                {
                                    ComparisonTypeName = ComparisonType.Between,
                                    LowerBoundValue = "10",
                                    UpperBoundValue = "12",
                                    Unit = new UnitOfMeasure
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Potential of Hydrogen",
                                        Abbreviation = "pH"
                                    }
                                }
                            },
                            new Item
                            {
                                ItemId = Guid.NewGuid(),
                                Name = "Stage #1 Conductivity",
                                Meter = "K4-AIT-143-01-03-1A",
                                Specification = new Specification
                                {
                                    ComparisonTypeName = ComparisonType.LessThan,
                                    UpperBoundValue = "25000",
                                    Unit = new UnitOfMeasure
                                    {
                                        Id = Guid.NewGuid(),
                                        Name = "Microsecond",
                                        Abbreviation = "µS"
                                    }
                                }
                            },
                            new Item
                            {
                                ItemId = Guid.NewGuid(),
                                Name = "RO Reject / IRW",
                                Meter = "Rotometer",
                                Specification = new Specification
                                {
                                    ComparisonTypeName = ComparisonType.Between,
                                    LowerBoundValue = "2",
                                    UpperBoundValue = "4",
                                    //GPM
                                    UnitId = Guid.Parse("{696E7DA2-FD7B-40E6-A6FD-579F7B7F0AE0}"),
                                }
                            }
                        }
                    }
                }
            });
            ctx.SaveChanges();
        }
    }
}

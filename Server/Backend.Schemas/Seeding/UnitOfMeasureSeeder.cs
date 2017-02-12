using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    internal class UnitOfMeasureSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            if (!ctx.UnitsOfMeasure.Any())
            {
                ctx.UnitsOfMeasure.AddOrUpdate(u => u.Name,
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Celcius",
                        Abbreviation = "C"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Fahrenheit",
                        Abbreviation = "F"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Kelvin",
                        Abbreviation = "K"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Pounds per Square Inch",
                        Abbreviation = "psi"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Inches",
                        Abbreviation = "in"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Centimeters",
                        Abbreviation = "cm"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Millimeters",
                        Abbreviation = "mm"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Meter",
                        Abbreviation = "m"
                    },
                    new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Open or Closed",
                        Abbreviation = "open or closed"
                    }
                );

                ctx.SaveChanges();
            }

        }
    }
}

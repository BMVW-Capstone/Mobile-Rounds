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
                    new UnitOfMeasure { Name = "Celcius", Abbreviation = "C" },
                    new UnitOfMeasure { Name = "Fahrenheit", Abbreviation = "F" },
                    new UnitOfMeasure { Name = "Kelvin", Abbreviation = "K" },
                    new UnitOfMeasure { Name = "Pounds per Square Inch", Abbreviation = "psi" },
                    new UnitOfMeasure { Name = "Inches", Abbreviation = "in" },
                    new UnitOfMeasure { Name = "Centimeters", Abbreviation = "cm" },
                    new UnitOfMeasure { Name = "Millimeters", Abbreviation = "mm" },
                    new UnitOfMeasure { Name = "Meter", Abbreviation = "m" },
                    new UnitOfMeasure { Name = "Open or Closed", Abbreviation = "open or closed" }
                );

                ctx.SaveChanges();
            }

        }
    }
}

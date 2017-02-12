using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    class ReadingSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            var baseTime = DateTime.Parse("02/11/2017");

            if (!ctx.Readings.Any())
            {
                ctx.Readings.AddOrUpdate(r => new { r.ItemName, r.ItemMeter },
                    new Reading
                    {
                        ItemName = "Tank 1",
                        ItemMeter = "Temperature 1",
                        TimeTaken = DateTime.Now,
                        Value = "5",
                        IsOutOfSpec = false,
                        RegionName = "North Side",
                        RoundHour = baseTime.AddHours(-8)
                    },
                    new Reading
                    {
                        ItemName = "Tank 1",
                        ItemMeter = "Temperature 2",
                        TimeTaken = DateTime.Now.AddMinutes(-5),
                        Value = "100",
                        IsOutOfSpec = true,
                        RegionName = "North Side",
                        RoundHour = baseTime.AddHours(-8)
                    });
                ctx.SaveChanges();
            }
        }
    }
}

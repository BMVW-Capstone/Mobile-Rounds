using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    class RoundSeeder : ISeeder
    {
        public void Seed(DatabaseContext ctx)
        {
            var baseTime = new DateTime(2017, 2, 11, 0, 0, 0);
            if (!ctx.Rounds.Any())
            {
                ctx.Rounds.AddOrUpdate(r => new { r.RoundHour, r.Region },
                new Round
                {
                    RoundHour = baseTime.AddHours(-8),
                    AssignedTo = "tyler.vanderhoef@wsu.edu",
                    StartTime = baseTime.AddHours(-8).AddMinutes(30),
                    EndTime = baseTime.AddHours(-7),
                    RegionName = "South Side"
                },
                new Round
                {
                    RoundHour = baseTime.AddHours(-8),
                    AssignedTo = "matt.burris@wsu.edu",
                    StartTime = baseTime.AddHours(-8).AddMinutes(30),
                    EndTime = baseTime.AddHours(-7),
                    RegionName = "North Side"
                },
                new Round
                {
                    RoundHour = baseTime.AddHours(-16),
                    AssignedTo = "cwillette@wsu.edu",
                    StartTime = baseTime.AddHours(-16).AddMinutes(30),
                    EndTime = baseTime.AddHours(-15),
                    RegionName = "North Side"
                });
                ctx.SaveChanges();
            }
        }
    }
}

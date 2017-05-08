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
                var regionId = ctx.Regions.First().Id;
                var region2Id = ctx.Regions.ToList().Last().Id;

                ctx.Rounds.AddOrUpdate(r => r.Id,
                    new Round
                    {
                        Id = Guid.NewGuid(),
                        RoundHour = baseTime.AddHours(-8).Hour,
                        AssignedTo = "tyler.vanderhoef@wsu.edu",
                        StartTime = baseTime.AddHours(-8).AddMinutes(30),
                        EndTime = baseTime.AddHours(-7),
                        RegionId = regionId
                    },
                    new Round
                    {
                        Id = Guid.NewGuid(),
                        RoundHour = baseTime.AddHours(-8).Hour,
                        AssignedTo = "matt.burris@wsu.edu",
                        StartTime = baseTime.AddHours(-8).AddMinutes(30),
                        EndTime = baseTime.AddHours(-7),
                        RegionId = region2Id
                    },
                    new Round
                    {
                        Id = Guid.NewGuid(),
                        RoundHour = baseTime.AddHours(-16).Hour,
                        AssignedTo = "cwillette@wsu.edu",
                        StartTime = baseTime.AddHours(-16).AddMinutes(30),
                        EndTime = baseTime.AddHours(-15),
                        RegionId = region2Id
                    });
                ctx.SaveChanges();
            }
        }
    }
}

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
                var roundId = ctx.Rounds.First().Id;
                var itemId = ctx.Items.First().ItemId;
                var item2Id = ctx.Items.ToList().Last().ItemId;

                ctx.Readings.AddOrUpdate(r => r.Id,
                    new Reading
                    {
                        Id = Guid.NewGuid(),
                        TimeTaken = DateTime.Now,
                        Value = "5",
                        IsOutOfSpec = false,
                        RoundId = roundId,
                        ItemId = itemId
                    },
                    new Reading
                    {
                        Id = Guid.NewGuid(),
                        TimeTaken = DateTime.Now.AddMinutes(-5),
                        Value = "100",
                        IsOutOfSpec = true,
                        RoundId = roundId,
                        ItemId = item2Id
                    });
                ctx.SaveChanges();
            }
        }
    }
}

using Backend.Schemas;
using Backend.Schemas.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedTests
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseSeeder seeder = new DatabaseSeeder();
            using (var ctx = new DatabaseContext())
            {
                seeder.Seed(ctx);
            }
        }
    }
}

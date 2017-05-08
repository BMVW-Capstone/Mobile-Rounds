using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas.Seeding
{
    interface ISeeder
    {
        void Seed(DatabaseContext ctx);
    }
}

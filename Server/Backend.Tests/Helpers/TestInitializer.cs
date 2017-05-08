using Backend.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests.Helpers
{
    [TestClass]
    public class TestInitializer
    {
        /// <summary>
        /// Runs once per test assembly (not per test). 
        /// Cleans the database and sets up the directory 
        /// to run the database from.
        /// </summary>
        /// <param name="context">The test context.</param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            //set location for database
            AppDomain.CurrentDomain.SetData(
                "DataDirectory",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            using (var ctx = new DatabaseContext("TestDatabase"))
            {
                //delete the database to make sure it is always recreated.
                ctx.Database.Delete();
            }
        }
    }
}

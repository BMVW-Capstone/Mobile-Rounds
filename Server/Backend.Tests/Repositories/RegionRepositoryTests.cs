using Backend.DataAccess.Repositories;
using Backend.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Regular.Region;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Tests.Repositories
{
    [TestClass]
    public class RegionRepositoryTests : BaseTestClass
    {
        [TestMethod]
        public async Task Inserts_New_Region()
        {
            var repo = new RegionRepository(Context);

            var newRegion = new RegionModel
            {
                Name = "My Custom Region"
            };

            var inserted = await repo.InsertAsync(newRegion);

            Assert.IsNotNull(inserted);
            Assert.AreEqual(1, Context.Regions.Count());
            Assert.AreEqual(inserted.Name, "My Custom Region");
            Assert.IsTrue(inserted.Id != Guid.Empty);
        }

        [TestMethod]
        public async Task Updates_Region()
        {
            var repo = new RegionRepository(Context);

            var original = new RegionModel
            {
                Id = Guid.Parse("{070F3379-2EE0-4EFB-A44F-600E485F8D6E}"),
                Name = "My Custom Region"
            };

            var inserted = await repo.InsertAsync(original);
            var updated = new RegionModel
            {
                Id = inserted.Id,
                Name = "My New Region"
            };

            var result = await repo.UpdateAsync(updated);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, Context.Regions.Count());
            Assert.AreNotEqual(inserted.Name, result.Name);
        }
    }
}

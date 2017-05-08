using Backend.DataAccess.Repositories;
using Backend.Schemas;
using Backend.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Tests.Repositories
{
    [TestClass]
    public class RegionRepositoryTests : BaseTestClass
    {
        public const string Category = "Repos";

        [TestMethod]
        [TestCategory(Category)]
        public async Task Gets_Regions_In_Order_By_Name()
        {
            var repo = new RegionRepository(Context);

            Context.Regions.Add(new Region
            {
                Id = Guid.NewGuid(),
                Name = "My Custom Region"
            });

            Context.Regions.Add(new Region
            {
                Id = Guid.NewGuid(),
                Name = "A different name"
            });
            Context.SaveChanges();

            var orderedList = await repo.GetAsync(true);

            Assert.AreEqual(2, orderedList.Count());
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom Region", orderedList.Last().Name);
        }

        [TestMethod]
        [TestCategory(Category)]
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
        [TestCategory(Category)]
        public async Task Soft_Deletes()
        {
            var repo = new RegionRepository(Context);

            var region = new Region
            {
                Id = Guid.NewGuid(),
                Name = "My Custom Region",
                IsMarkedAsDeleted = false
            };

            var deletedRegion = new RegionModel
            {
                Id = region.Id,
                Name = region.Name,
                IsDeleted = true
            };

            Context.Regions.Add(region);
            Context.SaveChanges();

            var deleted = await repo.UpdateAsync(deletedRegion);

            Assert.AreEqual(1, Context.Regions.Count());
            Assert.IsTrue(deleted.IsDeleted);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task Cannot_Insert_Duplicate_Region()
        {
            var repo = new RegionRepository(Context);

            var newRegion = new RegionModel
            {
                Name = "My Custom Region"
            };

            Context.Regions.Add(new Region
            {
                Id = Guid.NewGuid(),
                Name = newRegion.Name
            });
            Context.SaveChanges();

            var inserted = await repo.InsertAsync(newRegion);

            Assert.IsNull(inserted);
            Assert.AreEqual(1, Context.Regions.Count());
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task Cannot_Insert_Missing_Name()
        {
            var repo = new RegionRepository(Context);

            var newRegion = new RegionModel();

            var inserted = await repo.InsertAsync(newRegion);

            Assert.IsNull(inserted);
            Assert.IsFalse(Context.Regions.Any());
        }

        [TestMethod]
        [TestCategory(Category)]
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

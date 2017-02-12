using Backend.DataAccess.Repositories;
using Backend.Schemas;
using Backend.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Regular.Station;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Tests.Repositories
{
    [TestClass]
    public class StationRepositoryTests : BaseTestClass
    {
        public const string Category = "Repos";

        private readonly Guid DefaultRegionId = Guid.Parse("{12255F6B-BA85-42F9-BD3B-1732860D075C}");

        void InsertDefaultRegion()
        {
            Context.Regions.Add(new Region
            {
                Id = DefaultRegionId,
                Name = "Default Region"
            });
            Context.SaveChanges();
        }

        [TestInitialize]
        public void Startup()
        {
            InsertDefaultRegion();
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task Gets_In_Order_By_Name()
        {
            var repo = new StationRepository(Context);

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = "My Custom Station",
                RegionId = DefaultRegionId
            });

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = "A different name",
                RegionId = DefaultRegionId
            });
            Context.SaveChanges();

            var orderedList = await repo.GetAsync();

            Assert.AreEqual(2, orderedList.Count());
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom Station", orderedList.Last().Name);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task Inserts_New()
        {
            var repo = new StationRepository(Context);

            var newStation = new StationModel
            {
                Name = "My Custom Station",
                RegionId = DefaultRegionId
            };

            var inserted = await repo.InsertAsync(newStation);

            Assert.IsNotNull(inserted);
            Assert.AreEqual(1, Context.Stations.Count());
            Assert.AreEqual(inserted.Name, "My Custom Station");
            Assert.AreEqual(newStation.RegionId, inserted.RegionId);
            Assert.IsTrue(inserted.Id != Guid.Empty);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task Cannot_Insert_Duplicate()
        {
            var repo = new StationRepository(Context);

            var newStation = new StationModel
            {
                Name = "My Custom Station",
                RegionId = DefaultRegionId
            };

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = newStation.Name,
                RegionId = DefaultRegionId
            });
            Context.SaveChanges();

            var inserted = await repo.InsertAsync(newStation);

            Assert.IsNull(inserted);
            Assert.AreEqual(1, Context.Stations.Count());
        }


        [TestMethod]
        [TestCategory(Category)]
        public async Task Updates()
        {
            var repo = new StationRepository(Context);

            var original = new StationModel
            {
                Id = Guid.Parse("{070F3379-2EE0-4EFB-A44F-600E485F8D6E}"),
                Name = "My Custom Station",
                RegionId = DefaultRegionId
            };

            var inserted = await repo.InsertAsync(original);
            var updated = new StationModel
            {
                Id = inserted.Id,
                Name = "My New Station",
                RegionId = DefaultRegionId
            };

            var result = await repo.UpdateAsync(updated);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, Context.Stations.Count());
            Assert.AreNotEqual(inserted.Name, result.Name);
        }
    }
}

using Backend.Controllers;
using Backend.Schemas;
using Backend.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Regular.Station;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Tests
{
    [TestClass]
    public class StationsControllerTests : BaseTestClass
    {
        public const string Category = "Controllers";
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
        public async Task GET_Returns_List()
        {
            var controller = new StationsController(Context);
            base.ConfigureRequest(controller);

            // Act
            var result = await GetData<List<StationModel>>(controller.Get());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_Ordered_List()
        {
            var controller = new StationsController(Context);
            base.ConfigureRequest(controller);

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

            var orderedList = await GetData<List<StationModel>>(controller.Get());

            Assert.AreEqual(2, orderedList.Count());
            Assert.AreNotEqual(Guid.Empty, orderedList.First().Id);
            Assert.AreNotEqual(Guid.Empty, orderedList.Last().Id);
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom Station", orderedList.Last().Name);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_Ordered_List_Excluding_Deleted()
        {
            var controller = new StationsController(Context);
            base.ConfigureRequest(controller);

            var region = Context.Regions.First();

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = "My Custom Station",
                RegionId = region.Id
            });

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = "A different name",
                RegionId = region.Id
            });

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = "A hidden name",
                RegionId = region.Id,
                IsMarkedAsDeleted = true
            });
            Context.SaveChanges();

            var orderedList = await GetData<List<StationModel>>(controller.Get());

            Assert.AreEqual(2, orderedList.Count());
            Assert.AreNotEqual(Guid.Empty, orderedList.First().Id);
            Assert.AreNotEqual(Guid.Empty, orderedList.Last().Id);
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom Station", orderedList.Last().Name);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Is_OK()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Get());

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Is_OK()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var model = new StationModel
            {
                Name = "Test No Id",
                RegionId = DefaultRegionId
            };

            var result = await GetResponse(controller.Post(model));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Inserts_Records()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var initialCount = Context.Stations.Count();

            var model = new StationModel
            {
                Name = "Test No Id",
                RegionId = DefaultRegionId
            };

            var result = await GetData<StationModel>(controller.Post(model));

            Assert.IsNotNull(result);
            Assert.AreEqual(model.Name, result.Name);
            Assert.AreNotEqual(Guid.Empty, result.Id);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Fails_Duplicate_Records()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var model = new StationModel
            {
                Name = "Test No Id",
                RegionId = DefaultRegionId
            };

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                RegionId = DefaultRegionId
            });
            Context.SaveChanges();

            var result = await GetResponse(controller.Post(model));
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Inserts_Different_Region_Records()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var newRegionId = Guid.NewGuid();

            var model = new StationModel
            {
                Name = "Test No Id",
                RegionId = newRegionId
            };

            Context.Regions.Add(new Region
            {
                Id = newRegionId,
                Name = "My New Region"
            });

            Context.Stations.Add(new Station
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                RegionId = DefaultRegionId
            });
            Context.SaveChanges();

            var result = await GetResponse(controller.Post(model));
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Null_Data()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Put(null));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Missing_Id()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var model = new StationModel { Name = "Test No Id" };

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Missing_Region()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var model = new StationModel
            {
                Id = Guid.NewGuid(),
                Name = "Test No Region"
            };

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_OK()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var model = new StationModel
            {
                Id = Guid.Parse("{69EA67A4-C575-472B-B463-C156E5BA61F3}"),
                Name = "Test No Id",
                RegionId = DefaultRegionId
            };

            //setup database record
            Context.Stations.Add(new Station
            {
                Id = model.Id,
                Name = model.Name,
                RegionId = DefaultRegionId
            });
            Context.SaveChanges();

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Updates_Data()
        {
            var controller = new StationsController(Context);
            ConfigureRequest(controller);

            var model = new StationModel
            {
                Id = Guid.Parse("{69EA67A4-C575-472B-B463-C156E5BA61F3}"),
                Name = "Test No Id",
                RegionId = DefaultRegionId
            };

            //setup database record
            Context.Stations.Add(new Station
            {
                Id = model.Id,
                Name = model.Name,
                RegionId = DefaultRegionId
            });
            Context.SaveChanges();

            model.Name = "My New Name";

            var result = await GetData<StationModel>(controller.Put(model));

            Assert.AreEqual(model.Name, result.Name);
        }
    }
}

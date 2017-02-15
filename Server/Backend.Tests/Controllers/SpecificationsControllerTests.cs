using Backend.Controllers;
using Backend.Schemas;
using Backend.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Models;
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
    public class SpecificationsControllerTests : BaseTestClass
    {
        public const string Category = "Controllers";

        private Item SetupContext()
        {
            var region = new Region
            {
                Id = Guid.NewGuid(),
                Name = "Region"
            };
            var station = new Station
            {
                Id = Guid.NewGuid(),
                Name = "Station",
                IsMarkedAsDeleted = false,
                RegionId = region.Id
            };
            var item = new Item
            {
                Id = Guid.NewGuid(),
                IsMarkedAsDeleted = false,
                Name = "My Item",
                Meter = "The Meter",
                StationId = station.Id
            };

            var unit = new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "Celcius",
                Abbreviation = "C"
            };

            var between = new ComparisonType
            {
                Name = ComparisonType.Between
            };

            var either = new ComparisonType
            {
                Name = ComparisonType.Either
            };
            Context.ComparisonTypes.Add(between);
            Context.ComparisonTypes.Add(either);
            Context.UnitsOfMeasure.Add(unit);
            Context.Regions.Add(region);
            Context.Stations.Add(station);
            Context.Items.Add(item);
            Context.SaveChanges();
            return item;
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Is_OK()
        {
            var controller = new SpecificationsController(Context);
            ConfigureRequest(controller);

            var item = SetupContext();

            var model = new SpecificationModel
            {
                UnitOfMeasureId = Context.UnitsOfMeasure.First().Id,
                ComparisonType = ComparisonType.Between,
                LowerBound = "0",
                UpperBound=  "10",
                Id = item.Id
            };

            var result = await GetResponse(controller.Post(model));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Inserts_Records()
        {
            var controller = new SpecificationsController(Context);
            ConfigureRequest(controller);

            var initialCount = Context.Specifications.Count();

            var item = SetupContext();

            var model = new SpecificationModel
            {
                UnitOfMeasureId = Context.UnitsOfMeasure.First().Id,
                ComparisonType = ComparisonType.Between,
                LowerBound = "0",
                UpperBound = "10",
                Id = item.Id
            };

            var result = await GetData<SpecificationModel>(controller.Post(model));

            Assert.IsNotNull(result);
            Assert.AreEqual(model.UnitOfMeasureId, result.UnitOfMeasureId);
            Assert.AreEqual(model.ComparisonType, result.ComparisonType);
            Assert.AreEqual(model.LowerBound, result.LowerBound);
            Assert.AreEqual(model.UpperBound, result.UpperBound);
            Assert.AreNotEqual(Guid.Empty, result.Id);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Fails_Duplicate_Records()
        {
            var controller = new SpecificationsController(Context);
            ConfigureRequest(controller);

            var item = SetupContext();

            var model = new SpecificationModel
            {
                UnitOfMeasureId = Context.UnitsOfMeasure.First().Id,
                ComparisonType = ComparisonType.Between,
                LowerBound = "0",
                UpperBound = "10",
                Id = item.Id
            };

            Context.Specifications.Add(new Specification
            {
                ItemId = item.Id,
                UnitId = model.UnitOfMeasureId,
                ComparisionTypeName = model.ComparisonType,
                LowerBoundValue = model.LowerBound,
                UpperBoundValue = model.UpperBound
            });
            Context.SaveChanges();

            var result = await GetResponse(controller.Post(model));
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Null_Data()
        {
            var controller = new SpecificationsController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Put(null));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Missing_Id()
        {
            var controller = new SpecificationsController(Context);
            ConfigureRequest(controller);

            var model = new SpecificationModel();

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_OK()
        {
            var controller = new SpecificationsController(Context);
            ConfigureRequest(controller);

            var item = SetupContext();

            var model = new SpecificationModel
            {
                Id = item.Id,
                UnitOfMeasureId = Context.UnitsOfMeasure.First().Id,
                ComparisonType = ComparisonType.Between,
                LowerBound = "0",
                UpperBound = "10"
            };

            //setup database record
            Context.Specifications.Add(new Specification
            {
                ItemId = item.Id,
                UnitId = model.UnitOfMeasureId,
                ComparisionTypeName = model.ComparisonType,
                LowerBoundValue = model.LowerBound,
                UpperBoundValue = model.UpperBound
            });
            Context.SaveChanges();

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Updates_Data()
        {
            var controller = new SpecificationsController(Context);
            ConfigureRequest(controller);

            var item = SetupContext();

            var model = new SpecificationModel
            {
                Id = item.Id,
                UnitOfMeasureId = Context.UnitsOfMeasure.First().Id,
                ComparisonType = ComparisonType.Either,
                LowerBound = "True",
                UpperBound = "False",
                IsDeleted = true
            };

            //setup database record
            Context.Specifications.Add(new Specification
            {
                ItemId = item.Id,
                UnitId = model.UnitOfMeasureId,
                ComparisionTypeName = model.ComparisonType,
                LowerBoundValue = model.LowerBound,
                UpperBoundValue = model.UpperBound
            });
            Context.SaveChanges();

            var result = await GetData<SpecificationModel>(controller.Put(model));

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.UnitOfMeasureId, result.UnitOfMeasureId);
            Assert.AreEqual(model.ComparisonType, result.ComparisonType);
            Assert.AreEqual(model.LowerBound, result.LowerBound);
            Assert.IsTrue(result.IsDeleted);
        }
    }
}

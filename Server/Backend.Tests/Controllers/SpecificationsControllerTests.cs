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
                ItemId = Guid.NewGuid(),
                IsMarkedAsDeleted = false,
                Name = "My Item",
                Meter = "The Meter",
                StationId = station.Id,
                Specification = new Specification
                {
                    ComparisonTypeName = ComparisonType.Between,
                    LowerBoundValue = "10",
                    UpperBoundValue = "100",
                    Unit = new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test",
                        Abbreviation = "ABR"
                    }
                }
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

            var model = new SpecificationModel()
            {
                UnitOfMeasure = new UnitOfMeasureModel()
            };

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Missing_UnitId()
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
                Id = item.ItemId,
                UnitOfMeasure = new UnitOfMeasureModel
                {
                    Id = Context.UnitsOfMeasure.First().Id
                },
                ComparisonType = ComparisonType.Between,
                LowerBound = "0",
                UpperBound = "10"
            };

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
                Id = item.ItemId,
                UnitOfMeasure = new UnitOfMeasureModel
                {
                    Id = Context.UnitsOfMeasure.First().Id
                },
                ComparisonType = ComparisonType.Either,
                LowerBound = "True",
                UpperBound = "False",
                IsDeleted = true
            };

            var result = await GetData<SpecificationModel>(controller.Put(model));

            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.UnitOfMeasure.Id, result.UnitOfMeasure.Id);
            Assert.AreEqual(model.ComparisonType, result.ComparisonType);
            Assert.AreEqual(model.LowerBound, result.LowerBound);
            Assert.IsTrue(result.IsDeleted);
        }
    }
}

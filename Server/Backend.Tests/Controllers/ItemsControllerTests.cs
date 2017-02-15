using Backend.Controllers;
using Backend.Schemas;
using Backend.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests.Controllers
{
    [TestClass]
    public class ItemsControllerTests : BaseTestClass
    {
        public const string Category = "Controllers";

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Items_For_Station_Is_OK()
        {
            var controller = new ItemsController(Context);
            ConfigureRequest(controller);

            var station = new Station
            {
                Id = Guid.NewGuid(),
                Name = "Station",
                Region = new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Region"
                }
            };

            Context.Stations.Add(station);
            Context.SaveChanges();

            var result = await GetResponse(controller.Get(station.Id));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Items_For_Station()
        {
            var controller = new ItemsController(Context);
            ConfigureRequest(controller);

            var station = new Station
            {
                Id = Guid.NewGuid(),
                Name = "Station",
                Region = new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Region"
                }
            };

            var item = new Item
            {
                ItemId = Guid.NewGuid(),
                Meter = "meter",
                Name = "test",
                Station = station,
                Specification = new Specification
                {
                    ComparisonTypeName = ComparisonType.Between,
                    LowerBoundValue = "0",
                    UpperBoundValue = "10",
                    Unit = new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "C",
                        Abbreviation = "ABB"
                    }
                }
            };

            Context.Stations.Add(station);
            Context.ComparisonTypes.Add(new ComparisonType
            {
                Name = ComparisonType.Between
            });
            Context.Items.Add(item);
            Context.SaveChanges();

            var result = await GetData<List<ItemModel>>(controller.Get(station.Id));

            Assert.AreEqual(item.ItemId, result[0].Id);
            Assert.AreEqual(item.Name, result[0].Name);
            Assert.AreEqual(item.StationId, result[0].StationId);
            Assert.AreEqual(Context.Items.Count(), result.Count());
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Items_For_Station_Empty()
        {
            var controller = new ItemsController(Context);
            ConfigureRequest(controller);

            var station = new Station
            {
                Id = Guid.NewGuid(),
                Name = "Station",
                Region = new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Region"
                }
            };

            var item = new Item
            {
                ItemId = Guid.NewGuid(),
                Meter = "meter",
                Name = "test",
                Station = station,
                IsMarkedAsDeleted = true,
                Specification = new Specification
                {
                    ComparisonTypeName = ComparisonType.Between,
                    LowerBoundValue = "0",
                    UpperBoundValue = "10",
                    Unit = new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = "C",
                        Abbreviation = "ABB"
                    }
                }
            };

            Context.Stations.Add(station);
            Context.ComparisonTypes.Add(new ComparisonType
            {
                Name = ComparisonType.Between
            });
            Context.Items.Add(item);
            Context.SaveChanges();

            var result = await GetData<List<ItemModel>>(controller.Get(station.Id));

            Assert.AreEqual(0, result.Count);
        }
    }
}

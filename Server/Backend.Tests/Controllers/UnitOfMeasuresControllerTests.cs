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
    public class UnitOfMeasuresControllerTests : BaseTestClass
    {
        public const string Category = "Controllers";

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_List()
        {
            var controller = new UnitOfMeasuresController(Context);
            base.ConfigureRequest(controller);

            // Act
            var result = await GetData<List<UnitOfMeasureModel>>(controller.Get(true));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_Ordered_List_Excluding_Deleted()
        {
            var controller = new UnitOfMeasuresController(Context);
            base.ConfigureRequest(controller);

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "My Custom UnitOfMeasure"
            });

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "A different name"
            });

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "A hidden name",
                IsMarkedAsDeleted = true
            });
            Context.SaveChanges();

            var orderedList = await GetData<List<UnitOfMeasureModel>>(controller.Get(false));

            Assert.AreEqual(2, orderedList.Count());
            Assert.AreNotEqual(Guid.Empty, orderedList.First().Id);
            Assert.AreNotEqual(Guid.Empty, orderedList.Last().Id);
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom UnitOfMeasure", orderedList.Last().Name);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_Ordered_List()
        {
            var controller = new UnitOfMeasuresController(Context);
            base.ConfigureRequest(controller);

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "My Custom UnitOfMeasure"
            });

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "A different name"
            });

            Context.SaveChanges();

            var orderedList = await GetData<List<UnitOfMeasureModel>>(controller.Get(true));

            Assert.AreEqual(2, orderedList.Count());
            Assert.AreNotEqual(Guid.Empty, orderedList.First().Id);
            Assert.AreNotEqual(Guid.Empty, orderedList.Last().Id);
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom UnitOfMeasure", orderedList.Last().Name);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_Ordered_By_Name_Then_Deleted()
        {
            var controller = new UnitOfMeasuresController(Context);
            base.ConfigureRequest(controller);

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "My Custom UnitOfMeasure"
            });

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "A different name"
            });

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = Guid.NewGuid(),
                Name = "A third name",
                IsMarkedAsDeleted = true
            });

            Context.SaveChanges();

            var orderedList = await GetData<List<UnitOfMeasureModel>>(controller.Get(true));

            Assert.AreEqual(3, orderedList.Count());
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom UnitOfMeasure", orderedList.ElementAt(1).Name);
            Assert.AreEqual("A third name", orderedList.Last().Name);
        }


        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Is_OK()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Get(true));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Is_OK()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var model = new UnitOfMeasureModel
            {
                Name = "Test No Id"
            };

            var result = await GetResponse(controller.Post(model));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Inserts_Records()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var initialCount = Context.UnitsOfMeasure.Count();

            var model = new UnitOfMeasureModel
            {
                Name = "Test No Id",
                Abbreviation = "TNI"
            };

            var result = await GetData<UnitOfMeasureModel>(controller.Post(model));

            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Id);
            Assert.AreEqual(model.Name, result.Name);
            Assert.AreEqual(model.Abbreviation, result.Abbreviation);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Fails_Duplicate_Records()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var model = new UnitOfMeasureModel
            {
                Name = "Test No Id"
            };

            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Name = model.Name
            });
            Context.SaveChanges();

            var result = await GetResponse(controller.Post(model));
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }


        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Null_Data()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Put(null));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Missing_Id()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var model = new UnitOfMeasureModel { Name = "Test No Id" };

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_OK()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var model = new UnitOfMeasureModel
            {
                Id = Guid.Parse("{69EA67A4-C575-472B-B463-C156E5BA61F3}"),
                Name = "Test No Id",
                Abbreviation = "TNO"
            };

            //setup database record
            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = model.Id,
                Name = model.Name,
                Abbreviation = model.Abbreviation
            });
            Context.SaveChanges();

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Updates_Data()
        {
            var controller = new UnitOfMeasuresController(Context);
            ConfigureRequest(controller);

            var model = new UnitOfMeasureModel
            {
                Id = Guid.Parse("{69EA67A4-C575-472B-B463-C156E5BA61F3}"),
                Name = "Test No Id",
                Abbreviation = "TNI"
            };

            //setup database record
            Context.UnitsOfMeasure.Add(new UnitOfMeasure
            {
                Id = model.Id,
                Name = model.Name,
                Abbreviation = model.Abbreviation
            });
            Context.SaveChanges();

            model.Name = "My New Name";
            model.Abbreviation = "MNN";

            var result = await GetData<UnitOfMeasureModel>(controller.Put(model));

            Assert.AreEqual(model.Name, result.Name);
            Assert.AreEqual(model.Abbreviation, result.Abbreviation);
        }
    }
}

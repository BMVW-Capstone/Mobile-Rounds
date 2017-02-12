﻿using Backend.Controllers;
using Backend.Schemas;
using Backend.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobile_Rounds.ViewModels.Admin.Regions;
using Mobile_Rounds.ViewModels.Regular.Region;
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
    public class RegionsControllerTests : BaseTestClass
    {
        public const string Category = "Controllers";

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_List()
        {
            var controller = new RegionsController(Context);
            base.ConfigureRequest(controller);

            // Act
            var result = await GetData<List<RegionModel>>(controller.Get());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Returns_Ordered_List()
        {
            var controller = new RegionsController(Context);
            base.ConfigureRequest(controller);

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

            var orderedList = await GetData<List<RegionModel>>(controller.Get());

            Assert.AreEqual(2, orderedList.Count());
            Assert.AreEqual("A different name", orderedList.First().Name);
            Assert.AreEqual("My Custom Region", orderedList.Last().Name);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task GET_Is_OK()
        {
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Get());

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Is_OK()
        {
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var model = new RegionModel
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
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var initialCount = Context.Regions.Count();

            var model = new RegionModel
            {
                Name = "Test No Id"
            };

            var result = await GetData<RegionModel>(controller.Post(model));

            Assert.IsNotNull(result);
            Assert.AreEqual(model.Name, result.Name);
            Assert.AreNotEqual(Guid.Empty, result.Id);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task POST_Fails_Duplicate_Records()
        {
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var model = new RegionModel
            {
                Name = "Test No Id"
            };

            Context.Regions.Add(new Region
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
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Put(null));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_Bad_Request_Missing_Id()
        {
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var model = new RegionModel { Name = "Test No Id" };

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Is_OK()
        {
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var model = new RegionModel
            {
                Id = Guid.Parse("{69EA67A4-C575-472B-B463-C156E5BA61F3}"),
                Name = "Test No Id"
            };

            //setup database record
            Context.Regions.Add(new Region { Id = model.Id, Name = model.Name });
            Context.SaveChanges();

            var result = await GetResponse(controller.Put(model));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory(Category)]
        public async Task PUT_Updates_Data()
        {
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var model = new RegionModel
            {
                Id = Guid.Parse("{69EA67A4-C575-472B-B463-C156E5BA61F3}"),
                Name = "Test No Id"
            };

            //setup database record
            Context.Regions.Add(new Region { Id = model.Id, Name = model.Name });
            Context.SaveChanges();

            model.Name = "My New Name";

            var result = await GetData<RegionModel>(controller.Put(model));

            Assert.AreEqual(model.Name, result.Name);
        }
    }
}

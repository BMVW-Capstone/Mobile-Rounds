using Backend.Controllers;
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
        [TestMethod]
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
        public async Task GET_Is_OK()
        {
            var controller = new RegionsController(Context);
            ConfigureRequest(controller);

            var result = await GetResponse(controller.Get());

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}

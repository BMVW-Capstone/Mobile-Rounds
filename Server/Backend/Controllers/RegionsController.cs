using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Admin.Regions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    [RoutePrefix("api/regions")]
    public class RegionsController : ApiController
    {
        private readonly IRepository<RegionsViewModel> datasource;

        public RegionsController(DatabaseContext database)
        {
            datasource = new RegionRepository(database);
        }

        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var results = await datasource.GetAsync();

            return Ok(results);
        }
    }
}
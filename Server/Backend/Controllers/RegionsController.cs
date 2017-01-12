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
    [Authorize]
    public class RegionsController : ApiController
    {
        private readonly IRepository<RegionsViewModel> datasource;

        public RegionsController(DatabaseContext database)
        {
            datasource = new RegionRepository(database);
        }

        [Route("open")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAnonymous()
        {
            var user = HttpContext.Current.User;
            var role = user.IsInRole("Developers");

            var results = await datasource.GetAsync();

            return Ok(new
            {
                db = results,
                user = user.Identity.Name,
                isDeveloper = role
            });
        }

        [Route("closed")]
        public async Task<IHttpActionResult> Get()
        {
            var user = HttpContext.Current.User;
            var role = user.IsInRole("Developers");

            var results = await datasource.GetAsync();

            return Ok(new
            {
                db = results,
                user = user.Identity.Name,
                isDeveloper = role
            });
        }
    }
}
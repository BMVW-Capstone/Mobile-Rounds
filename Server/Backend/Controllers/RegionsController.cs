using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Admin.Regions;
using Mobile_Rounds.ViewModels.Regular.Region;
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
        private readonly IRepository<RegionModel> datasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionsController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public RegionsController(DatabaseContext database)
        {
            this.datasource = new RegionRepository(database);
        }

        /// <summary>
        /// Gets a list of all the regions.
        /// </summary>
        /// <returns>A list of regions.</returns>
        public async Task<IHttpActionResult> Get()
        {
            var results = await this.datasource.GetAsync();
            return this.Ok(results);
        }
    }
}
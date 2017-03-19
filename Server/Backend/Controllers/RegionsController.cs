using System.Threading.Tasks;
using System.Web.Http;
using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Models;
using Swashbuckle.Swagger.Annotations;

namespace Backend.Controllers
{
    /// <summary>
    /// The entrypoint into the <see cref="Region"/> resources.
    /// </summary>
    [RoutePrefix("api/regions")]
    [Authorize]
    public class RegionsController : ApiController
    {
        private const string SwaggerName = "Regions";

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
        /// <param name="includeDeleted">Indicates if the results should filter out deletions.</param>
        /// <returns>A list of regions.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get(bool includeDeleted)
        {
            var results = await this.datasource.GetAsync(includeDeleted);
            return this.Ok(results);
        }

        /// <summary>
        /// Inserts a given <see cref="Region"/> using the values
        /// from the <see cref="RegionModel"/>.
        /// </summary>
        /// <param name="newModel">The object with the values to insert.</param>
        /// <returns>The newly updated region. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Post(RegionModel newModel)
        {
            if (newModel == null)
            {
                return this.BadRequest("Model must not be null.");
            }

            var results = await this.datasource.InsertAsync(newModel);
            if (results == null)
            {
                return this.BadRequest("The record could not be inserted.");
            }

            return this.Ok(results);
        }

        /// <summary>
        /// Updates a given <see cref="Region"/> using the values
        /// from the <see cref="RegionModel"/>.
        /// </summary>
        /// <param name="updated">The object with the updated values.</param>
        /// <returns>The newly updated region. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Put(RegionModel updated)
        {
            if (updated == null)
            {
                return this.BadRequest("Model must not be null.");
            }

            var results = await this.datasource.UpdateAsync(updated);
            if (results == null)
            {
                return this.BadRequest("The record could not be updated.");
            }

            return this.Ok(results);
        }
    }
}

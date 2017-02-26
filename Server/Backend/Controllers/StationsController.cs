using System.Threading.Tasks;
using System.Web.Http;
using Backend.DataAccess.Abstractions;
using Backend.DataAccess.Repositories;
using Backend.Schemas;
using Mobile_Rounds.ViewModels.Models;
using Swashbuckle.Swagger.Annotations;
using System;

namespace Backend.Controllers
{
    /// <summary>
    /// The entrypoint into the <see cref="Station"/> resources.
    /// </summary>
    [RoutePrefix("api/stations")]
    [Authorize]
    public class StationsController : ApiController
    {
        public const string SwaggerName = "Stations";
        private readonly IRepository<StationModel> datasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationsController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public StationsController(DatabaseContext database)
        {
            this.datasource = new StationRepository(database);
        }

        /// <summary>
        /// Gets a list of all the stations.
        /// </summary>
        /// <returns>A list of stations.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get()
        {
            var results = await this.datasource.GetAsync();
            return this.Ok(results);
        }

        /// <summary>
        /// Inserts a given <see cref="Station"/> using the values
        /// from the <see cref="StationModel"/>.
        /// </summary>
        /// <param name="newModel">The object with the values to insert.</param>
        /// <returns>The newly updated station. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Post(StationModel newModel)
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
        /// Updates a given <see cref="Station"/> using the values
        /// from the <see cref="StationModel"/>.
        /// </summary>
        /// <param name="updated">The object with the updated values.</param>
        /// <returns>The newly updated station. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Put(StationModel updated)
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

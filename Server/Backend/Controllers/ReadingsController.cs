using System;
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
    /// The entrypoint into the <see cref="Reading"/> resources.
    /// </summary>
    [RoutePrefix("api/readings")]
    [Authorize]
    public class ReadingsController : ApiController
    {
        private const string SwaggerName = "Readings";

        private readonly ReadingRepository datasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingsController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public ReadingsController(DatabaseContext database)
        {
            this.datasource = new ReadingRepository(database);
        }

        /// <summary>
        /// Gets a list of all the Readings.
        /// </summary>
        /// <param name="includeDeleted">Indicates if the results include deleted items.</param>
        /// <returns>A list of Readings.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get(bool includeDeleted)
        {
            var results = await this.datasource.GetAsync(includeDeleted);
            return this.Ok(results);
        }

        /// <summary>
        /// Inserts a given <see cref="Reading"/> using the values
        /// from the <see cref="ReadingModel"/>.
        /// </summary>
        /// <param name="newModel">The object with the values to insert.</param>
        /// <returns>The newly updated Reading. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Post(ReadingModel newModel)
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
        /// Updates a given <see cref="Reading"/> using the values
        /// from the <see cref="ReadingModel"/>.
        /// </summary>
        /// <param name="updated">The object with the updated values.</param>
        /// <returns>The newly updated Reading. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Put(ReadingModel updated)
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

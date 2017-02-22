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
    /// The entrypoint into the <see cref="Round"/> resources.
    /// </summary>
    [RoutePrefix("api/rounds")]
    [Authorize]
    public class RoundsController : ApiController
    {
        private const string SwaggerName = "Rounds";
        private readonly IRepository<RoundModel> datasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoundsController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public RoundsController(DatabaseContext database)
        {
            this.datasource = new RoundRepository(database);
        }

        /// <summary>
        /// Gets a list of all the rounds.
        /// </summary>
        /// <returns>A list of rounds.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get()
        {
            var results = await this.datasource.GetAsync();
            return this.Ok(results);
        }

        /// <summary>
        /// Inserts a given <see cref="Round"/> using the values
        /// from the <see cref="RoundModel"/>.
        /// </summary>
        /// <param name="newModel">The object with the values to insert.</param>
        /// <returns>The newly updated unit. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Post(RoundModel newModel)
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
        /// Updates a given <see cref="Round"/> using the values
        /// from the <see cref="RoundModel"/>.
        /// </summary>
        /// <param name="updated">The object with the updated values.</param>
        /// <returns>The newly updated unit. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Put(RoundModel updated)
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

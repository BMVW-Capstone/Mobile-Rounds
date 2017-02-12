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
    [RoutePrefix("api/units")]
    [Authorize]
    public class UnitOfMeasuresController : ApiController
    {
        private const string SwaggerName = "Units Of Measure";
        private readonly IRepository<UnitOfMeasureModel> datasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasuresController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public UnitOfMeasuresController(DatabaseContext database)
        {
            this.datasource = new UnitOfMeasureRepository(database);
        }

        /// <summary>
        /// Gets a list of all the regions.
        /// </summary>
        /// <returns>A list of regions.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get()
        {
            var results = await this.datasource.GetAsync();
            return this.Ok(results);
        }

        /// <summary>
        /// Inserts a given <see cref="UnitOfMeasure"/> using the values
        /// from the <see cref="UnitOfMeasureModel"/>.
        /// </summary>
        /// <param name="newModel">The object with the values to insert.</param>
        /// <returns>The newly updated unit. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Post(UnitOfMeasureModel newModel)
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
        /// Updates a given <see cref="UnitOfMeasure"/> using the values
        /// from the <see cref="UnitOfMeasureModel"/>.
        /// </summary>
        /// <param name="updated">The object with the updated values.</param>
        /// <returns>The newly updated unit. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Put(UnitOfMeasureModel updated)
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

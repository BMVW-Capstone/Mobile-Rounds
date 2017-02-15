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
    /// The entrypoint into the <see cref="Specification"/> resources.
    /// </summary>
    [RoutePrefix("api/specifications")]
    [Authorize]
    public class SpecificationsController : ApiController
    {
        private const string SwaggerName = "Specifications";

        private readonly IRepository<SpecificationModel> datasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationsController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public SpecificationsController(DatabaseContext database)
        {
            this.datasource = new SpecificationRepository(database);
        }

        /// <summary>
        /// Inserts a given <see cref="Specification"/> using the values
        /// from the <see cref="SpecificationModel"/>.
        /// </summary>
        /// <param name="newModel">The object with the values to insert.</param>
        /// <returns>The newly updated specification. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Post(SpecificationModel newModel)
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
        /// Updates a given <see cref="Specification"/> using the values
        /// from the <see cref="SpecificationModel"/>.
        /// </summary>
        /// <param name="updated">The object with the updated values.</param>
        /// <returns>The newly updated spec. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Put(SpecificationModel updated)
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

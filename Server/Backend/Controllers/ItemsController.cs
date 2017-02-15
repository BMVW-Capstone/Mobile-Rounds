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
    /// The entrypoint into the <see cref="Item"/> resources.
    /// </summary>
    [RoutePrefix("api/items")]
    [Authorize]
    public class ItemsController : ApiController
    {
        private const string SwaggerName = "Items";

        private readonly ItemRepository datasource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsController"/> class.
        /// </summary>
        /// <param name="database">The database used for operations.</param>
        public ItemsController(DatabaseContext database)
        {
            this.datasource = new ItemRepository(database);
        }

        /// <summary>
        /// Gets a list of all the Items.
        /// </summary>
        /// <returns>A list of Items.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get()
        {
            var results = await this.datasource.GetAsync();
            return this.Ok(results);
        }

        /// <summary>
        /// Gets all the items in a station.
        /// </summary>
        /// <returns>A list of items.</returns>
        /// <param name="stationId">The id of the station.</param>
        [Route("{stationId}/items")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Get(Guid stationId)
        {
            var results = await this.datasource.GetForStationAsync(stationId);
            return this.Ok(results);
        }

        /// <summary>
        /// Inserts a given <see cref="Item"/> using the values
        /// from the <see cref="ItemModel"/>.
        /// </summary>
        /// <param name="newModel">The object with the values to insert.</param>
        /// <returns>The newly updated Item. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Post(ItemModel newModel)
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
        /// Updates a given <see cref="Item"/> using the values
        /// from the <see cref="ItemModel"/>.
        /// </summary>
        /// <param name="updated">The object with the updated values.</param>
        /// <returns>The newly updated Item. If the update failed, returns null.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public async Task<IHttpActionResult> Put(ItemModel updated)
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

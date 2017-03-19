using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Mobile_Rounds.ViewModels.Models;
using Swashbuckle.Swagger.Annotations;

namespace Backend.Controllers
{
    /// <summary>
    /// The entrypoint into the users resources.
    /// </summary>
    [RoutePrefix("api/users")]
    [Authorize]
    public class UsersController : ApiController
    {
        private const string SwaggerName = "Users";
        private readonly Dictionary<string, string> settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="settings">The app settings from the Web.config.</param>
        public UsersController(Dictionary<string, string> settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Gets the metadata for the logged in user.
        /// </summary>
        /// <returns>The logged in users metadata.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public IHttpActionResult Get()
        {
            var ident = this.RequestContext.Principal;
            var user = new UserModel
            {
                Email = ident.Identity.Name,
                IsAdministrator = ident.IsInRole(this.settings["AppAdminGroup"])
            };

            return this.Ok(user);
        }
    }
}
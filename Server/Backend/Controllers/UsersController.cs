using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Mobile_Rounds.ViewModels.Models;
using Swashbuckle.Swagger.Annotations;
using System.DirectoryServices;
using Mobile_Rounds.ViewModels.Platform;
using Backend.Helpers;

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
        private readonly ISettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="settings">The app settings from the Web.config.</param>
        public UsersController(ISettings settings)
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
                DomainName = ident.Identity.Name,
                IsAdministrator = ident.IsAppAdmin(this.settings)
            };

            if (this.settings.GetValue<string>(Constants.Web.ModeKey) != "development")
            {
                var nameSplit = ident.Identity.Name.Split('\\');
                DirectoryEntry userEntry = new DirectoryEntry($"WinNT://{nameSplit[0]}/{nameSplit[1]}");
                user.FriendlyName = userEntry.Properties["FullName"].Value.ToString();
            }

            return this.Ok(user);
        }
        /// <summary>
        /// Responds to head request
        /// </summary>
        /// <returns>The logged in users metadata.</returns>
        [Route("")]
        [SwaggerOperation(Tags = new[] { SwaggerName })]
        public IHttpActionResult Head()
        {
            return this.Ok();
        }
    }
}
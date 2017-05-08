using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Models
{
    /// <summary>
    /// Represents the currently logged in user.
    /// </summary>
    public class UserModel
    {
        public UserModel()
        {
            IsAdministrator = false;
        }

        /// <summary>
        /// The Users email address.
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets the friendly name as displayed on their login page.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the user is an admin or not.
        /// </summary>
        public bool IsAdministrator { get; set; }
    }
}

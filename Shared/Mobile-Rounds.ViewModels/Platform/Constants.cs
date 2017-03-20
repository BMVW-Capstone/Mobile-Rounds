using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Platform
{
    public static class Constants
    {
        public const string APIHostConfigKey = "API_Host";
        public const string UserAdminKey = "USER_Admin";

        public static class ApiOptions
        {
            public const string IncludeDeleted = "includeDeleted=true";
            public const string ExcludeDeleted = "includeDeleted=false";
        }

        public static class Endpoints
        {
            public const string Users = "/api/users";
            public const string Units = "/api/units";
            public const string Regions = "/api/regions";
            public const string Stations = "/api/stations";
            public const string Rounds = "/api/rounds";
            public const string Items = "/api/items";
        }

        public static class Web
        {
            /// <summary>
            /// This corresponds to the Web.config key for admin group in the api app.
            /// </summary>
            public const string AdminGroupKey = "AppAdminGroup";
        }
    }
}

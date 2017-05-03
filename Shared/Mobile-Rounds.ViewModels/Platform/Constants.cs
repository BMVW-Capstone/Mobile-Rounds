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
        public const string UserDomainName = "USER_Domain_Name";

        public static class FileNames
        {
            public const string CurrentRound = "current_round.json";
            public const string CurrentReadings = "current_readings.json";
            public const string Regions = "regions.json";
            public const string Stations = "stations.json";
            public const string Units = "units.json";
        }

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
            public const string Readings = "/api/readings";
        }

        public static class Web
        {
            /// <summary>
            /// This corresponds to the Web.config key for admin group in the api app.
            /// </summary>
            public const string AdminGroupKey = "AppAdminGroup";

            /// <summary>
            /// This corresponds to the Web.config key for admin group in the api app.
            /// </summary>
            public const string ModeKey = "Mode";
        }
    }
}

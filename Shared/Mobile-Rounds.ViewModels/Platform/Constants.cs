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

        public static class ApiOptions
        {
            public const string IncludeDeleted = "includeDeleted=true";
            public const string ExcludeDeleted = "includeDeleted=false";
        }

        public static class Endpoints
        {
            public const string Units = "/api/units";
            public const string Regions = "/api/regions";
            public const string Stations = "/api/stations";
            public const string Rounds = "/api/rounds";
            public const string Items = "/api/items";
        }
    }
}

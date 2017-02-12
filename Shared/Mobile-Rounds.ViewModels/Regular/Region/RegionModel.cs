using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Regular.Region
{
    /// <summary>
    /// Represents the contract between the server and mobile app.
    /// </summary>
    public class RegionModel
    {
        /// <summary>
        /// The id of this particular region.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of a region.
        /// </summary>
        public string Name { get; set; }
    }
}

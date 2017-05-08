using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Models
{
    /// <summary>
    /// Represents the contract between the server and mobile app.
    /// </summary>
    public class RegionModel
    {
        public RegionModel()
        {
            Stations = new List<StationModel>();
        }
        /// <summary>
        /// The id of this particular region.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of a region.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Marks if the record is percieved as deleted
        /// in the database. This is considered a soft
        /// delete.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// A list of stations that are inside this region.
        /// </summary>
        public List<StationModel> Stations { get; set; }
    }
}

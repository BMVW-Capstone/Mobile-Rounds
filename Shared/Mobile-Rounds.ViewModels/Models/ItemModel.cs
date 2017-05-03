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
    public class ItemModel
    {
        /// <summary>
        /// The id of this particular item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of an item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the meter.
        /// </summary>
        public string Meter { get; set; }

        /// <summary>
        /// The Id of the station.
        /// </summary>
        public Guid StationId { get; set; }

        public string StationName { get; set; }

        public string RegionName { get; set; }

        /// <summary>
        /// Marks if the record is percieved as deleted
        /// in the database. This is considered a soft
        /// delete.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// The acceptable values for the given item.
        /// </summary>
        public SpecificationModel Specification { get; set; }

        public ReadingModel CurrentReading { get; set; }

        public IEnumerable<ReadingModel> PastFourReadings { get; set; }
    }
}

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
    public class ReadingModel
    {
        /// <summary>
        /// The id of this particular reading.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The id of the item to pull from.
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// The name of an reading.
        /// </summary>
        public Guid RoundId { get; set; }


        /// <summary>
        /// The time of reading.
        /// </summary>
        public DateTime TimeTaken { get; set; }

        /// <summary>
        /// The value of the reading.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Marks if the record is percieved as 
        /// out of spec.
        /// </summary>
        public bool IsOutOfSpec { get; set; }
    }
}

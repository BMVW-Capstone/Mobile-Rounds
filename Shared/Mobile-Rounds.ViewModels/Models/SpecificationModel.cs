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
    public class SpecificationModel
    {
        /// <summary>
        /// The id of this particular specification.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The max value of a specification.
        /// </summary>
        public string UpperBound { get; set; }

        /// <summary>
        /// The min value of a specification.
        /// </summary>
        public string LowerBound { get; set; }

        /// <summary>
        /// The name of a comparison type used.
        /// </summary>
        public string ComparisonType { get; set; }

        /// <summary>
        /// The id of a the unit of measure used.
        /// </summary>
        public Guid UnitOfMeasureId { get; set; }

        /// <summary>
        /// Marks if the record is percieved as deleted
        /// in the database. This is considered a soft
        /// delete.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}

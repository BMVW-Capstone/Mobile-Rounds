using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class Round
    {
        /// <summary>
        /// This is the date + hour. No minute. This is because a <see cref="Region"/>
        /// should only have one round per hour block.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public DateTime RoundHour { get; set; }

        /// <summary>
        /// Foreign key to the <see cref="Region"/> as well as the second part of
        /// the composite key for a given round.
        /// </summary>
        [Key, Index(IsUnique = false), Column(Order = 1)]
        [ForeignKey(nameof(Region))]
        public string RegionName { get; set; }

        /// <summary>
        /// User who is assigned the round. Decided when a person starts the round.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string AssignedTo { get; set; }

        /// <summary>
        /// UTC based start time of the round.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// UTC based end time of the round.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Navigation property to the region the round belongs to.
        /// </summary>
        public virtual Region Region { get; set; }

    }
}

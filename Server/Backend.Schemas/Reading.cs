using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class Reading : RemovableRecord
    {
        public Reading()
        {
            //Assume that the reading is out of spec. This way,
            //if something goes wrong it will default to notifying somebody.
            IsOutOfSpec = true;
        }

        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The id of an item to take readings from in a <see cref="Station"/>. An
        /// example would be Tank 2 C02 or something like that.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(Item))]
        public Guid ItemId { get; set; }

        /// <summary>
        /// The date and time that the reading was taken.
        /// </summary>
        [Index(IsUnique = true)]
        public DateTime TimeTaken { get; set; }

        /// <summary>
        /// The id for the round.
        /// </summary>
        public Guid RoundId { get; set; }

        /// <summary>
        /// The value that was recorded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }

        /// <summary>
        /// Indicates if the recorded value was out of spec or not.
        /// </summary>
        public bool IsOutOfSpec { get; set; }

        /// <summary>
        /// Gets the comments (if any) for the current reading.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The <see cref="Item"/> that this reading belongs to.
        /// </summary>
        public virtual Item Item { get; set; }

        /// <summary>
        /// The current round.
        /// </summary>
        public virtual Round Round { get; set; }
    }
}

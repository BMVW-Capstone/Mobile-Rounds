using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class Reading
    {
        /// <summary>
        /// This is 0 because the other composite keys define 2 and 3.
        /// </summary>
        public const int CompositKey_TimeTaken = 0;

        public Reading()
        {
            //Assume that the reading is out of spec. This way,
            //if something goes wrong it will default to notifying somebody.
            IsOutOfSpec = true;
        }

        /// <summary>
        /// The name of an item to take readings from in a <see cref="Station"/>. An
        /// example would be Tank 2 C02 or something like that.
        /// </summary>
        [Key, Column(Order = Item.CompositeKey_Name)]
        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(Item))]
        public string ItemName { get; set; }

        /// <summary>
        /// TBD. Cannot remember at them moment...
        /// </summary>
        [Key, Column(Order = Item.CompositeKey_Meter)]
        [Index]
        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(Item))]
        public string ItemMeter { get; set; }

        /// <summary>
        /// The date and time that the reading was taken.
        /// </summary>
        [Key, Column(Order = CompositKey_TimeTaken)]
        [Index(IsUnique = true)]
        public DateTime TimeTaken { get; set; }

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
        /// The <see cref="Item"/> that this reading belongs to.
        /// </summary>
        public virtual Item Item { get; set; }
    }
}

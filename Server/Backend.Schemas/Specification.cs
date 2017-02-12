using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Schemas
{
    public class Specification
    {
        /// <summary>
        /// The name of an item to take readings from in a <see cref="Station"/>. An
        /// example would be Tank 2 C02 or something like that.
        /// </summary>
        [Key, Column(Order = Item.CompositeKey_Name)]
        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(Item))]
        public string ItemName { get; set; }

        /// <summary>
        /// TBD. Cannot remember at them moment...
        /// </summary>
        [Key, Column(Order = Item.CompositeKey_Meter)]
        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(Item))]
        public string ItemMeter { get; set; }

        /// <summary>
        /// The name of the unit of measure.
        /// </summary>
        [ForeignKey(nameof(Unit))]
        [Required(AllowEmptyStrings = false)]
        public string UnitName { get; set; }

        /// <summary>
        /// The name of the comparison type.
        /// </summary>
        [ForeignKey(nameof(ComparisonType))]
        [Required(AllowEmptyStrings = false)]
        public string ComparisionTypeName { get; set; }

        /// <summary>
        /// The lower bound value of this spec.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string LowerBoundValue { get; set; }

        /// <summary>
        /// The upper bound value of this spec.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string UpperBoundValue { get; set; }

        /// <summary>
        /// The type of comparison this spec uses.
        /// </summary>
        public virtual ComparisonType ComparisonType { get; set; }

        /// <summary>
        /// The unit that this spec uses.
        /// </summary>
        public virtual UnitOfMeasure Unit { get; set; }

        /// <summary>
        /// The item that this specification is enforced on.
        /// </summary>
        public virtual Item Item { get; set; }
    }
}

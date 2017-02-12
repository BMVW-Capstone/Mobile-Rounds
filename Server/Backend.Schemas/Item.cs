using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    /// <summary>
    /// An item that can have a value taken from it. This is essentially like a meter
    /// or anything that needs values recorded.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// These must be two and three since station columns define 0 and 1.
        /// </summary>
        public const int CompositeKey_Name = 2;
        public const int CompositeKey_Meter = 3;

        /// <summary>
        /// The name of an item to take readings from in a <see cref="Station"/>. An
        /// example would be Tank 2 C02 or something like that.
        /// </summary>
        [Key, Column(Order = CompositeKey_Name)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// TBD. Cannot remember at them moment...
        /// </summary>
        [Key, Column(Order = CompositeKey_Meter)]
        [Required(AllowEmptyStrings = false)]
        public string Meter { get; set; }

        /// <summary>
        /// The name of the station it is in.
        /// </summary>
        [ForeignKey(nameof(Station))]
        [Column(Order = Station.CompositeKey_Name)]
        [Required(AllowEmptyStrings = false)]
        public string StationName { get; set; }

        /// <summary>
        /// The name of the region that the station is in.
        /// </summary>
        [ForeignKey(nameof(Station))]
        [Column(Order = Station.CompositeKey_Region)]
        [Required(AllowEmptyStrings = false)]
        public string RegionName { get; set; }

        /// <summary>
        /// The <see cref="Station"/> that the meter belongs in.
        /// </summary>
        public virtual Station Station { get; set; }

        /// <summary>
        /// The <see cref="Specification"/> that the meter belongs in.
        /// </summary>
        public virtual Specification Specification { get; set; }

        /// <summary>
        /// The list of readings that have taken place for this item.
        /// </summary>
        public virtual ICollection<Reading> Readings { get; set; }
    }
}

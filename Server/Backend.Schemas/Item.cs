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
    public class Item : RemovableRecord
    {
        [Key, ForeignKey(nameof(Specification))]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of an item to take readings from in a <see cref="Station"/>. An
        /// example would be Tank 2 C02 or something like that.
        /// </summary>
        [Index(IsUnique = false)]
        [Required(AllowEmptyStrings = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(300)]
        public string Name { get; set; }

        /// <summary>
        /// The name of the instrument that has readings.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Meter { get; set; }

        /// <summary>
        /// The station it is in.
        /// </summary>
        [ForeignKey(nameof(Station))]
        public Guid StationId { get; set; }

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

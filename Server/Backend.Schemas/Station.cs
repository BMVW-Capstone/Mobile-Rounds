using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class Station : RemovableRecord
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the station. Like Compressor Room.
        /// </summary>
        [Index(IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(300)]
        public string Name { get; set; }

        /// <summary>
        /// The id of the region it belongs to.
        /// </summary>
        [Index(IsUnique = false)]
        [ForeignKey(nameof(Region))]
        public Guid RegionId { get; set; }

        /// <summary>
        /// The <see cref="Region"/> that these stations belong to.
        /// </summary>
        public virtual Region Region { get; set; }

        /// <summary>
        /// The list of <see cref="Item"/>'s that belong in this station.
        /// </summary>
        public virtual ICollection<Item> Items { get; set; }
    }
}

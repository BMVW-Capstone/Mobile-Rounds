using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class Station
    {
        public const int CompositeKey_Name = 0;
        public const int CompositeKey_Region = 1;

        /// <summary>
        /// The name of the station. Like Compressor Room.
        /// </summary>
        [Key, Column(Order = CompositeKey_Name)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        /// <summary>
        /// The name of the region it belongs to.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Key, Index, Column(Order = CompositeKey_Region)]
        [ForeignKey(nameof(Region))]
        public string RegionName { get; set; }

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

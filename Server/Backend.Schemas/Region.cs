using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class Region : RemovableRecord
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of a region.
        /// </summary>
        [Index(IsUnique = true)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(300)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// Rounds that have been associated with this region.
        /// </summary>
        public virtual ICollection<Round> Rounds { get; set; }

        /// <summary>
        /// Station that have been associated with this region.
        /// </summary>
        public virtual ICollection<Station> Stations { get; set; }
    }
}

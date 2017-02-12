using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class Region
    {
        /// <summary>
        /// The name of a region.
        /// </summary>
        [Key]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        /// <summary>
        /// Rounds that have been associated with this region.
        /// </summary>
        public virtual ICollection<Round> Rounds { get; set; }
    }
}

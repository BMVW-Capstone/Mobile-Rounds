using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class UnitOfMeasure
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the unit. Such as kelvin, pounds per square inch, etc...
        /// </summary>
        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(300)]
        public string Name { get; set; }

        /// <summary>
        /// The abbrevation of the unit such as k, psi, f, etc...
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// The <see cref="Specification"/>'s that utilize this unit of measure.
        /// </summary>
        public virtual ICollection<Specification> Specs { get; set; }
    }
}

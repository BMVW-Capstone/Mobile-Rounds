using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    public class ComparisonType
    {
        public const string LessThan = "Less Than";
        public const string LessThanOrEqual = "Less Than Or Equal To";
        public const string GreaterThan = "Greater Than";
        public const string GreaterThanOrEqual = "Greater Than Or Equal To";
        public const string EqualTo = "Equal To";
        public const string Between = "Between";
        public const string Either = "Either";
        public const string NotApplicable = "Not Applicable";

        /// <summary>
        /// The name of the comparison. This would be like 
        /// Greater Than, Less Than, etc...
        /// </summary>
        [Key]
        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}

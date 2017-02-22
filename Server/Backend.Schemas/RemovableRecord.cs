using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Schemas
{
    /// <summary>
    /// A record type that is removable, but still technically saved.
    /// </summary>
    public abstract class RemovableRecord
    {
        /// <summary>
        /// Marks a record as not deleted by default.
        /// </summary>
        public RemovableRecord()
        {
            IsMarkedAsDeleted = false;
        }

        /// <summary>
        /// Determines if the record was deleted. This is so we can 
        /// continue to keep all data even if they delete it.
        /// </summary>
        public bool IsMarkedAsDeleted { get; set; }
    }
}

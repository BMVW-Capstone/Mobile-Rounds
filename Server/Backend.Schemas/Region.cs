﻿using System;
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
        [Key]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}

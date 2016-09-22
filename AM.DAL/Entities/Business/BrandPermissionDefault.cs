﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class BrandPermissionDefault
    {
        [Key, Column(Order = 0)]
        public string BrandCode { get; set; }
        [Key, Column(Order = 1)]
        public string ResourceName { get; set; }
    }
}

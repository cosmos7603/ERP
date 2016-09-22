﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class FormLetterCustomer
    {
        [Key, Column(Order = 0)]
        public int FormLetterId { get; set; }
        [Key, Column(Order = 1)]
        public int CustomerId { get; set; }
    }
}

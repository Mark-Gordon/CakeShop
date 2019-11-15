﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string UserEmail { get; set; }
        public int TotalAmount { get; set; }
        public decimal TotalCost { get; set; }
        public IEnumerable<Purchase> Purchase { get; set; }
        
    }
}

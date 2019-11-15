using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        public int CartId { get; set; }

        public Cake Cake { get; set; }

        public decimal Cost { get; set; }

        public int Amount { get; set; }

    }
}

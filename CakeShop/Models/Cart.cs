using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class Cart
    {
        [Key]
        public int ShoppingCartId { get; set; }
        public Cake Cake { get; set; }
        public int Amount { get; set; }
        public decimal TotalCost { get; set; }
        public string UserEmail { get; set; }
    }
}

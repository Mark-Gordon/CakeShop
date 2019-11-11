using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.ViewModels
{
    public class CakesListViewModel
    {
        public IEnumerable<Cake> Cakes { get; set; }
        public string CurrentCategory { get; set; }
    }
}

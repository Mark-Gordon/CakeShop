using CakeShop.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Components
{
    public class CartSummary : ViewComponent
    {

        private readonly ICartRepository _cart;

        public CartSummary(ICartRepository cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            int total = _cart.GetNumberOfItems(User.Identity.Name);        

            return View(total);
        }
    }
}

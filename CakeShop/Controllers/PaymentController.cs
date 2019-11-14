using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeShop.Models;
using CakeShop.Models.Repositories;
using CakeShop.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;

namespace CakeShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<CakeController> _logger;
        private readonly ICakeRepository _cakeRepository;
        private readonly ICartRepository _cartRepository;

        public PaymentController(ILogger<CakeController> logger, ICakeRepository cakeRepository, ICartRepository cartRepository)
        {
            _logger = logger;
            _cakeRepository = cakeRepository;
            _cartRepository = cartRepository;
        }

        //Returns view of the cart
        public IActionResult Index()
        {
            IEnumerable<Cart> cakes = _cartRepository.GetCartItems(User.Identity.Name);
            return View(cakes);
        }

        [Authorize]
        public RedirectToActionResult AddToCart(int cakeId)
        {
            var selectedCake = _cakeRepository.AllCakes.FirstOrDefault(p => p.CakeId == cakeId);

            if (selectedCake != null)
            {

                _cartRepository.AddToCart(selectedCake, User.Identity.Name);
            }

            return RedirectToAction("Index","Cake");
        }

        //Handles the purchase processing with Stripe API
        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            Dictionary<string, string> Metadata = new Dictionary<string, string>();
            //  Metadata.Add("Product", "RubberDuck");
            //  Metadata.Add("Quantity", "10");
            var options = new ChargeCreateOptions
            {
                //Amount takes a 'long' value so conversion from decimal to the appropriate long value
                Amount = (long)(_cartRepository.GetTotalCost(User.Identity.Name) * 100),
                Currency = "GBP",
                Description = "The Cake Company! user purchase.",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = Metadata
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            _cartRepository.StoreCompletedOrder(User.Identity.Name);
            //Clear cart after purchase
            _cartRepository.ClearCart(User.Identity.Name);
            
            return View();
        }

        public RedirectToActionResult DeleteFromCart(int cakeId)
        {
            _cartRepository.DeleteCartItemsById(User.Identity.Name, cakeId);


            return RedirectToAction("Index", "Payment");
        }

        public RedirectToActionResult UpdateNumberOfItemsInCart(int cakeId, int amount)
        {


            _cartRepository.UpdateCartItemAmount(User.Identity.Name, cakeId, amount);


            return RedirectToAction("Index", "Payment");
        }




      
    }
}
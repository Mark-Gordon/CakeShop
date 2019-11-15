using CakeShop.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models.Repositories.Interfaces
{
    public class CartRepository : ICartRepository
    {

        private readonly AppDbContext _appDbContext;

        public CartRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddToCart(Cake cake, string email)
        {
            var cart =
         _appDbContext.Cart.SingleOrDefault(c=> c.UserEmail == email);
               
                cart.TotalAmount++;
                cart.TotalCost += cake.Price;

                var purchase = _appDbContext.Purchase.Where(p => p.CartId == cart.CartId && p.Cake.CakeId == cake.CakeId).SingleOrDefault();
                if(purchase == null)
                {
                    purchase = CreatePurchase(cart.CartId, cake);
                    _appDbContext.Purchase.Add(purchase);
                }
                else
                {
                    purchase.Amount += 1;
                    purchase.Cost = cake.Price * purchase.Amount;
                    _appDbContext.Purchase.Update(purchase);
                } 
            

            _appDbContext.SaveChanges();
        }

        private Purchase CreatePurchase(int cartId, Cake cake)
        {
            var purchase = new Purchase
            {
                CartId = cartId,
                Cake = cake,
                Amount = 1,
                Cost = cake.Price
            };

            return purchase;
        }

        public int GetNumberOfItems(string email)
        {
            if (_appDbContext.Cart.Where(c => c.UserEmail == email).FirstOrDefault() == null)
                CreateCart(email);

            int total = _appDbContext.Cart.Where(c => c.UserEmail == email).FirstOrDefault().TotalAmount;

            return total;
        }

        public decimal GetTotalCost(string email)
        {
            return _appDbContext.Cart.Where(c => c.UserEmail == email).FirstOrDefault().TotalCost;
        }

        public Cart GetCart(string email)
        {
            var cart = _appDbContext.Cart.Include(c => c.Purchase).ThenInclude(c=>c.Cake).Where(c => c.UserEmail == email).FirstOrDefault();
            if (cart == null)
                cart = CreateCart(email);

            return cart;

        }

        private Cart CreateCart(string email)
        {
            var cart = new Cart
            {
                UserEmail = email,
                TotalCost = 0,
                TotalAmount = 0
            };

            _appDbContext.Cart.Add(cart);
            _appDbContext.SaveChanges();

            return cart;
        }

        public void ClearCart(string email)
        {
            var cart = _appDbContext.Cart.Include(c=>c.Purchase).Where(x => x.UserEmail == email).FirstOrDefault();
            IEnumerable<Purchase> purchases = cart.Purchase;

            _appDbContext.RemoveRange(purchases);

            _appDbContext.Remove(cart);

            _appDbContext.SaveChanges();
        }

        public void DeleteCartItemsById(string email, int cakeId)
        {
            Cart cart = _appDbContext.Cart.Include(c => c.Purchase).ThenInclude(c => c.Cake).Where(x => x.UserEmail == email).FirstOrDefault();
            Purchase purchase = cart.Purchase.Where(x => x.Cake.CakeId == cakeId).FirstOrDefault();

            cart.TotalAmount -= purchase.Amount;
            cart.TotalCost -= purchase.Cost;

            _appDbContext.Purchase.Remove(purchase);
            _appDbContext.Cart.Update(cart);
            _appDbContext.SaveChanges();
        }

        public void UpdateCartItemAmount(string email, int cakeId, int amount)
        {
            if (amount == 0)
            {
                DeleteCartItemsById(email, cakeId);
                return;
            }
            Cart cart = _appDbContext.Cart.Include(c => c.Purchase).ThenInclude(c=>c.Cake).Where(x => x.UserEmail == email).FirstOrDefault();
            Purchase purchase = cart.Purchase.Where(p => p.Cake.CakeId == cakeId).FirstOrDefault();


            cart.TotalAmount = cart.TotalAmount + amount - purchase.Amount;
            cart.TotalCost = cart.TotalCost - purchase.Cost;

            purchase.Amount = amount;
            purchase.Cost = amount * purchase.Cake.Price;

            cart.TotalCost += purchase.Cost;

            _appDbContext.Update(cart);
            _appDbContext.Update(purchase);

            _appDbContext.SaveChanges();

        }

        //public void StoreCompletedOrder(string email)
        //{
        //    IEnumerable<Cart> cart = _appDbContext.Cart.Include(c => c.Cake).Where(x => x.UserEmail == email);

        //    ICollection<CompletedOrder> completedOrders = new List<CompletedOrder>();

        //    foreach(var cartItem in cart)
        //    {
        //        var order = new CompletedOrder
        //        {
        //            UserEmail = cartItem.UserEmail,
        //            Cake = cartItem.Cake,
        //            Amount = cartItem.Amount,
        //            TotalCost = cartItem.TotalCost
        //        };
        //        completedOrders.Add(order);
        //    }



        //    _appDbContext.AddRange(completedOrders);

        //    _appDbContext.SaveChanges();
        //}
    }
}

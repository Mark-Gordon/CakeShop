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
            var CartItem =
         _appDbContext.Cart.SingleOrDefault(c => c.Cake.CakeId == cake.CakeId && c.UserEmail == email);

            if (CartItem == null)
            {
                CartItem = new Cart
                {
                    UserEmail = email,
                    Cake = cake,
                    Amount = 1,
                    TotalCost = cake.Price
                };

                _appDbContext.Cart.Add(CartItem);
            }
            else
            {
                CartItem.Amount++;
                CartItem.TotalCost += cake.Price;
            }
            _appDbContext.SaveChanges();
        }

        public int GetNumberOfItems(string email)
        {
            int total = _appDbContext.Cart.Where(c => c.UserEmail == email).Sum(s => s.Amount);

            return total;
        }

        public decimal GetTotalCost(string email)
        {
            return _appDbContext.Cart.Where(c => c.UserEmail == email).Sum(s => s.TotalCost); ;
        }

        public IEnumerable<Cart> GetCartItems(string email)
        {
                return _appDbContext.Cart.Include(c => c.Cake).Where(c => c.UserEmail == email);

        }

        public void ClearCart(string email)
        {
            IEnumerable<Cart> carts = _appDbContext.Cart.Where(x => x.UserEmail == email);
            _appDbContext.RemoveRange(carts);
            _appDbContext.SaveChanges();
        }

        public void DeleteCartItemsById(string email, int cakeId)
        {
            IEnumerable<Cart> cakes = _appDbContext.Cart.Include(c=>c.Cake).Where(x => x.UserEmail == email && x.Cake.CakeId == cakeId);
            _appDbContext.RemoveRange(cakes);
            _appDbContext.SaveChanges();
        }

        public void UpdateCartItemAmount(string email, int cakeId, int amount)
        {
            if(amount == 0)
            {
                DeleteCartItemsById(email, cakeId);
                return;
            }
            Cart cart = _appDbContext.Cart.Include(c => c.Cake).Where(x => x.UserEmail == email && x.Cake.CakeId == cakeId).FirstOrDefault();

            cart.Amount = amount;
            cart.TotalCost = cart.Amount * cart.Cake.Price;

            _appDbContext.Update(cart);

            _appDbContext.SaveChanges();

        }

        public void StoreCompletedOrder(string email)
        {
            IEnumerable<Cart> cart = _appDbContext.Cart.Include(c => c.Cake).Where(x => x.UserEmail == email);

            ICollection<CompletedOrder> completedOrders = new List<CompletedOrder>();

            foreach(var cartItem in cart)
            {
                var order = new CompletedOrder
                {
                    UserEmail = cartItem.UserEmail,
                    Cake = cartItem.Cake,
                    Amount = cartItem.Amount,
                    TotalCost = cartItem.TotalCost
                };
                completedOrders.Add(order);
            }



            _appDbContext.AddRange(completedOrders);

            _appDbContext.SaveChanges();
        }
    }
}

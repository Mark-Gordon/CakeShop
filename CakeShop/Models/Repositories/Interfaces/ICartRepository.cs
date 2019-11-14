using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models.Repositories.Interfaces
{
    public interface ICartRepository
    {
        void AddToCart(Cake cake, string email);

        int GetNumberOfItems(string email);

        public decimal GetTotalCost(string email);

        public void ClearCart(string email);

        IEnumerable<Cart> GetCartItems(string email);

        public void DeleteCartItemsById(string email, int cakeId);

        public void UpdateCartItemAmount(string email, int cakeId, int amount);

        public void StoreCompletedOrder(string email);
    }
}

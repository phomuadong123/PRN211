using BusinessObject.Models ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.CartRepo
{
    public interface ICartRepository
    {
        public Dictionary<int, Cart> GetCart();
        public void AddToCart(int productId, int quantity, decimal price);
        public void RemoveFromCart(int productId);
        public void UpdateCart(int productId, int quantity, decimal price);
        public void DeleteCart();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruYumOrderApiWithAzure.Data_Layer;
using TruYumOrderApiWithAzure.Models;

namespace TruYumOrderApiWithAzure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderContext _orderContext { get; set; }
        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }
        public Cart AddToCart(Cart cart)
        {
            _orderContext.Carts.Add(cart);
            _orderContext.SaveChanges();
            return cart;
        }

        public List<Cart> ShowCart()
        {
            return _orderContext.Carts.ToList();
        }
    }
}

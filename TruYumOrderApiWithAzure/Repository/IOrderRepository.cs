using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruYumOrderApiWithAzure.Models;

namespace TruYumOrderApiWithAzure.Repository
{
    public interface IOrderRepository
    {
         Cart AddToCart(Cart cart);
         List<Cart> ShowCart();
    }
}

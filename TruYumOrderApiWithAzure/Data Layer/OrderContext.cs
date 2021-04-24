using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruYumOrderApiWithAzure.Models;

namespace TruYumOrderApiWithAzure.Data_Layer
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }
        public DbSet<Cart> Carts { get; set; }
    }
}

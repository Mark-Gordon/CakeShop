using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Cake> Cakes { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<CompletedOrder> CompletedOrder { get; set; }
    }
}

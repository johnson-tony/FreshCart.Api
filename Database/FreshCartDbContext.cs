using FreshCard.Api.Models;
using FreshCart.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FreshCart.Api.Database
{
    public class FreshCartDbContext:DbContext
    {
        public FreshCartDbContext(DbContextOptions<FreshCartDbContext> options) : base(options)
        {
        }
        [Key]
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PlaceOrder> PlaceOrders { get; set; }

        public DbSet<AddToCart> AddToCarts { get; set;}
        public DbSet<CustomerRegister> CustomerRegister { get; set; }

        public DbSet<admin> Admin {  get; set; }

        
    }
}

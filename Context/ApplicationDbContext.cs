using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Shop_Final_Project_ITStep.Models.Entities;

namespace Online_Shop_Final_Project_ITStep.Context
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<FavouriteProducts> FavouriteProducts { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ProductCategories> Productcategories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<UserBalances> Userbalances { get; set; }
        public DbSet<Users> User { get; set; }
    }
}

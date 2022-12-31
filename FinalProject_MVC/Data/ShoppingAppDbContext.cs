using FinalProject_MVC.Models;
using Microsoft.EntityFrameworkCore;


namespace FinalProject_MVC.Data
{
    public class ShoppingAppDbContext : DbContext
    {
		public ShoppingAppDbContext()
		{
		}

		public ShoppingAppDbContext(DbContextOptions<ShoppingAppDbContext> options) : base(options) 
        {
            
        
        } 
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<ShoppingListDetail> ShoppingListDetail { get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}

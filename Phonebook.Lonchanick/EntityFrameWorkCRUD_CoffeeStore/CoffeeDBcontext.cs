using Microsoft.EntityFrameworkCore;
using PlayingSpectre.Models;

namespace PlayingSpectre;

internal class CoffeeDBcontext : DbContext
{
	public DbSet<Product> Coffees { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderProd> OrderProds { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	=>
		optionsBuilder.UseSqlite($"Data Source =  CoffeStore.db");
}

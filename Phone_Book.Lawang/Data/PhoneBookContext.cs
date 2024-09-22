using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phone_Book.Lawang.Models;

namespace Phone_Book.Lawang;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories {get; set;}
    private  string?  _connectionString { get; set; }
    public void SetConnectionString(string connectionString)
    {
        _connectionString = connectionString;
    } 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
        .AddUserSecrets<PhoneBookContext>().Build();
        optionsBuilder.UseSqlServer(config["ConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category() {Id = 1, CategoryName = "Family"},
            new Category() {Id = 2, CategoryName = "Work"},
            new Category() {Id = 3, CategoryName = "Friend"}
        );
    }
}

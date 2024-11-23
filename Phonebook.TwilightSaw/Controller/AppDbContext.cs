using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TwilightSaw.Phonebook.Model;

namespace TwilightSaw.Phonebook.Controller;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Category> Categories { get; set; }

    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.None);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>();
        modelBuilder.Entity<Category>().HasMany(u => u.contacts).WithMany(u => u.categories)
            .UsingEntity(j => j.ToTable("ContactCategories"));
    }
}
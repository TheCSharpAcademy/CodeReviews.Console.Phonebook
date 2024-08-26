using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess;
public class MyDbContext : DbContext
{
    public DbSet<Contact> contacts {  get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=PhoneBook;Trusted_Connection=True; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasKey(x => x.ID);
        modelBuilder.Entity<Contact>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Contact>().Property(x => x.Email).IsRequired();
        modelBuilder.Entity<Contact>().Property(x => x.PhoneNumber).IsRequired().HasMaxLength(11);
    }
}

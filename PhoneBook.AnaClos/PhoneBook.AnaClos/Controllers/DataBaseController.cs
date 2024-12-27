using Microsoft.EntityFrameworkCore;
using PhoneBook.AnaClos.Models;

using Microsoft.Extensions.Configuration;

namespace PhoneBook.AnaClos.Controllers;

public class DataBaseController : DbContext
{
    private string _connectionString;
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }


    public DataBaseController(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataBaseController()
    {
        _connectionString = "Server=DESKTOP-H645C4H\\SQLEXPRESS;Database=PhoneBook;Trusted_Connection=True;TrustServerCertificate=True;";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("Category");
        modelBuilder.Entity<Contact>().ToTable("Contact");
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.IdCategory)
            .OnDelete(DeleteBehavior.Restrict); // Configuración de la restricción

        modelBuilder.Entity<Category>()
        .HasIndex(c => c.Name)
        .IsUnique(); // Configuración del índice único

        //modelBuilder.Entity<Contact>()
        //.HasOne(c => c.Category)
        //.WithMany(c => c.Contacts)
        //.HasForeignKey(c => c.IdCategory)
        //.OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<Contact>()
        //.HasIndex(c => c.Name)
        //.IsUnique();



        //base.OnModelCreating(modelBuilder);
    }
}
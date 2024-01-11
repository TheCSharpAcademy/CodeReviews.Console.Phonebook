using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace PhoneBookProgram;
public class PhoneBookContext : DbContext
{
    public static readonly string? connectionString = 
        ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; // switch to json
    public DbSet<Contact> Contacts {get; set;}
    public DbSet<Email> Emails {get; set;} 
    public DbSet<PhoneNumber> PhoneNumbers {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .Property(p => p.ContactName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Contact>()
            .HasIndex(p => p.ContactName)
            .IsUnique(true);

        modelBuilder.Entity<Contact>()
            .Property(p => p.Category)
            .HasMaxLength(50)
            .IsUnicode(true);

        modelBuilder.Entity<Contact>()
            .OwnsMany(p => p.Emails)
            .Property(p => p.LocalName)
            .HasMaxLength(64)
            .IsUnicode(false)
            .IsRequired(true);

        modelBuilder.Entity<Contact>()
            .OwnsMany(p => p.Emails)
            .Property(p => p.DomainName)
            .HasMaxLength(254)
            .IsUnicode(false)
            .IsRequired(true);

        modelBuilder.Entity<Contact>()
            .OwnsMany(p => p.PhoneNumbers)
            .Property(p => p.CountryCode)
            .HasMaxLength(3)
            .IsUnicode(false)
            .IsRequired(true);

        modelBuilder.Entity<Contact>()
            .OwnsMany(p => p.PhoneNumbers)
            .Property(p => p.LocalNumber)
            .HasMaxLength(15)
            .IsUnicode(false)
            .IsRequired(true);

        // modelBuilder.Entity<Contact>()
        //     .Navigation(p => p.Emails)
        //     .AutoInclude(false);
        
        // modelBuilder.Entity<Contact>()
        //     .Navigation(p => p.PhoneNumbers)
        //     .AutoInclude(false);
    }
}
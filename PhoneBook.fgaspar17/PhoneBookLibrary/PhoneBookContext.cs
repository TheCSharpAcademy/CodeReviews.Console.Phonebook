using Microsoft.EntityFrameworkCore;

namespace PhoneBookLibrary;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ContactCategory> ContactsCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\phonebook;Database=PhoneBookDB;Integrated Security=True;");
    }

    // UNIQUE Name
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Contact>()
            .HasIndex(c => c.Name)
            .IsUnique();

        builder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        builder.Entity<ContactCategory>()
            .HasKey(cc => new { cc.ContactId, cc.CategoryId });
    }
}
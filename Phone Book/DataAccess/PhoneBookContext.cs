using System.Configuration;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Email>()
            .HasOne(e => e.Contact)
            .WithMany(c => c.EmailAddresses)
            .HasForeignKey(e => e.ContactId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PhoneNumber>()
            .HasOne(p => p.Contact)
            .WithMany(c => c.PhoneNumbers)
            .HasForeignKey(p => p.ContactId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = 1, Name = "Adam Smith" },
            new Contact { Id = 2, Name = "Beth Johnson" },
            new Contact { Id = 3, Name = "Chris Brown" },
            new Contact { Id = 4, Name = "Dave Wilson" },
            new Contact { Id = 5, Name = "Emma Taylor" }
        );

        modelBuilder.Entity<Email>().HasData(
             new Email { Id = 1, ContactId = 1, EmailAddress = "adam.smith@email.com" },
             new Email { Id = 2, ContactId = 2, EmailAddress = "beth.johnson@email.com" },
             new Email { Id = 3, ContactId = 3, EmailAddress = "chris.brown@email.com" },
             new Email { Id = 4, ContactId = 4, EmailAddress = "dave.wilson@email.com" },
             new Email { Id = 5, ContactId = 5, EmailAddress = "emma.taylor@email.com" }
        );

        modelBuilder.Entity<PhoneNumber>().HasData(
             new PhoneNumber { Id = 1, ContactId = 1, Number = "07956049455" },
             new PhoneNumber { Id = 2, ContactId = 2, Number = "07123456789" },
             new PhoneNumber { Id = 3, ContactId = 3, Number = "07234567890" },
             new PhoneNumber { Id = 4, ContactId = 4, Number = "07345678901" },
             new PhoneNumber { Id = 5, ContactId = 5, Number = "07456789012" }
        );
    }
}

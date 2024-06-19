using System.Configuration;
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
    }
}

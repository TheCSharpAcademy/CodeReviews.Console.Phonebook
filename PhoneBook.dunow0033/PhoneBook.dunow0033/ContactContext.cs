using Microsoft.EntityFrameworkCore;

namespace PhoneBook.dunow0033;

internal class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Integrated Security=true;Database=Phonebook;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}

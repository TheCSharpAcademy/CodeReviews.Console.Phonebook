using Microsoft.EntityFrameworkCore;

namespace PhoneBook.UgniusFalze;

public class ContactsContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(LocalDb)\\PhoneBook;Initial Catalog=PhoneBook;Integrated Security=SSPI;Trusted_Connection=yes;AttachDBFilename=C:\\Users\\ugniu\\source\\repos\\CodeReviews.Console.Phonebook\\PhoneBook.UgniusFalze\\PhoneBook.UgniusFalze\\PhoneBook.mdf");
    }
}
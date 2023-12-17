using Microsoft.EntityFrameworkCore;
using PhoneBook.UgniusFalze.Models;

namespace PhoneBook.UgniusFalze.Utils;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    private readonly string SqlServerConnectionString =
        "Server=(LocalDb)\\PhoneBook;Initial Catalog=PhoneBook;Integrated Security=SSPI;Trusted_Connection=yes";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(SqlServerConnectionString);
    }
}
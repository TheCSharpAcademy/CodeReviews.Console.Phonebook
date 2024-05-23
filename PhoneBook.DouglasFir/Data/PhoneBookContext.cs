using Microsoft.EntityFrameworkCore;
using PhoneBook.DouglasFir.Models;

namespace PhoneBook.DouglasFir.Data;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;");
    }
}

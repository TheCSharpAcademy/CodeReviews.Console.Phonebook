using Microsoft.EntityFrameworkCore;
using PhoneBookCarDioLogics.Models;

namespace PhoneBookCarDioLogics;

internal class PhonebookAppDbContext: DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PhonebookDatabase;Trusted_Connection=True;");
    }
}

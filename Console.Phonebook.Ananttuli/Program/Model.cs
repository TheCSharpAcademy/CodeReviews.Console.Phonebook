using Microsoft.EntityFrameworkCore;
using Program.Categories;
using Program.Contacts;
using Program.ContactsCategories;
using Program.Database;

namespace Program;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ContactCategory> ContactCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ConnectionManager.Init();
        optionsBuilder.UseSqlServer(
            ConnectionManager.GetConnectionString(ConfigManager.Database["Name"])
        );
    }
}


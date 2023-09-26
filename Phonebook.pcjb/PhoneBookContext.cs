namespace PhoneBook;

using Microsoft.EntityFrameworkCore;

class PhoneBookContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = Configuration.GetInstance();
        optionsBuilder.UseSqlServer(config.DatabaseConnectionString);
    }
}
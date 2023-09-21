namespace PhoneBook;

using Microsoft.EntityFrameworkCore;

class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    private readonly string? databaseConnectionString;

    public PhoneBookContext(string? databaseConnectionString) : base()
    {
        this.databaseConnectionString = databaseConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(databaseConnectionString);
    }
}
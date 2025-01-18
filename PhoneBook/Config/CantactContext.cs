using Microsoft.EntityFrameworkCore;

internal class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Config.ConnectionString);
    }
}

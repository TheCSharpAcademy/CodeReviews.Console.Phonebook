using Microsoft.EntityFrameworkCore;

namespace PhoneBook;
internal class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=DatabasePhonebook;Integrated Security=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}


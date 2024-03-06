using Microsoft.EntityFrameworkCore;

namespace PhoneBook_CRUD
{
    internal class ContactsContext : DbContext
    {
        public DbSet<Contacts> Contacts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source = contacts.db");

    }
}

using Microsoft.EntityFrameworkCore;
namespace Phonebook.kjanos89
{
    public class PhonebookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PhonebookDB;Trusted_Connection=True;");
        }
        public void Initialize()
        {
            this.Database.EnsureCreated();
        }
    }
}

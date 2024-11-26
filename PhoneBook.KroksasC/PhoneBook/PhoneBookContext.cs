using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook
{
    internal class PhoneBookContext : DbContext
    {
        public DbSet<Contact> Contacts {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PhoneBookDB;Integrated Security=True;TrustServerCertificate=True;");

        public static void InitializeDatabase()
        {
            using var context = new PhoneBookContext();
            context.Database.EnsureCreated();
        }
    }

}

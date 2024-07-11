using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhoneBook.Models
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Retrieve the connection string
            var connectionString = ConfigurationManager.ConnectionStrings["ContactContext"].ConnectionString;

            // Set the connection string
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
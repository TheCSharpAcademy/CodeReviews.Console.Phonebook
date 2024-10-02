using PhoneBook.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PhoneBook
{
    public class PhonebookContext: DbContext
    {
        public DbSet<Contact>? Contact { get; set; }

        private string? connectionString = ConfigurationManager.AppSettings.Get("PhonebookDBConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");
            optionsBuilder.UseSqlServer(configuration);
        }
    }
}


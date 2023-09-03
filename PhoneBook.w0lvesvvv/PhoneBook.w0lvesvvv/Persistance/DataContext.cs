using Microsoft.EntityFrameworkCore;
using PhoneBook.w0lvesvvv.Models;
using System.Configuration;

namespace PhoneBook.w0lvesvvv.Persistance
{
    public class DataContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["sqlConnectionString"]);
        }
    }
}

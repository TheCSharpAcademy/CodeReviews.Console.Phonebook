using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sadklouds.PhoneBook.Models;

namespace sadklouds.PhoneBook.DataAccess
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
        }
    }
}

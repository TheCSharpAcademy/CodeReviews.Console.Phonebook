using PhoneBook.maccer989.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PhoneBook.maccer989.DataAccess
{
    public class ContactContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            var config = builder.Build();
            options.UseSqlServer(config.GetConnectionString("Default"));
        }
    }
}

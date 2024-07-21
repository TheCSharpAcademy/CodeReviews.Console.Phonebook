using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
namespace Phonebook.kjanos89
{
    public class PhonebookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _connectionString = "Server=localhost;Database=PhonebookDB;Integrated Security=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

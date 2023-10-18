using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Data
{
    public class PhoneBookDbContext : DbContext
    {

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer("Server=.;Database=PhoneBookDb;Trusted_Connection=True;TrustServerCertificate=True;");


    }
}

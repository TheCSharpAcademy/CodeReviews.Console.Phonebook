using Microsoft.EntityFrameworkCore;
using PhoneBook.Model;

namespace PhoneBook.Data
{
    public class DataService : DbContext
    {
        public DataService() { }
        public DataService(DbContextOptions<DataService> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PhoneBooks;Integrated Security=true");
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}

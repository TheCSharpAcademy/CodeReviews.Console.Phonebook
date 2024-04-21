using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PhobeBook.Kakurokan
{
    public class DataAccess : DbContext
    {
        public DataAccess()
        {
        }

        public DbSet<ContactModel> Contact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ContactsDatabase"].ConnectionString);
        }
    }
}

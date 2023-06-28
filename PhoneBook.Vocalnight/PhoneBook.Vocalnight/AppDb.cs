using Microsoft.EntityFrameworkCore;

namespace PhoneBook
{
    public class AppDb : DbContext
    {
        public DbSet<PhoneBook> PhoneBook { get; set; }

        private const string ConnectionString =
            @"Server=(localdb)\mssqllocaldb;
            Database=PhoneBookDatabase;
            Trusted_Connection=True";

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }

}

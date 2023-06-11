using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

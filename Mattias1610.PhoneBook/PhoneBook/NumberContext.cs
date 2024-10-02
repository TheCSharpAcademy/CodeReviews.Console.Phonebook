using Microsoft.EntityFrameworkCore;

namespace PhoneBook
{
    public class NumberContext : DbContext
    {
     public DbSet<Number> Numbers{get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=phonebook.db");
        }
    }
}
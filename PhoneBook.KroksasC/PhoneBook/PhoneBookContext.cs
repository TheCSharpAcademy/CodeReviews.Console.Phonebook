using Microsoft.EntityFrameworkCore;
using PhoneBook.Helpers;
using PhoneBook.Models;


namespace PhoneBook
{
    internal class PhoneBookContext : DbContext
    {
        public DbSet<Contact> Contacts {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(DataAccesHelper.CnnVal("Phonebook"));

        public static void InitializeDatabase()
        {
            using var context = new PhoneBookContext();
            context.Database.EnsureCreated();
        }
    }

}

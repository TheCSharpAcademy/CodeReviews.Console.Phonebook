using Microsoft.EntityFrameworkCore;
using PhoneBook.Arashi256.Config;
using PhoneBook.Arashi256.Models;

namespace PhoneBook.Arashi256.Data
{
    internal class DataContext : DbContext
    {
        private DatabaseConnection _connection;

        public DataContext()
        {
            _connection = new DatabaseConnection();         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CategoryId);

            // Seed initial Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Friends" },
                new Category { Id = 2, Name = "Work" },
                new Category { Id = 3, Name = "Family" }
            );
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

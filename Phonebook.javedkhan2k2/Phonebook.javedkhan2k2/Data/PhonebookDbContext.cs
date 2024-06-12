
using Microsoft.EntityFrameworkCore;
using Phonebook.Entities;

namespace Phonebook.Data;

public class PhonebookDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Phonebook; User Id=SA; Password=ghost@123;TrustServerCertificate=True");
    }

}
using PhoneBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PhoneBook;

internal class PhonebookContext : DbContext
{
    private static readonly IConfigurationRoot Config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddUserSecrets<Program>()
        .Build();
    
    
    
    public DbSet<Contact> Contacts { get; set; }
    
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(Config.GetConnectionString("PhonebookDatabase"));

}
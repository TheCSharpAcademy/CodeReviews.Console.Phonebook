using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Phonebook.K_MYR.Models;

internal class ContactsContext : DbContext
{
    private readonly string _connectionString = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder
    {
        DataSource = ConfigurationManager.AppSettings.Get("DataSource"),
        InitialCatalog = ConfigurationManager.AppSettings.Get("DatabaseName"),
        UserID = ConfigurationManager.AppSettings.Get("UserName"),
        Password = ConfigurationManager.AppSettings.Get("Password")
    }.ConnectionString;
    
    public DbSet<Contact> Contacts {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);
}

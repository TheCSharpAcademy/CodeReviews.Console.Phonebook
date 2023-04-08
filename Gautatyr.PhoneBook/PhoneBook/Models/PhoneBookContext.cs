using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using System.Configuration;

namespace PhoneBook;

public class PhoneBookContext : DbContext
{
    string dbString = ConfigurationManager.AppSettings.Get("ConnectionString");

    public DbSet<Contact> Contacts { get; set; }

    public string DbPath { get; }

    public PhoneBookContext()
    {
        DbPath = ConfigurationManager.AppSettings.Get("DbFilePath");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(dbString);
}
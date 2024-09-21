using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Phonebook.tonyissa.Context;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public string DbPath { get; } = ConfigurationManager.AppSettings.Get("ConnectionString")!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(DbPath);
}
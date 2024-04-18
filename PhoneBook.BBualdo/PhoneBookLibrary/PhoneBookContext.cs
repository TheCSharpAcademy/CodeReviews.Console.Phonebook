using Microsoft.EntityFrameworkCore;
using PhoneBookLibrary.Models;

namespace PhoneBookLibrary;

public class PhoneBookContext : DbContext
{
  public DbSet<Group> Groups { get; set; }
  public DbSet<Contact> Contacts { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString"));
}
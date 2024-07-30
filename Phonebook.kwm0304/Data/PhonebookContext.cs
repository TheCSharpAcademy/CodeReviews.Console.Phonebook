using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phonebook.kwm0304.Models;

namespace Phonebook.kwm0304.Data;
public class PhonebookContext : DbContext
{
  public DbSet<Contact> Contacts { get; set; }
  public DbSet<ContactGroup> ContactGroups { get; set; }

  private readonly IConfiguration _configuration;

  public PhonebookContext(DbContextOptions<PhonebookContext> options, IConfiguration configuration)
      : base(options)
  {
    _configuration = configuration;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Contact>()
        .HasKey(c => c.ContactId);

    modelBuilder.Entity<ContactGroup>()
        .HasKey(g => g.GroupName);
  }
}
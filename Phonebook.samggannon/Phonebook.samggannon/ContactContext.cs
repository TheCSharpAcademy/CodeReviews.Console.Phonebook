using Microsoft.EntityFrameworkCore;
using Phonebook.samggannon.Models;
using System.Configuration;

namespace Phonebook.samggannon;

internal class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString);
    }

    //// This also doesn't work
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString);

    //// Neither Does this
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    //.UseSqlServer(ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString);
    

}

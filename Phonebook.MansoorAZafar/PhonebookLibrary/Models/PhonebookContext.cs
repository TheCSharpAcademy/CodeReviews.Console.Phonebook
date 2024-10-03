using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PhonebookLibrary.Models;

internal class PhonebookContext: DbContext
{ 
    public DbSet<Contact> Contacts { get; set; }

    //Migrations are just a way to manage changes in a database over time
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", false, false)
                                    .Build()["DatabaseConnection"]);
}

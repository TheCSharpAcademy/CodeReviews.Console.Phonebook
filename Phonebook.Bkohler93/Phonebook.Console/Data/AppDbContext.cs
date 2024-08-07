using Microsoft.EntityFrameworkCore;
using Phonebook.Console.Config;
using Phonebook.Console.Models;

namespace Phonebook.Console.Data;

public class AppDbContext : DbContext {
    public DbSet<Contact> Contacts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ 
        var config = new AppConfig();     
        optionsBuilder.UseSqlServer(config.GetConnectionString());
    } 
}
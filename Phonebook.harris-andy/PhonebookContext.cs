using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Phonebook;

internal class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            var _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            string connectionString = _config.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string not found. Check appsettings.json");

            optionsBuilder.UseSqlServer(connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"THIS IS THE ERROR MESSAGE {ex.Message}");
        }
    }
}
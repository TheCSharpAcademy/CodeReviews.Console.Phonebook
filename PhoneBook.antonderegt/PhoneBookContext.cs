using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PhoneBook;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public string ConnectionString { get; }

    public PhoneBookContext()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        ConnectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"] ?? "Connectionstring not found";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(ConnectionString);
}

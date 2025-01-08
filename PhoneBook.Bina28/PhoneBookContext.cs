namespace PhoneBook.Bina28;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

internal class PhoneBookContext : DbContext
{
	string _connectionString = string.Empty;

	public PhoneBookContext()
	{
		var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", false, true)
			.Build();

		_connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");

	}

	public DbSet<PhoneBook> PhoneBooks { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_connectionString);
	}


}

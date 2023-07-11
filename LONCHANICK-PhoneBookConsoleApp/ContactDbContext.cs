using Microsoft.EntityFrameworkCore;

namespace LONCHANICK_PhoneBookConsoleApp;

public class ContactDbContext : DbContext
{
	/*public ContactDbContext(DbContextOptions options) : base(options)
	{ }*/
	public DbSet<Contact> Contacts { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
		optionsBuilder.UseSqlServer($"Server=localhost\\MSSQLSERVER01;Database=PhoneBook; Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True;");
	//"Server=localhost\\MSSQLSERVER01;Database=BethanysPieShop0123456; Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True;"

}

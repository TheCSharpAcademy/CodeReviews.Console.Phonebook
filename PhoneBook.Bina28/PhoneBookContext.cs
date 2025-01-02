namespace PhoneBook.Bina28;

using Microsoft.EntityFrameworkCore;

internal class PhoneBookContext : DbContext
{
	public DbSet<PhoneBook> PhoneBooks { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>

		optionsBuilder.UseSqlServer($"Server=(localdb)\\LocalDBDemo;Database=DB;Trusted_Connection=True;");
}

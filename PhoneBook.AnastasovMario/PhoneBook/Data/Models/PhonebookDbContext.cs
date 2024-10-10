using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Configurations;

namespace PhoneBook.Data.Models
{
	public class PhonebookDbContext() : DbContext()
	{
		private string _connectionString = "Server=localhost;Database=PhoneBookDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
		public DbSet<Contact> Contacts { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new ContactConfiguration());
			base.OnModelCreating(builder);
		}
	}
}

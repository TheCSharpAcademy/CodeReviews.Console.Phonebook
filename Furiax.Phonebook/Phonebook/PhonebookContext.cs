using Microsoft.EntityFrameworkCore;
using Phonebook.Model;

namespace Phonebook;

internal class PhonebookContext : DbContext
{
	public DbSet<Contact>Contacts { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;AttachDbFileName=C:\Users\xt3rm\Documents\Carl\programming\c sharp academy\Phonebook\Furiax.Phonebook\Phonebook.mdf;Database=Phonebook;Trusted_Connection=True;");
	}
}

using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Phonebook;

internal class Context : DbContext
{
    public Context()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Options.ConnectionString);
    }

    public DbSet<Contact> Contacts { get; set; }
}
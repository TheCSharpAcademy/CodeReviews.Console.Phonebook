using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Phonebook.ukpagrace.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Category { get; set; }

        private readonly string? _databaseName;
        private readonly string? _server;

        public ApplicationContext()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _databaseName = config.GetSection("Database")["DatabaseName"];
            _server = config.GetSection("Database")["Server"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
                optionsBuilder.UseSqlServer($"Data Source={_server};Initial Catalog={_databaseName};Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Contacts)
                .HasForeignKey(e => e.CategoryID);
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;
    }
}

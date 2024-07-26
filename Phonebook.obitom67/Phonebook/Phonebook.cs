using Microsoft.EntityFrameworkCore;

namespace Phonebook
{
    
        public class PhonebookContext : DbContext
        {
            public DbSet<Contact> Contacts { get; set; }
            
            public string DbPath { get; }

            public PhonebookContext()
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                DbPath = System.IO.Path.Join(path, "Phonebook.db");
            }

            // The following configures EF to create a Sqlite database file in the
            // special "local" folder for your platform.
            protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlServer($"Data Source=(localdb)\\LocalDBDemo");
        }
    

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

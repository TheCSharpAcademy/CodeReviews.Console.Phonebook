using System.Data.Entity;

namespace PhoneBook
{
    public class DataContext : DbContext
    {

        public DataContext(string connectionString) : base(connectionString)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserPermission> UserPermissions { get; set; }
    }
}
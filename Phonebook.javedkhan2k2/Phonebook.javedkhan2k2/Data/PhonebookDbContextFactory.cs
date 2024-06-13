using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Phonebook.Data
{
    public class PhonebookDbContextFactory : IDesignTimeDbContextFactory<PhonebookDbContext>
    {
        public PhonebookDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<PhonebookDbContextFactory>()
                .AddEnvironmentVariables()
                .Build();

            var databaseUserId = configuration["DatabaseUserID"];
            var databasePassword = configuration["DatabasePassword"];

            var optionsBuilder = new DbContextOptionsBuilder<PhonebookDbContext>();
            optionsBuilder.UseSqlServer(@$"Server=localhost,1433;Initial Catalog=Phonebook;User Id={databaseUserId};Password={databasePassword};TrustServerCertificate=True");

            return new PhonebookDbContext(databaseUserId, databasePassword);
        }
    }
}

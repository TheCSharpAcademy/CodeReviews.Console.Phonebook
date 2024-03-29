using Microsoft.EntityFrameworkCore;
namespace Phonebook.JaegerByte
{
    internal class PhonebookDataContext :DbContext
    {
        public DbSet<DataModel> TblPhonebook { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionString"));
        }
    }
}

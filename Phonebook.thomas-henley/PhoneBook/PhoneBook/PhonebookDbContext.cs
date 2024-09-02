using Microsoft.EntityFrameworkCore;

namespace PhoneBook;

public class PhonebookDbContext(string cs) : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(cs);
    }

    public void PopulateExampleData()
    {
        Contacts.Add(new Contact() 
        {
            Name = "Sheriff Woody",
            Email = "woody@snakeinmyboot.com",
            Phone = "0123456789",
        });

        Contacts.Add(new Contact()
        {
            Name = "Buzz Lightyear",
            Email = "buzz@starcommand.net",
            Phone = "0987654321",
        });

        Contacts.Add(new Contact()
        {
            Name = "T. Rex",
            Email = "rex@andysroom.edu",
            Phone = "1231231231",
        });

        Contacts.Add(new Contact()
        {
            Name = "Bo Peep",
            Email = "bo@thepasture.com",
            Phone = "4567809876",
        });

        SaveChanges();
    }
}
using hasona23.PhoneBook.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace hasona23.PhoneBook.Database
{
    internal class PhoneBookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PhoneBookDB;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasKey("ID");
        }
        public void EnsureDatabaseCreated()
        {

            Database.EnsureCreated();
        }

        public void UpdateContact(Contact oldContact, Contact newContact)
        {
            var existingContact = Contacts.Find(oldContact.ID);
            if (existingContact != null)
            {
                // Update the properties
                if (!string.IsNullOrEmpty(newContact.Name))
                    existingContact.Name = newContact.Name;

                if (!string.IsNullOrEmpty(newContact.Phone))
                    existingContact.Phone = newContact.Phone;

                if (!string.IsNullOrEmpty(newContact.Email))
                    existingContact.Email = newContact.Email;

                // Save changes to the database
                SaveChanges();
            }
            else
            {
                Console.WriteLine("Contact not found!");
            }
        }
        public List<Contact> GetAllContacts()
        {
            return Contacts.ToList();
        }
        public List<Contact> GetContacts(Contact filterContact)
        {
            var query = Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(filterContact.Name))
            {
                query = query.Where(c => c.Name.Contains(filterContact.Name));
            }
            if (!string.IsNullOrEmpty(filterContact.Phone))
            {
                query = query.Where(c => c.Phone.Contains(filterContact.Phone));
            }

            if (!string.IsNullOrEmpty(filterContact.Email))
            {
                query = query.Where(c => c.Email.Contains(filterContact.Email));
            }

            
            return query.ToList();
        }

        public void DeleteContact(Contact contact)
        {
            Contacts.Remove(contact);
            SaveChanges();
        }
        public void AddContact(Contact contact)
        {
            foreach (var cont in Contacts)
            {
                if (cont.Name == contact.Name && cont.Phone == contact.Phone && cont.Email == contact.Email)
                {
                    Console.WriteLine($"Contact: {contact} already exist");
                    return;
                }
            }


            Contacts.Add(contact);
            SaveChanges();
        }
    }
}

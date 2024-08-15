using PhoneBook.Arashi256.Models;
using PhoneBook.Arashi256.Data;
using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Arashi256.Controllers
{
    internal class ContactController
    {
        private readonly DataContext _db = new DataContext();
        private CategoryController _categoryController;

        public ContactController(CategoryController cc)
        {
            _categoryController = cc; 
            if (_categoryController == null) _categoryController = new CategoryController();
        }

        public bool AddContact(ContactDto c)
        {
            Contact contact = new Contact()
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            };
            try
            {
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: There was a problem adding contact: '{ex.Message}'");
                return false;
            }
        }

        public List<ContactDto> GetAllContacts()
        {
            List<ContactDto> displayContacts = new List<ContactDto>();
            List<Contact> contacts = _db.Contacts
                                        .Include(c => c.Category) 
                                        .OrderBy(c => c.Category.Name)
                                        .ThenBy(c => c.Name)
                                        .ToList();
            for (int i = 0; i < contacts.Count; i++) 
            {
                displayContacts.Add(new ContactDto() { 
                                            Id = contacts[i].Id, 
                                            DisplayId = i + 1, 
                                            CategoryId = contacts[i].CategoryId,
                                            CategoryName = _categoryController.GetCategoryFromId(contacts[i].CategoryId).Name,
                                            Title = contacts[i].Title, 
                                            Name = contacts[i].Name, 
                                            PhoneNumber = contacts[i].PhoneNumber, 
                                            Email = contacts[i].Email });
            }
            return displayContacts;
        }

        public bool DeleteContact(ContactDto c)
        {
            try
            {
                var contact = _db.Contacts.SingleOrDefault(x => x.Id == c.Id);
                if (contact == null) return false;
                _db.Contacts.Remove(contact);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: There was a problem deleting contact: '{ex.Message}'");
                return false;
            }
        }

        public bool UpdateContact(ContactDto c)
        {
            try
            {
                var contact = _db.Contacts.SingleOrDefault(x => x.Id == c.Id);
                if (contact == null) return false;
                contact.CategoryId = c.CategoryId;
                contact.Title = c.Title;
                contact.Name = c.Name;
                contact.PhoneNumber = c.PhoneNumber;
                contact.Email = c.Email;
                _db.Update(contact);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: There was a problem updating contact: '{ex.Message}'");
                return false;
            }
        }
    }
}

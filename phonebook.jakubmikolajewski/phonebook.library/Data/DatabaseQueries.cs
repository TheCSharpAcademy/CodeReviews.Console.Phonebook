using Microsoft.EntityFrameworkCore;
using Phonebook.Library.Helpers;
using Phonebook.Library.Models;

namespace Phonebook.Library.Data;

public class DatabaseQueries : Singleton<DatabaseQueries>
{
    public void InsertContact()
    {
        using (var context = new PhonebookContext())
        {
            bool createUser = true;
            context.Database.EnsureCreated();
            List<Contact> contactList = [];

            while (createUser)
            {
                Console.Clear();
                Contact contact = new ();
                contactList.Add(UserInput.ChooseContactProperties(contact));
                createUser = UserInput.PerformAgain("add");
            }

            context.Contacts.AddRange(contactList);            
            context.SaveChanges();
        }
    }

    public void DeleteContact()
    {
        using (var context = new PhonebookContext())
        {
            bool deleteUser = true;
      
            while (deleteUser)
            {
                Console.Clear();
                context.Contacts
                    .Where(c => c.Id == UserInput.ChooseContact())
                    .ExecuteDelete();
                deleteUser = UserInput.PerformAgain("delete");
            }
        }
    }

    public void UpdateContact()
    {
        using (var context = new PhonebookContext())
        {
            bool updateUser = true;

            while (updateUser)
            {
                Contact contact = GetSingleContact(context);

                List<Contact> contactList = [contact];
                PresentationLayer.ShowTable(contactList);

                contact = (UserInput.ChooseContactProperties(contact));
                context.SaveChanges();

                updateUser = UserInput.PerformAgain("update");
            }
        }
    }

    public Contact GetSingleContact(PhonebookContext context)
    {
        return context.Contacts
                    .Where(c => c.Id == UserInput.ChooseContact())
                    .Single();    
    }

    public List<Contact> GetAllContacts()
    {
        List<Contact> contactList = [];
        using (var context = new PhonebookContext())
        {
            contactList = context.Contacts.ToList();
        }
        return contactList;
    }

    public List<string> GetCategories()
    {
        List<string> categoryList = [];
        using (var context = new PhonebookContext())
        {
            categoryList = context.Contacts.Select(c => c.Category)
                                        .Distinct()
                                        .ToList();
        }
        return categoryList;
    }

    public List<Contact> GetContactsByCategory(string category)
    {       
        if (category.Equals("All contacts"))
            return GetAllContacts();

        List<Contact> contactList = [];
        using (var context = new PhonebookContext())
        {
            contactList = context.Contacts.Where(c => c.Category.Equals(category))
                                        .ToList();
        }
        return contactList;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TwilightSaw.Phonebook.Model;

namespace TwilightSaw.Phonebook.Controller;

public class ContactController(AppDbContext context)
{
    
    public Contact Add(Contact contact)
    {
        context.Contacts.Add(contact);
        context.SaveChanges();
        return contact;
    }

    public Contact AddToCategory(Contact contact, Category chosenCategory)
    {
        var category = context.Categories.FirstOrDefault(c => c == chosenCategory);
        contact.categories = new List<Category> { category };
        context.Contacts.Add(contact);
        context.SaveChanges();
        return contact;
    }

    public List<Contact> Read()
    {
        return context.Contacts.Select(t => t).ToList();
    }

    public List<Contact> Read(Category category)
    {
        return context.Contacts.Where(t => t.categories.Any(r => r.Name == category.Name)).ToList();
    }

    public void Delete(Contact contact)
    {
        context.Contacts.Where(c => c.Id.Equals(contact.Id)).AsNoTracking().ExecuteDelete();
        context.SaveChanges();
    }

    public void Update(Contact contact, string updatable, string value)
    {
        switch (updatable)
        {
            case "Name":
                context.Contacts.Where(c => c.Id.Equals(contact.Id)).AsNoTracking().
                    ExecuteUpdate(s => s.SetProperty(c => c.Name, c => value));
                break;
            case "Phone Number":
                context.Contacts.Where(c => c.Id.Equals(contact.Id)).AsNoTracking().
                    ExecuteUpdate(s => s.SetProperty(c => c.PhoneNumber, c => value));
                break;
            case "Email":
                context.Contacts.Where(c => c.Id.Equals(contact.Id)).AsNoTracking().
                    ExecuteUpdate(s => s.SetProperty(c => c.Email, c => value));
                break;
        }
        context.SaveChanges();
        context.Entry(contact).Reload();
    }

}
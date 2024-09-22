using Microsoft.EntityFrameworkCore;

namespace PhoneBookLibrary;

public static class ContactCategoryController
{
    public static List<Contact> GetContactsByCategoryId(int categoryId)
    {
        using var db = new PhoneBookContext();
        var contactsCategory = db.ContactsCategories
            .Include(cc => cc.Contact)
            .Include(cc => cc.Category)
            .Where(cc => cc.CategoryId == categoryId)
            .Select(cc => cc.Contact).ToList();
        return contactsCategory;
    }

    public static Category GetCategoryByContactName(string contactName)
    {
        using var db = new PhoneBookContext();
        var result = db.ContactsCategories
            .Include(cc => cc.Contact)
            .Include(cc => cc.Category)
            .Where(cc => cc.Contact.Name == contactName)
            .Select(cc => cc.Category)
            .SingleOrDefault();
        return result;
    }

    public static List<ContactCategory> GetCategoriesByContactId(int contactId)
    {
        using var db = new PhoneBookContext();
        var categoriesContact = db.ContactsCategories
            .Include(cc => cc.Contact)
            .Include(cc => cc.Category)
            .Where(cc => cc.ContactId == contactId)
            .ToList();
        return categoriesContact;
    }

    public static void InsertContactCategory(ContactCategory contactCategory)
    {
        using var db = new PhoneBookContext();

        db.Contacts.Attach(contactCategory.Contact);
        db.Categories.Attach(contactCategory.Category);

        db.ContactsCategories.Add(contactCategory);
        db.SaveChanges();
    }

    public static void DeleteContactCategory(ContactCategory contactCategory)
    {
        using var db = new PhoneBookContext();

        db.Contacts.Attach(contactCategory.Contact);
        db.Categories.Attach(contactCategory.Category);

        db.ContactsCategories.Remove(contactCategory);
        db.SaveChanges();
    }
}
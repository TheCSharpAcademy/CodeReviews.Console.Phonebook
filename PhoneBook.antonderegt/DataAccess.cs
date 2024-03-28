using Microsoft.EntityFrameworkCore;

namespace PhoneBook;

public class DataAccess
{
    public static async Task AddContactAsync(Contact contact)
    {
        using var context = new PhoneBookContext();
        context.Add(contact);
        await context.SaveChangesAsync();
    }

    public static async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        using var context = new PhoneBookContext();
        return await context.Contacts.ToListAsync();
    }

    public static async Task UpdateContactAsync(Contact contact)
    {
        using var context = new PhoneBookContext();
        context.Contacts.Update(contact);
        await context.SaveChangesAsync();
    }

    public static async Task RemoveContactAsync(Contact contact)
    {
        using var context = new PhoneBookContext();
        context.Contacts.Remove(contact);
        await context.SaveChangesAsync();
    }
}
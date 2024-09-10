using Microsoft.EntityFrameworkCore;
using Phonebook.tonyissa.Context;

namespace Phonebook.tonyissa.Repositories;

public static class PhonebookRepository
{

    public static async Task AddEntryAsync(PhonebookContext context, Contact entry)
    {
        await context.Contacts.AddAsync(entry);
        await context.SaveChangesAsync();
    }

    public static async Task<Contact> GetEntryAsync(PhonebookContext context, int id)
    {
        return await context.Contacts.FindAsync(id);
    }

    public static async Task<List<Contact>> GetEntryFromName(PhonebookContext context, string name)
    {
        return await context.Contacts.Where(c => c.Name.Contains(name)).ToListAsync();
    }

    public static async Task<List<Contact>> GetAllEntriesAsync(PhonebookContext context)
    {
        return await context.Contacts.ToListAsync<Contact>();
    }

    public static async Task UpdateEntryAsync(PhonebookContext context, Contact entry)
    {
        context.Contacts.Update(entry);
        await context.SaveChangesAsync();
    }

    public static async Task DeleteEntryAsync(PhonebookContext context, Contact entry)
    {
        context.Contacts.Remove(entry);
        await context.SaveChangesAsync();
    }
}
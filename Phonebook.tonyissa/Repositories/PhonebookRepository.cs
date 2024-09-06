using Microsoft.EntityFrameworkCore;
using Phonebook.tonyissa.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.tonyissa.Repositories;

public class PhonebookRepository
{
    private readonly PhonebookContext _context;

    public PhonebookRepository(PhonebookContext context)
    {
        _context = context;
    }

    public async Task AddEntryAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task<Contact> GetEntryAsync(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task<List<Contact>> GetAllEntriesAsync()
    {
        return await _context.Contacts.ToListAsync<Contact>();
    }

    public async Task UpdateEntryAsync(Contact entry)
    {
        _context.Contacts.Update(entry);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEntryAsync(int id)
    {
        var entry = await _context.Contacts.FindAsync(id);

        if (entry == null) return;

        _context.Contacts.Remove(entry);
    }
}
using Microsoft.EntityFrameworkCore;
using Phonebook.kwm0304.Data;
using Phonebook.kwm0304.Interfaces;
using Phonebook.kwm0304.Models;
using Spectre.Console;

namespace Phonebook.kwm0304.Repositories;

public class GroupRepository : IGroupRepository
{

  private readonly PhonebookContext _context;

  public GroupRepository(PhonebookContext context)
  {
    _context = context;
  }

  public async Task<List<ContactGroup>> GetAllGroupsAsync()
  {
    return await _context.ContactGroups.ToListAsync();
  }

  public async Task<ContactGroup> AddGroupAsync(ContactGroup group)
  {
      _context.ContactGroups.Add(group);
      await _context.SaveChangesAsync();
      return group;
  }

  public async Task<bool> UpdateGroupAsync(ContactGroup group)
  {
    _context.Entry(group).State = EntityState.Modified;
    try
    {
      await _context.SaveChangesAsync();
      return true;
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      throw;
    }
  }

  public async Task DeleteGroupAsync(ContactGroup group)
  {
    _context.ContactGroups.Remove(group);
    await _context.SaveChangesAsync();
  }

  public async Task<bool> GroupExistsAsync(string groupName)
  {
    return await _context.ContactGroups.AnyAsync(e => e.GroupName == groupName);
  }
}
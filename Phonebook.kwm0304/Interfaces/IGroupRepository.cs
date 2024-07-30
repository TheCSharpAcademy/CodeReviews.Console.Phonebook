using Phonebook.kwm0304.Models;

namespace Phonebook.kwm0304.Interfaces;

public interface IGroupRepository
{
  Task<List<ContactGroup>> GetAllGroupsAsync();
  Task<ContactGroup> AddGroupAsync(ContactGroup group);
  Task<bool> UpdateGroupAsync(ContactGroup group);
  Task DeleteGroupAsync(ContactGroup group);
}
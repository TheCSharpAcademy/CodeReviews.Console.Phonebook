using Phonebook.kwm0304.Interfaces;
using Phonebook.kwm0304.Models;
using Phonebook.kwm0304.Views;
using Spectre.Console;

namespace Phonebook.kwm0304.Services;

public class GroupService
{
  private readonly IGroupRepository _repository;
  public GroupService(IGroupRepository repository)
  {
    _repository = repository;
  }

  public async Task<ContactGroup?> HandleGroup(string groupChoice)
  {
    switch (groupChoice)
    {
      case "Add group":
        string group = UserPrompts.StringPrompt("group");
        bool exists = await _repository.GroupExistsAsync(group);
        if (!exists)
        {
          return await AddGroup(group);
        }
        return null;

      case "Choose existing":
        List<ContactGroup> allGroups = await GetAllGroups();
        return SelectionMenu.SelectGroup(allGroups);

      case "Back":
        return null;
      default:
        return null;
    }
  }

  public async Task<ContactGroup> AddGroup(string name)
  {
    ContactGroup newGroup = new()
    {
      GroupName = name
    };
    AnsiConsole.WriteLine("Group added successfully");
    return await _repository.AddGroupAsync(newGroup);
  }

  public async Task<List<ContactGroup>> GetAllGroups()
  {
    return await _repository.GetAllGroupsAsync();
  }

  public async Task<ContactGroup?> GetGroup()
  {
    bool includeGroup = AnsiConsole.Confirm("Do you want to add this contact to a group?");
    if (!includeGroup)
    {
      return null;
    }
    string groupChoice = SelectionMenu.ContactGroupOption();
    return await HandleGroup(groupChoice);
  }
}

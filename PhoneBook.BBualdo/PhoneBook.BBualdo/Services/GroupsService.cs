using PhoneBookLibrary;
using PhoneBookLibrary.Controllers;
using PhoneBookLibrary.Models;

namespace PhoneBook.BBualdo.Services;

public static class GroupsService
{
  public static void CreateGroup()
  {
    string groupName = UserInput.GetGroupName();
    if (groupName == "0") return;

    GroupsController.InsertGroup(groupName);
  }

  public static bool ShowGroups()
  {
    List<Group>? groups = GroupsController.GetGroups();

    if (groups == null) return false;

    ConsoleEngine.ShowGroupsTable(groups);
    return true;
  }

  public static void UpdateGroup()
  {
    if (ShowGroups())
    {
      int groupId = UserInput.GetId("group");
      if (groupId == 0) return;
      Group? group = GroupsController.GetGroupById(groupId);
      if (group == null) return;

      string newName = UserInput.GetGroupName(group.Name);
      if (newName == "0") return;
      group.Name = newName;

      GroupsController.UpdateGroup(group);
    }
  }

  public static void DeleteGroup()
  {
    if (ShowGroups())
    {
      int groupId = UserInput.GetId("group");
      if (groupId == 0) return;
      Group? group = GroupsController.GetGroupById(groupId);
      if (group == null) return;

      GroupsController.DeleteGroup(group);
    }
  }
}
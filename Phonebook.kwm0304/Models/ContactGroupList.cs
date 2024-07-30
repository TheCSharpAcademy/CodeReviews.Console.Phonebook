namespace Phonebook.kwm0304.Models;

public class ContactGroupList
{
  private List<ContactGroup> _groups = [];

  public void AddGroup(string groupName)
  {
    _groups.Add(new ContactGroup { GroupName = groupName });
  }

  public void RemoveGroup(string groupName)
  {
    _groups.RemoveAll(g => g.GroupName == groupName);
  }

  public List<ContactGroup> GetAllGroups()
  {
    return new List<ContactGroup>(_groups);
  }
}
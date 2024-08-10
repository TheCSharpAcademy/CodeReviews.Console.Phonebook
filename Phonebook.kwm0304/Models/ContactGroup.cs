namespace Phonebook.kwm0304.Models;

public class ContactGroup
{
  public int GroupId { get; set; }
  public string? GroupName { get; set; }
    public override string ToString()
    {
        return $"{GroupName}";
    }
}
using System.ComponentModel;

namespace PhoneBook.Enums;

/// <summary>
/// Enum with the editable contact properties available in the phone book.
/// </summary>
internal enum ContactEditOptions
{
    [Description("First Name")]
    FirstName,
    [Description("Last Name")]
    LastName,
    [Description("Phone Number")]
    Phone,
    [Description("Email Address")]
    Email,
    [Description("Group Name")]
    Group,
}
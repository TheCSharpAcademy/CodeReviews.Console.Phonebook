using System.ComponentModel.DataAnnotations;

namespace Phonebook.Enums;
internal enum PhonebookAction
{
    [Display(Name="View Contacts")] ViewContacts,
    [Display(Name="Create Contact")] CreateContact,
    [Display(Name="Update Contact")] UpdateContact,
    [Display(Name="Delete Contact")] DeleteContact,
    [Display(Name="Send Email")] SendEmail,
    [Display(Name="Exit")] Exit
}

internal enum UpdateOptions
{
    Name,
    Email,
    Number,
    Category,
    All,
    Exit
}

internal enum FilterOptions
{
    All,
    Friends,
    Family,
    Work,
    Exit
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhoneNumbers;
namespace Phonebook.Models;


internal class Phone
{
    [Key]
    public int PhoneId { get; set; }

    [ForeignKey(nameof(ContactId))]
    public int ContactId { get; set; }
    public string PhoneNumber { get; set; }

    public Phone()
    {
        // Parameterless constructor for Entity Framework
    }
    public Phone(string phoneNumber, int contactId)
    {
        this.ContactId = contactId;
        this.PhoneNumber = phoneNumber;
    }
}


using System.ComponentModel.DataAnnotations;


namespace Phonebook.Models;
internal class Contact
{
    [Key]
    public int ContactId { get; set; }

    [Required]

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Email> Emails { get; set; }
    public ICollection<Phone> PhoneNumbers { get; set; }

}

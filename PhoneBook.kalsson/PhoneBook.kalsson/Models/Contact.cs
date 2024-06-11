using System.ComponentModel.DataAnnotations;

namespace PhoneBook.kalsson.Models;

public class Contact
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    public List<Email> EmailAddresses { get; set; } = new List<Email>();
    public List<Phone> PhoneNumbers { get; set; } = new List<Phone>();
}
using System.ComponentModel.DataAnnotations;

namespace Phonebook.K_MYR.Models;

internal class Contact
{
    public int ContactId { get; set; }
    [StringLength(50)]
    public required string Name { get; set; }
    [StringLength(50)]
    public required string EmailAdress { get; set; }
    [StringLength(20)]

    public required string PhoneNumber { get; set; }
    public required int CategoryId { get; set; }
    public Category? Category { get; set; }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.K_MYR.Models;

internal class Contact
{
    public int ContactId { get; set; }
    public string Name {get; set;}
    public string EmailAdress { get; set; }
    public string PhoneNumber { get; set; }
    public int? CategoryId { get; set; }
    public Category Category { get; set; }
}

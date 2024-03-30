using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models;

internal class Email
{
    [Key]
    public int EmailId { get; set; }

    [ForeignKey(nameof(ContactId))]
    public int ContactId { get; set; }
    public string EmailAddress { get; set; }
}

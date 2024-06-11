using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.kalsson.Models;

public class Phone
{
    public int Id { get; set; }
    
    [Required]
    public int ContactId { get; set; }
    
    [Required]
    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; }
}
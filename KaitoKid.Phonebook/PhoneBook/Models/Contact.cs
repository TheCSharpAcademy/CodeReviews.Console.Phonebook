using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public int TempId {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email {  get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [DefaultValue("General")]
        public string? Category { get; set; } = "General";

    }
}

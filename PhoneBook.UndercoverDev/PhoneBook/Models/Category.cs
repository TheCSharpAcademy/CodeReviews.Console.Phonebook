using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Contact> Contacts { get; set; } = [];
    }
}
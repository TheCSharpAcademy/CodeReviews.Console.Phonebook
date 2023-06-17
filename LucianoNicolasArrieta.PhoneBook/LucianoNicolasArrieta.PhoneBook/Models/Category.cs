using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LucianoNicolasArrieta.PhoneBook.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public Category(string name)
        {
            Name = name;
        }
    }
}

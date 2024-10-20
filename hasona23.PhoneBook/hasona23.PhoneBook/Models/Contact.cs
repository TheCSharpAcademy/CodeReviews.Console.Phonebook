using System.ComponentModel.DataAnnotations;

namespace hasona23.PhoneBook.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Contact() { }
        public Contact(string name, string phoneNumber , string email)
        {
            Name = name;
            Phone = phoneNumber;
            Email = email;
        }
        public override string ToString() 
        {
            return $"Name: {Name}, Phone: {Phone}, Email: {Email}";
        }
    }
}

namespace PhoneBook.Arashi256.Models
{
    internal class Category
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property for the related contacts
        public List<Contact> Contacts { get; set; }
    }
}

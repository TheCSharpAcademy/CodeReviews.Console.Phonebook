namespace PhoneBook.Arashi256.Models
{
    internal class Contact
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        // Foreign key for Category
        public int CategoryId { get; set; }
        // Navigation property to the Category
        public Category Category { get; set; }
    }
}
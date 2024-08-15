namespace PhoneBook.Arashi256.Models
{
    internal class ContactDto
    {
        public int? Id { get; set; }
        public int? DisplayId { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

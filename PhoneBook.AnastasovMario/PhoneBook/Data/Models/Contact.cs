using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Data.Models
{
	public class Contact
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[Phone]
		public string PhoneNumber { get; set; } = null!;
	}
}

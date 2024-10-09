using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Dtos
{
	public class ContactDto
	{
		public string Name { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[Phone]
		public string PhoneNumber { get; set; } = null!;
	}
}

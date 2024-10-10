using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneBook.Data.Models;

namespace PhoneBook.Data.Configurations
{
	public class ContactConfiguration : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> builder)
		{
			// Seeding data using the HasData method
			builder.HasData(SeedData());
		}

		public Contact[] SeedData()
		{
			// Create and return an array of contacts
			return new Contact[]
			{
								new Contact
								{
										Id = 1,
										Name = "John Doe",
										Email = "john.doe@example.com",
										PhoneNumber = "555-123-4567"
								},
								new Contact
								{
										Id = 2,
										Name = "Jane Smith",
										Email = "jane.smith@example.com",
										PhoneNumber = "555-987-6543"
								},
								new Contact
								{
										Id = 3,
										Name = "Michael Johnson",
										Email = "michael.johnson@example.com",
										PhoneNumber = "555-654-3210"
								},
								new Contact
								{
										Id = 4,
										Name = "Emily Davis",
										Email = "emily.davis@example.com",
										PhoneNumber = "555-876-5432"
								},
								new Contact
								{
										Id = 5,
										Name = "William Brown",
										Email = "william.brown@example.com",
										PhoneNumber = "555-432-1098"
								}
			};
		}
	}
}

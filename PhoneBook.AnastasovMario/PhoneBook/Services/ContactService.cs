using Microsoft.EntityFrameworkCore;
using PhoneBook.Contracts;
using PhoneBook.Data.Models;
using PhoneBook.Dtos;

namespace PhoneBook.Services
{
  public class ContactService(PhonebookDbContext context) : IContactService
	{
		private PhonebookDbContext _context = context;

		public async Task AddContact(ContactDto contact)
		{
			bool emailExists = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == contact.Email) != null;

			if (emailExists)
			{
				Console.WriteLine("The email is already in use");
			}

			bool phoneExists = await _context.Contacts.FirstOrDefaultAsync(c => c.PhoneNumber == contact.PhoneNumber) != null;

			if (phoneExists)
			{
				Console.WriteLine("The number is already in use");
			}

			_context.Contacts.Add(new Contact
			{
				Name = contact.Name,
				PhoneNumber = contact.PhoneNumber,
				Email = contact.Email,
			});

			await _context.SaveChangesAsync();

			Console.WriteLine("New Contact has been added");
		}

		public async Task DeleteContact(int id)
		{
			var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

			if (contact != null)
			{
				_context.Contacts.Remove(contact);
				await _context.SaveChangesAsync();
			}
			else
			{
				Console.WriteLine("Contact with this Id doesn't exist");
			}
		}

		public async Task<List<ContactDto>> GetAllContacts()
		{
			return await _context.Contacts
				.Select(c => new ContactDto
				{
					Name = c.Name,
					PhoneNumber = c.PhoneNumber,
					Email = c.Email,
				})
				.ToListAsync();
		}

    public Task<Contact> GetProduct(int id)
    {
      throw new NotImplementedException();
    }

    public async Task UpdateContact(int id, ContactDto updatedContact)
		{
			var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

			if (contact != null)
			{
				 contact.Name = updatedContact.Name;
				 contact.PhoneNumber = updatedContact.PhoneNumber;
				 contact.Email = updatedContact.Email;

				 await _context.SaveChangesAsync();
			}
			else
			{
				Console.WriteLine("Contact with this Id doesn't exist");
			}
		}
	}
}

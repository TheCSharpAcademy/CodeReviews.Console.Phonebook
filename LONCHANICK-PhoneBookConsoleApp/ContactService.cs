using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LONCHANICK_PhoneBookConsoleApp
{
	internal class ContactService
	{
		internal static void AddContact()
		{
			Contact contact = new();
			contact.Name = AnsiConsole.Ask<string>("Name:");
			contact.PhoneNumber = AnsiConsole.Ask<string>("PhoneNumber:");
			ContactRepository.Add(contact);
		}

		internal static void RemoveContact()
		{
			throw new NotImplementedException();
		}

		internal static void UpdateContact()
		{
			throw new NotImplementedException();
		}

		internal static void ViewAllContacts()
		{
			throw new NotImplementedException();
		}

		internal static void ViewContact()
		{
			throw new NotImplementedException();
		}
	}
}

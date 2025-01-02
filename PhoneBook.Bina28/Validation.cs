using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook.Bina28;

internal class Validation
{
	internal static string GetValidPhoneNumber()
	{
		string phoneNumber;
		do
		{
			phoneNumber = AnsiConsole.Ask<string>("Enter Phone Number in the format (+country code) xxxxxxx: ");
		}
		while (!IsValidPhoneNumber(phoneNumber));

		return phoneNumber;
	}

	private static bool IsValidPhoneNumber(string phoneNumber)
	{
		string pattern = @"^\(\+\d{1,4}\)\s?\d{7,15}$";
		if (Regex.IsMatch(phoneNumber, pattern))
		{
			return true;
		}

		AnsiConsole.MarkupLine("[red]Invalid phone number! Please try again.[/]");
		return false;
	}

	internal static string GetValidEmail()
	{
		string email;
		do
		{
			email = AnsiConsole.Ask<string>("Enter Email: ");
		}
		while (!IsValidEmail(email));

		return email;
	}

	private static bool IsValidEmail(string email)
	{
		// Regex pattern to validate email
		string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
		if (Regex.IsMatch(email, pattern))
		{
			return true;
		}

		AnsiConsole.MarkupLine("[red]Invalid email address! Please try again.[/]");
		return false;
	}
}

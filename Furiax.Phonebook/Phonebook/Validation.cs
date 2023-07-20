using System.Net.Mail;

namespace Phonebook
{
	internal class Validation
	{
		internal static bool IsValidPhoneNumber(string phoneNumber)
		{
            foreach (char c in phoneNumber)
			{
				if (!char.IsDigit(c))
				{
					return false;
				}
			}
			return true;
		}
		internal static bool IsValidEmail(string email)
		{
			try
			{
				MailAddress mailAddress = new MailAddress(email);
					return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

	}
}

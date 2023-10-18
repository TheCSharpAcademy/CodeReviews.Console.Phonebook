using PhoneBook.Data;
using System.Globalization;
using System.Text.RegularExpressions;

// Code from - https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
namespace PhoneBook.Utils
{
    public static class Validate
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidNumber(string number)
        {
            return int.TryParse(number, out _);
        }

        public static bool IsValidId(string inputId)
        {
            if (!IsValidNumber(inputId)) return false;

            PhoneBookDbContext context = new PhoneBookDbContext();

            PhoneBookRepository db = new PhoneBookRepository(context);
            int id = int.Parse(inputId);

            var contact = db.GetContact(id);

            return contact != null;


        }

        public static bool IsValidString(string name)
        {
            return !string.IsNullOrWhiteSpace(name);

        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

            if (phoneNumber != null)
            {
                return Regex.IsMatch(phoneNumber, pattern);
            }
            else
            {
                return false;
            }
        }
    }

}

using System.Net.Mail;
using PhoneBook.Models;
using PhoneNumbers;

namespace PhoneBook.Utilities
{
    public class ValidationHelper
    {
        internal static bool CategoryExists(ContactContext context, string categoryName)
            => context.Categories.Any(c => c.Name.ToLower().Equals(categoryName.ToLower()));

        internal static bool ContactExists(ContactContext contactContext, string phoneNumber)
            => contactContext.Contacts.Any(c => c.PhoneNumber.Equals(phoneNumber));

        internal static bool EmailIsValid(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        internal static bool PhoneNumberIsValid(string phoneNumber, out string formattedNumber)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            formattedNumber = string.Empty;

            try
            {
                var parsedNumber = phoneNumberUtil.Parse(phoneNumber, null); // null for any country
                if (phoneNumberUtil.IsValidNumber(parsedNumber))
                {
                    formattedNumber = phoneNumberUtil.Format(parsedNumber, PhoneNumberFormat.INTERNATIONAL);
                    return true;
                }
            }
            catch (NumberParseException)
            {
                return false;
            }

            return false;
        }
    }
}
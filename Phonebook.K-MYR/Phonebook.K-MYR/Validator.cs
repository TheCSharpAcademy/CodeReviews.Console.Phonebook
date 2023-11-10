using PhoneNumbers;
using System.Net.Mail;

namespace Phonebook.K_MYR
{
    internal static class Validator
    {
        internal static bool EmailIsValid(string email)
        {
            return MailAddress.TryCreate(email, out _);
        }

        internal static bool PhoneNumberIsValid(string phoneNumber)
        {
            try
            {
                PhoneNumber number = PhoneNumberUtil.GetInstance().Parse(phoneNumber, "");
                return PhoneNumberUtil.GetInstance().IsValidNumber(number);
            }
            catch
            {
                return false;
            }
        }
    }
}

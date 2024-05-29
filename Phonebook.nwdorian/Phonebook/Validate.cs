using System.Text.RegularExpressions;

namespace Phonebook;
internal class Validate
{
    internal static bool IsValidEmail(string email)
    {
        var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

        var regex = new Regex(pattern);

        return regex.IsMatch(email) || email == "0";

    }

    internal static bool IsValidPhoneNumber(string phonenumber)
    {
        var pattern = @"^((\d{2,4})/)?((\d{6,8})|(\d{2})-(\d{2})-(\d{2,4})|(\d{3,4})-(\d{3,4}))$";

        var regex = new Regex(pattern);

        return regex.IsMatch(phonenumber) || phonenumber == "0";

    }
}

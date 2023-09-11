using System.Text.RegularExpressions;

namespace PhoneBook.w0lvesvvv.Utils
{
    public static class UserInputValidation
    {
        public static bool ValidateNumber(string number, out int parsedNumber)
        {
            if (int.TryParse(number, out parsedNumber))
            {
                return true;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input.");
            return false;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 11 || phoneNumber.ToCharArray()[3] != ' ' || phoneNumber.ToCharArray()[7] != ' ') return false;

            if (!phoneNumber.Substring(0, 1).Equals("6") && !phoneNumber.Substring(0, 3).Equals("976")) return false;

            return true;
        }
    }
}

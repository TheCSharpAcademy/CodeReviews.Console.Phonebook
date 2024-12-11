using System.Text.RegularExpressions;

namespace PhoneBook.Helpers
{
    internal class Validators
    {
        public static string ValidatePhoneNumberLT(string number)
        {
            while (!Regex.IsMatch(number , "^(?:\\+370|370|8)\\d{8}$"))
            {
                Console.WriteLine("You need to enter number! Please try again");
                number = Console.ReadLine();
            }
            return number;
        }
        public static string ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            while(!Regex.IsMatch(email, pattern))
            {
                Console.WriteLine("Wrong format please try again!");
                email = Console.ReadLine();
            }
            return email;
        }
        public static string ValidateName(string name) 
        {
            name = name.Replace(" ", "");
            while (!name.All(s => char.IsLetter(s)))
            {
                Console.WriteLine("Name entered incorrectly! Please try again!");
                name = Console.ReadLine();
                name = name.Replace(" ", "");
            }
            return name;
        }
        public static string ValidateNumber(string number)
        {
            number = number.Replace(" ", "");
            while (!number.All(s => char.IsDigit(s)))
            {
                Console.WriteLine("Input need to be number. Try again");
                number = Console.ReadLine();
            }
            return number;
        }
        public static void ValidateString(string str)
        {
            while (string.IsNullOrWhiteSpace(str))
            {
                Console.WriteLine("Entered value can't be empty");
                str = Console.ReadLine();
            }
        }
    }
}

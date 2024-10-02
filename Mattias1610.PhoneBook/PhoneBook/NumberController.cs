using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spectre.Console;

namespace PhoneBook{    
    internal class NumberController()
    {
        internal static void AddNumber()
        {
            var name = AnsiConsole.Ask<string>("Name:");
            var email = GetValidatedEmail();
            var phoneNumber = GetValidatedPhoneNumber();
            var category = AnsiConsole.Ask<string>("Category:");
            using var db = new NumberContext();
            db.Add(new Number{Name = name, Email = email, PhoneNumber = phoneNumber, Category = category });
            db.SaveChanges();
            Console.Clear();
            
        }

        internal static void DeleteNumber(Number number)
        {
            using var db = new NumberContext();
            db.Remove(number);
            db.SaveChanges();
        }

        internal static Number GetNumberById(int id){
            using var db = new NumberContext();
            var number = db.Numbers.SingleOrDefault(x => x.ID == id);

            return number;
        }

        internal static List <Number> Show()
        {
            using var db = new NumberContext();
            var numbers = db.Numbers.ToList();
            return numbers;
        }

        internal static void UpdateNumber(Number number)
        {
            var newName = AnsiConsole.Ask<string>($"New Name :");
            var newEmail = GetValidatedEmail($"New Email:");
            var newPhoneNumber = GetValidatedPhoneNumber($"New Phone Number:");
            var newCategory= AnsiConsole.Ask<string>($"New Category :");

            number.Name = string.IsNullOrWhiteSpace(newName) ? number.Name : newName;
            number.Email = string.IsNullOrWhiteSpace(newEmail) ? number.Email : newEmail;
            number.PhoneNumber = string.IsNullOrWhiteSpace(newPhoneNumber) ? number.PhoneNumber : newPhoneNumber;
            number.Category = string.IsNullOrWhiteSpace(newCategory) ? number.Category : newCategory;
            using var db = new NumberContext();
            db.Update(number);
            db.SaveChanges();
        }

        private static string GetValidatedEmail(string prompt = "Email:")
        {
            string email;
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; 

            do
            {
                email = AnsiConsole.Ask<string>(prompt);

                 

                if (Regex.IsMatch(email, emailPattern))
                    break;

                AnsiConsole.MarkupLine("[red]Invalid email format. Please try again.[/]");
            }
            while (true);

            return email;
        }

        private static string GetValidatedPhoneNumber(string prompt = "Phone Number:")
        {
            string phoneNumber;
            var phonePattern = @"^\d{10}$";  

            do
            {
                phoneNumber = AnsiConsole.Ask<string>(prompt);

               

                if (Regex.IsMatch(phoneNumber, phonePattern))
                    break;

                AnsiConsole.MarkupLine("[red]Invalid phone number. It must be 10 digits. Please try again.[/]");
            }
            while (true);

            return phoneNumber;
        }

    }
}
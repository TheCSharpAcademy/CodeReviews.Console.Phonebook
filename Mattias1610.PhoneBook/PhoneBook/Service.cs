using System.Net.Http.Headers;
using Spectre.Console;

namespace PhoneBook
{
    public class Service
    {
        internal static void DeleteNumber(){
            var number = GetNumber();
            NumberController.DeleteNumber(number);
        }

        internal static void DeleteNumbers(){
            var numbers = NumberController.Show();
            UserInterface.ShowTable(numbers);
        }

        internal static void GetNum(){
            var numbers = NumberController.Show();
            UserInterface.ShowTable(numbers);
        }

        static public Number GetNumber(){
            var numbers = NumberController.Show();
            var numbersArray = numbers.Select(x => x.Name).ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("CHOOSE: ")
                .AddChoices(numbersArray));
            var id = numbers.Single(x => x.Name == option).ID;
            var number = NumberController.GetNumberById(id);

            return number;
        }

        internal static void SendEmail()
        {
            EmailFunction email = new();
            email.Send();
        }

        internal static void GetCategories()
        {
            Categories categories = new();
            categories.Menu();
        }
    }
}
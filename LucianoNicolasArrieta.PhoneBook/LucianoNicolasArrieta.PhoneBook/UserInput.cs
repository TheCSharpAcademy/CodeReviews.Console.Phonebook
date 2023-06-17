using LucianoNicolasArrieta.PhoneBook.Models;
using Spectre.Console;
using System.Net.Mail;

namespace LucianoNicolasArrieta.PhoneBook
{
    public class UserInput
    {
        public Contact ContactInput()
        {
            var name = AnsiConsole.Ask<string>("What's the contact's [aqua]Name[/]?");
            var phoneNumber = AnsiConsole.Ask<string>("Insert the [aqua]Phone Number[/]: ");
            var email = EmailInput();

            return new Contact(name, phoneNumber, email.Address);
        }

        public MailAddress EmailInput()
        {
            MailAddress mail = null;

            while (mail == null)
            {
                string adress = AnsiConsole.Ask<string>("Email: ");
                try
                {
                    mail = new MailAddress(adress);
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]Invalid email. Try again.[/]");
                }
            }

            return mail;
        }

        public int ValidIdInput(List<int> ids, string aux, string purpose)
        {
            int id = AnsiConsole.Ask<int>($"Enter the id of the {aux} you want to {purpose}: ");

            while (!ids.Contains(id))
            {
                AnsiConsole.MarkupLine("[red]The id doesn't exist. Try again[/]");
                id = AnsiConsole.Ask<int>($"Enter the id of the {aux} you want to {purpose}: ");
            }

            return id;
        }

        internal Category CategoryInput()
        {
            var name = AnsiConsole.Ask<string>("Category [aqua]Name[/]:");

            return new Category(name);
        }
    }
}

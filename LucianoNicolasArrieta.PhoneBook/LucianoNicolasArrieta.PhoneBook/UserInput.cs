using LucianoNicolasArrieta.PhoneBook.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

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

        public int ValidIdInput(List<int> ids, string aux)
        {
            int id = AnsiConsole.Ask<int>($"Enter the id of the contact you want to {aux}: ");

            while (!ids.Contains(id))
            {
                AnsiConsole.MarkupLine("[red]The id doesn't exist. Try again[/]");
                id = AnsiConsole.Ask<int>($"Enter the id of the contact you want to {aux}: ");
            }

            return id;
        }
    }
}

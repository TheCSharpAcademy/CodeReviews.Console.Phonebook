using Spectre.Console;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace PhobeBook.Kakurokan
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                DataAccess dataAcess = new();

                AnsiConsole.Write(
        new FigletText("PhoneBook")
            .LeftJustified()
            .Color(Color.Red));

                var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Choose a option:")
            .PageSize(10)
            .AddChoices(new[] { "View contacts", "Add a contact","Update a contact" ,"Delete a contact", "Exit"
            }));

                switch (option)
                {
                    case "View contacts":
                        ViewContacts(dataAcess);
                        break;
                    case "Add a contact":
                        AddContact(dataAcess);
                        break;
                    case "Update a contact":
                        UpdateContact(dataAcess);
                        break;
                    case "Delete a contact":
                        DeleteContact(dataAcess);
                        break;
                } 

                Environment.Exit(0);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Sorry, an error ocurred: " + ex.ToString());
            }
        }


        public static void ViewContacts(DataAccess dataAccess)
        {
            var table = new Table();
            table.AddColumn("Name");
            table.AddColumn("Email");
            table.AddColumn("Phonenumber");
            table.Title("Contacts");
            foreach (var contact in dataAccess.Contact)
            {
                table.AddRow(contact.Name, contact.Email, contact.PhoneNumber);
            }


            AnsiConsole.Write(table);
            AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .PageSize(10)
        .AddChoices(new[] { "Return to menu"
        }));

            DisplayReturningTomenu();
        }

        public static void AddContact(DataAccess dataAccess)
        {
            ContactModel contact = CreateContact();

            dataAccess.Add(contact);
            dataAccess.SaveChanges();
            DisplayReturningTomenu();
        }

        public static void DeleteContact(DataAccess dataAccess)
        {
            int id = SelectContact(dataAccess);

            var choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[Red]Are you sure?[/]")
        .PageSize(15)
        .AddChoices(new[] {
            "Yes", "No"
        })); ;

            if (choice == "Yes")
            {
                dataAccess.Remove(dataAccess.Contact.Single(a => a.Id == id));
                dataAccess.SaveChanges();
            }
            DisplayReturningTomenu();

        }

        public static void UpdateContact(DataAccess dataAccess)
        {
            int id = SelectContact(dataAccess);

            ContactModel contact = CreateContact();

            var oldContact = dataAccess.Contact.First(a => a.Id == id);
            oldContact.Name = contact.Name;
            oldContact.PhoneNumber = contact.PhoneNumber;
            oldContact.Email = contact.Email;
            dataAccess.SaveChanges();
            DisplayReturningTomenu();

        }

        public static int SelectContact(DataAccess dataAcess)
        {
            var option =
    new SelectionPrompt<string>()
        .Title("Select a contact to [Red]delete[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]");

            foreach (var contact in dataAcess.Contact)
            {
                option.AddChoice("Id: " + contact.Id + "; Name: " + contact.Name + "; Email: " + contact.Email + "; Phonenumber: " + contact.PhoneNumber);
            }

            string selectedContact = AnsiConsole.Prompt(option);

            int id = int.Parse(Regex.Match(selectedContact, @"Id:\s*([0-9]+)").Value.Remove(0, 3));

            return id;
        }

        public static ContactModel CreateContact()
        {
            string name = AnsiConsole.Ask<string>("What´s the [Red]name[/] of your contact?");

            string email = "";

            while (email == "")
            {
                email = AnsiConsole.Ask<string>("What´s the [Red]email[/] of your contact?");
                if (!MailAddress.TryCreate(email, out var mailAddress))
                {
                    email = "";
                    AnsiConsole.Markup("[Red]Invalid email![/]");
                    AnsiConsole.WriteLine();
                }
            }

            string number = "";

            while (number == "")
            {
                number = AnsiConsole.Ask<string>("What´s the [Red]phonenumber[/] of your contact? <(555) 444-1234>");
                if (!Regex.IsMatch(number, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                {
                    number = "";
                    AnsiConsole.Markup("[Red]Invalid number![/]");
                    AnsiConsole.WriteLine();
                }
            }

            return new(name, email, number);
        }

        public static void DisplayReturningTomenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.Status().Start("Returning to menu...", ctx =>
            {
                Thread.Sleep(2000);
            });
            Main();

        }
    }
}

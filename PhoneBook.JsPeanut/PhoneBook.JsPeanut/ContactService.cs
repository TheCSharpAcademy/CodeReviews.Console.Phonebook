using PhoneBook.JsPeanut.Models;
using Spectre.Console;

namespace PhoneBook.JsPeanut
{
    internal class ContactService
    {
        internal static void InsertContact()
        {
            var name = AnsiConsole.Ask<string>("Contact's name");
            while (Validator.CheckName(name) == "null/empty")
            {
                Console.WriteLine("You didn't insert a name. Please insert one. Type M to go to the main menu.");

                if (name == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }

                name = AnsiConsole.Ask<string>("Contact's name");
            }
            if (Validator.CheckName(name) == "duplicated name")
            {
                Console.WriteLine("The name of your new contact coincides with another one! If you still want to name your new contact like that, please enter the name once again.");

                name = AnsiConsole.Ask<string>("Contact's name");
            }
            var phoneNumber = AnsiConsole.Ask<string>("Phone number:");
            while (Validator.CheckNumber(phoneNumber) == "null/empty")
            {
                Console.WriteLine("You didn't insert a phone number. Please insert one. Type M to go to the main menu.");

                if (phoneNumber == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }

                phoneNumber = AnsiConsole.Ask<string>("Phone number:");
            }
            while (Validator.CheckNumber(phoneNumber) == "duplicated number")
            {
                Console.WriteLine("The phone number of your new contact coincides with another one! Please enter another phone number. Type M to go back to the main menu.");

                phoneNumber = AnsiConsole.Ask<string>("Phone number:");

                if (phoneNumber == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }
            while (Validator.CheckNumber(phoneNumber) == "not a number")
            {
                Console.WriteLine("The phone number you entered is invalid. It can't contain numbers or letters. Please try again. Type M to go back to the main menu.");

                phoneNumber = AnsiConsole.Ask<string>("Phone number:");

                if (phoneNumber == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }

            ContactController.AddContact(name, phoneNumber);
        }
        internal static void DeleteContact()
        {
            var contact = GetContactOptionInput();
            var option = AnsiConsole.Prompt(
        new SelectionPrompt<YesNoOptions>()
        .Title($"You're about to delete the contact '{contact.Name}'. Are you sure?")
        .AddChoices(
            YesNoOptions.Yes,
            YesNoOptions.No));
            if (option == YesNoOptions.Yes)
            {
                ContactController.DeleteContact(contact);
            }
            else
            {
                Console.WriteLine("No contacts were deleted. Enter any key to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }

        internal static void GetContact()
        {
            var contact = GetContactOptionInput();
            UserInterface.ShowContact(contact);
        }

        internal static void GetContacts()
        {
            var contacts = ContactController.GetContacts();
            if (contacts.Count == 0 || contacts.Count == null)
            {
                Console.WriteLine("No contacts available. Enter any key to continue");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                UserInterface.ShowContactTable(contacts);
            }
        }

        internal static void UpdateContact()
        {
            var contact = GetContactOptionInput();
            var newContactName = AnsiConsole.Ask<string>("Contact's name:");
            while (Validator.CheckName(newContactName) == "null/empty")
            {
                Console.WriteLine("You didn't insert a name. Please insert one. Type M to go back to the main menu.");

                newContactName = AnsiConsole.Ask<string>("Contact's name");

                if (newContactName == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }
            if (Validator.CheckName(newContactName) == "duplicated name")
            {
                Console.WriteLine("The name of your new contact coincides with another one! If you still want to name your new contact like that, please enter the name once again.");

                newContactName = AnsiConsole.Ask<string>("Contact's name");
            }

            var newPhoneNumber = AnsiConsole.Ask<string>("Contact's phone number");
            while (Validator.CheckNumber(newPhoneNumber) == "null/empty")
            {
                Console.WriteLine("You didn't insert a phone number. Please insert one. Type M to go back to the main menu.");

                if (newPhoneNumber == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }

                newPhoneNumber = AnsiConsole.Ask<string>("Phone number:");
            }
            while (Validator.CheckNumber(newPhoneNumber) == "duplicated number")
            {
                Console.WriteLine("The phone number of your new contact coincides with another one! Please enter another phone number. Type M to go back to the main menu.");

                newPhoneNumber = AnsiConsole.Ask<string>("Phone number:");

                if (newPhoneNumber == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }
            while (Validator.CheckNumber(newPhoneNumber) == "not a number")
            {
                Console.WriteLine("The phone number you entered is invalid. It can't contain numbers or letters. Please try again. Type M to go back to the main menu.");

                newPhoneNumber = AnsiConsole.Ask<string>("Phone number:");

                if (newPhoneNumber == "M")
                {
                    Console.WriteLine("\nEnter any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }

            var option = AnsiConsole.Prompt(
        new SelectionPrompt<YesNoOptions>()
        .Title($"You're about to update the contact '{contact.Name}' of number '{contact.PhoneNumber}' and change its name to {newContactName} and its phone number to {newPhoneNumber}. Are you sure?")
        .AddChoices(
            YesNoOptions.Yes,
            YesNoOptions.No));
            if (option == YesNoOptions.Yes)
            {
                contact.Name  =newContactName;
                contact.PhoneNumber = newPhoneNumber;
                ContactController.UpdateContact(contact);
            }
            else
            {
                Console.WriteLine("No contacts were deleted. Enter any key to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
        static private Contact GetContactOptionInput()
        {
            var contacts = ContactController.GetContacts();

            var contactsArray = contacts.Select(x => x.Name).ToArray();

            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose Contact")
                .AddChoices(contactsArray));

            var id = contacts.Single(x => x.Name == option).Id;

            var contact = ContactController.GetContactById(id);

            return contact;
        }
    }
}

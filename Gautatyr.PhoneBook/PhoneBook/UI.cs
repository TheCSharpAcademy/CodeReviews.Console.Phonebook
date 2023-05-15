using static PhoneBook.Helpers;
using static PhoneBook.DataValidation;
using static PhoneBook.DatabaseInterface;
using PhoneBook.Models;
using ConsoleTableExt;

namespace PhoneBook;

internal class UI
{
    public static void MainMenu(string message = "")
    {
        Console.Clear();
        Console.WriteLine("\nCONTACT LIST:\n");

        DisplayContacts();

        Console.WriteLine("\n- Type 1 to Add a contact");
        Console.WriteLine("\n- Type 2 to Update a contact");
        Console.WriteLine("\n- Type 3 to Delete a contact");
        Console.WriteLine("\n- Type 0 to Close the Application");

        Console.WriteLine(message);

        int input = GetNumberInput();

        switch (input)
        {
            case 0:
                Environment.Exit(0);
                break;
            case 1:
                AddContactUI();
                break;
            case 2:
                int contactId = GetContactIdInput("\nPlease enter the id of the contact you wish to update, or 0 to cancel\n");

                if (contactId != 0)
                {
                    UpdateContactUI(contactId);
                }
                else
                {
                    MainMenu();
                }
                break;
            case 3:
                contactId = GetContactIdInput("\nPlease enter the id of the contact you wish to delete, or 0 to cancel\n");

                if (contactId != 0
                    && GetConfirmation($"\nDelete {GetContact(contactId).FullName()} ? (yes/no)\n"))
                    DeleteContact(contactId);

                MainMenu();
                break;
            default:
                MainMenu("\n\n|--> ERROR: Please type a number between 0 and 3. <---|\n\n");
                break;
        }
    }

    private static void AddContactUI()
    {
        Console.Clear();

        Console.WriteLine("\nCREATE CONTACT\n");

        string firstName = GetTextInput("\nFirst Name:\n");

        string lastName = GetTextInput("\nLast name:\n");

        string phoneNumber = GetTextInput("\nPhone Number:\n");

        CreateContact(firstName, lastName, phoneNumber);

        MainMenu($"\n\n{firstName} {lastName} added to your contacts !\n");
    }

    private static void UpdateContactUI(int contactId)
    {
        Console.Clear();

        DisplayContact(contactId);

        Console.WriteLine("\n- Type 1 to modify the first name");
        Console.WriteLine("\n- Type 2 to modify the last name");
        Console.WriteLine("\n- Type 3 to modify the phone number");
        Console.WriteLine("\n- Type 0 to to get back to the main menu");

        var input = GetNumberInput();

        while (input != 0)
        {
            Console.Clear();

            switch (input)
            {
                case 1:
                    string newFirstName = GetTextInput("\nType in the new first name:\n");
                    UpdateFirstName(newFirstName, contactId);
                    break;
                case 2:
                    string newLastName = GetTextInput("\nType in the new last name:\n");
                    UpdateLastName(newLastName, contactId);
                    break;
                case 3:
                    string newPhoneNumber = GetTextInput("\nType in the phone number:\n");
                    UpdatePhoneNumber(newPhoneNumber, contactId);
                    break;
            }

            Console.Clear();

            DisplayContact(contactId);

            Console.WriteLine("\n- Type 1 to modify the first name");
            Console.WriteLine("\n- Type 2 to modify the last name");
            Console.WriteLine("\n- Type 3 to modify the phone number");
            Console.WriteLine("\n- Type 0 to to get back to the main menu");

            input = GetNumberInput();
        }

        MainMenu();
    }

    private static void DisplayContacts()
    {
        List<Contact> contacts = GetContacts();

        ConsoleTableBuilder
            .From(contacts)
            .ExportAndWriteLine();
    }

    private static void DisplayContact(int id)
    {
        Contact contact = GetContact(id);

        List<Contact> contacts = new List<Contact>
        {
            contact
        };

        ConsoleTableBuilder
            .From(contacts)
            .ExportAndWriteLine();
    }
}

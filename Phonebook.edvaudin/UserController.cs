using Phonebook.DataAccess;
using Phonebook.Models;

namespace Phonebook
{
    internal class UserController
    {
        public static void ProcessInput(string userInput)
        {
            switch (userInput)
            {
                case "v":
                    GetContacts();
                    break;
                case "a":
                    AddContact();
                    break;
                case "d":
                    DeleteContact();
                    break;
                case "u":
                    UpdateContact();
                    break;
                case "0":
                    Program.SetEndAppToTrue();
                    break;
                default:
                    break;
            }
        }

        private static void UpdateContact()
        {
            GetContacts();
            Console.WriteLine("\nPlease enter the name of the contact for updating:");
            string name = UserInput.GetName();
            if (!Validator.IsNameInContacts(name))
            {
                Console.WriteLine($"\nWe could not find anyone called {name} in your contacts. Please try again.");
                UpdateContact();
            }
            else
            {
                Contact contact = DataAccessor.GetContact(name);
                Console.WriteLine($"\nWould you like to update {name}'s name or phone number? (Type 'n' or 'p')");
                string option = UserInput.GetUserUpdateOption();
                if (option == "n")
                {
                    UpdateContactName(name, contact);
                }
                else if (option == "p")
                {
                    UpdateContactPhoneNumber(name, contact);
                }
            }
        }

        private static void UpdateContactPhoneNumber(string name, Contact contact)
        {
            Console.WriteLine("\nPlease enter the new phone number for this contact:");
            string newNum = UserInput.GetPhoneNumber();
            DataAccessor.UpdateContactPhoneNumber(contact, newNum);
            Console.WriteLine($"\n{name}'s phone number has been updated to: {newNum}");
        }

        private static void UpdateContactName(string name, Contact contact)
        {
            Console.WriteLine("\nPlease enter the new name for this contact:");
            string newName = UserInput.GetName();
            DataAccessor.UpdateContactName(contact, newName);
            Console.WriteLine($"\n{name}'s name has been updated to: {newName}");
        }

        private static void DeleteContact()
        {
            GetContacts();
            Console.WriteLine("\nPlease enter the name of the contact for deletion:");
            string name = UserInput.GetName();
            if (!Validator.IsNameInContacts(name))
            {
                Console.WriteLine($"\nWe could not find anyone called {name} in your contacts. Please try again.");
                DeleteContact();
            }
            else
            {
                DataAccessor.DeleteContact(name);
                Console.WriteLine($"\n{name} has been removed from your contacts.");
            }
        }

        private static void AddContact()
        {
            Console.WriteLine("\nWhat is the name of your new contact?");
            string name = UserInput.GetName();
            if (Validator.IsNameInContacts(name))
            {
                Console.WriteLine($"\n{name} is already in your contacts, try updating their number instead.");
                return;
            }
            Console.WriteLine("\nWhat is the phone number of your new contact?");
            string phoneNumber = UserInput.GetPhoneNumber();
            DataAccessor.AddContact(name, phoneNumber);
            Console.WriteLine($"\n{name} has been added to your contacts.");
        }

        private static void GetContacts()
        {
            List<Contact> contacts = DataAccessor.GetContacts();
            UserMenu.ViewContacts(contacts);
        }
    }
}

using System;

namespace PhoneBook.maccer989
{
    public class UserInterface
    {
        public static bool checkForContacts = false;
        public static void GetUserInput()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("---------\n");
                Console.WriteLine("What would you like to do?\n");
                Console.WriteLine("Type 1 to View all Contacts");
                Console.WriteLine("Type 2 to Insert Contacts");
                Console.WriteLine("Type 3 to Delete a Contact");
                Console.WriteLine("Type 4 to Update a Contact\n");
                Console.WriteLine("Type 0 to Close Application");
                Console.WriteLine("---------------------------\n");

                string command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        GetAllContacts();
                        break;
                    case "2":
                        CreateNewContact();                        
                        break;
                    case "3":
                        RemoveContact();
                        break;
                    case "4":
                        CreateUpdatedContact();                        
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.");
                        break;
                }
            }
        }
        private static void GetAllContacts()
        {
            checkForContacts = EFCrud.CheckForContacts();
            if (checkForContacts)
            {
                Console.WriteLine("All Contacts");
                Console.WriteLine("------------");
                EFCrud.ReadAll();
            }
            else
            {
                Console.WriteLine("There are no Contacts, hit enter to return to Main Menu.");
                Console.ReadLine();
                GetUserInput();
            }
        }

        private static void RemoveContact()
        {
            checkForContacts = EFCrud.CheckForContacts();
            if (checkForContacts)
            {
                int contactId = GetContactId();
                EFCrud.RemoveContactId(contactId);
            }
            else
            {
                Console.WriteLine("There are no Contacts, hit enter to return to Main Menu.");
                Console.ReadLine();
                GetUserInput();
            }
        }
        private static int GetContactId()
        {
            Console.WriteLine("Enter Contact Id:");
            string contactIdInput = Console.ReadLine();
            while (!Validator.IsIdValid(contactIdInput))
            {
                Console.WriteLine("\nInvalid Contact Id, try again:");
                contactIdInput = Console.ReadLine();
            }
            int contactId = Int32.Parse(contactIdInput);
            return contactId;
        }
        private static void CreateNewContact()
        {
            Console.WriteLine("Create a new Contact:");
            string firstName = GetContactFirstName();
            string lastName = GetContactLastName();
            string PhoneNumber = GetContactPhoneNumber();
            string EmailAddress = GetContactEmailAddress();
            EFCrud.CreateContact(firstName, lastName, PhoneNumber, EmailAddress);

        }
        private static void CreateUpdatedContact()
        {
            checkForContacts = EFCrud.CheckForContacts();
            if (checkForContacts)
            {                
                int contactId = GetContactId();
                string firstName = GetContactFirstName();
                string lastName = GetContactLastName();
                string phoneNumber = GetContactPhoneNumber();
                string emailAddress = GetContactEmailAddress();
                EFCrud.UpdateContact(contactId, firstName, lastName, phoneNumber, emailAddress);
            }
            else
            {
                Console.WriteLine("There are no Contacts, hit enter to return to Main Menu.");
                Console.ReadLine();
                GetUserInput();
            }
        }
        private static string GetContactEmailAddress()
        {
            Console.WriteLine("\nEnter Email Address:");
            Console.WriteLine("Use one or more alphanumeric characters, dots (.), underscores (_), percent signs (%), plus signs (+), or hyphens (-) before the @ symbol.");
            Console.WriteLine("Use the @ symbol. One or more alphanumeric characters or hyphens after the @ symbol.");
            Console.WriteLine("Use a dot (.) followed by at least two alphanumeric characters (e.g., .com, .co.uk, .gov)\n");
            string emailAddress = Console.ReadLine();

            while (!Validator.IsValidEmailAddress(emailAddress))
            {
                Console.WriteLine("\nEmail Address, please enter again:");
                emailAddress = Console.ReadLine();
            }
            return emailAddress;
        }
        private static string GetContactPhoneNumber()
        {
            Console.WriteLine("\nEnter a Phone Number:");
            Console.WriteLine("Note:- the number must start with a plus sign (+) to indicate the presence of a country code.");
            Console.WriteLine("Use a country code of 1 to 4 digits. Use 1 to 14 digits for the rest of the phone number:\n");
            string phoneNumber = Console.ReadLine();

            while (!Validator.IsValidPhoneNumber(phoneNumber))
            {
                Console.WriteLine("\nInvalid Phone Number, please enter again:");
                phoneNumber = Console.ReadLine();
            }
            return phoneNumber;
        }
        private static string GetContactLastName()
        {
            Console.WriteLine("\nEnter Last Name:");
            string lastName = Console.ReadLine();
            while (!Validator.IsStringValid(lastName))
            {
                Console.WriteLine("\nInvalid Last Name");
                lastName = Console.ReadLine();
            }
            return lastName;
        }
        private static string GetContactFirstName()
        {
            Console.WriteLine("\nEnter First Name:");
            string firstName = Console.ReadLine();
            while (!Validator.IsStringValid(firstName))
            {
                Console.WriteLine("\nInvalid First Name");
                firstName = Console.ReadLine();
            }
            return firstName;
        }
    }
}

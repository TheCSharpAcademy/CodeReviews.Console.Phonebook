using PhoneBook.Models;
using PhoneBook.Helpers;
using System.Net.Mail;
using System.Configuration;

namespace PhoneBook
{
    internal class UI
    {
        public static MailMessage GetEmailInput()
        {
            Console.WriteLine("Write id of the contact that you want to send message to or press 0 to return:");
            string? id = Console.ReadLine();
            if (id == "0")
            {
                PhoneBookMenu.GetUserInput();
            }
            List<Contact> contacts = PhoneBookController.GetContactsList();
            Contact contactToSendTo = null;
            id = Validators.ValidateNumber(id);
            contactToSendTo = contacts.FirstOrDefault(contact => contact.Id == Convert.ToInt32(id));
            if (contactToSendTo == null)
            {
                Console.WriteLine("There is no contact with this id. Press any key to return");
                Console.ReadLine();
                PhoneBookMenu.GetUserInput();
            }

            var fromEmail = new MailAddress(ConfigurationManager.AppSettings.Get("email"));

            var email = new MailMessage();
            email.From = fromEmail;
            email.To.Add(contactToSendTo.Email);

            Console.WriteLine("Write an email subject:");
            email.Subject = Console.ReadLine().Trim();

            Console.WriteLine("Write a message that you want to send:");
            email.Body = Console.ReadLine();

            return email;
        }
        public static Contact? GetUserChoosedContactInput(List<Contact> contacts)
        {
            if (contacts == null)
            {
                Console.WriteLine("There is no contacts added!");
                Console.WriteLine("Press any number to continue");
                Console.ReadLine();
                PhoneBookMenu.GetUserInput();
            }
            Console.WriteLine("Enter contact id wich you want to choose or press 0 to return");
            var option = Console.ReadLine();
            if (option == "0")
            {
                PhoneBookMenu.GetUserInput();
            }
            option = Validators.ValidateNumber(option);
            var choosedContact = contacts.SingleOrDefault(x => x.Id == Convert.ToInt32(option));
            if (choosedContact == null)
            {
                Console.WriteLine("Chosed contact doesn't exist. Press any key to return");
                Console.ReadLine();
                PhoneBookMenu.GetUserInput();
            }
            return choosedContact;
        }
        public static string? GetContactName()
        {
            string name;
            Console.WriteLine("What is the name of the contact?(only letters is acceptable)");
            name = Console.ReadLine();
            name = Validators.ValidateName(name);
            return name;
        }
        public static string? GetContactEmail() 
        {
            string email;
            Console.WriteLine("What is the email of the contact?(valid input - ********@gmail.com)");
            email = Console.ReadLine();
            email = Validators.ValidateEmail(email);
            return email;
        }
        public static string? GetContactPhone()
        {
            string num;
            Console.WriteLine("What is the number of the contact?(valid input - +370 XXXX XXXX or 8 XXXX XXXX)");
            num = Console.ReadLine();
            num = Validators.ValidatePhoneNumberLT(num);
            return num;
        }
        public static void ShowCurrentContact(Contact contact)
        {
            Console.WriteLine("Name: " +  contact.Name);
            Console.WriteLine("Email: " + contact.Email);
            Console.WriteLine("Phone: " + contact.PhoneNumber);
        }
        public static void ShowContactTable(List<Contact> contacts)
        {
            Console.Clear();
            if (!contacts.Any())
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("Contacts:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Email",-20} {"Phone",-15}");
            Console.WriteLine(new string('-', 50));

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Id,-5} {contact.Name,-20} {contact.Email,-20} {contact.PhoneNumber,-15}");
            }
            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
        }
        static internal Contact? GetContactOptionInput()
        {
            Console.Clear();
            var contacts = PhoneBookController.GetContactsList();
            ShowContactTable(contacts);
            return GetUserChoosedContactInput(contacts);

        }
    }
}

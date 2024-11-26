using PhoneBook.Helpers;
using PhoneBook.Models;

namespace PhoneBook.Services
{
    internal class PhoneBookService
    {
        static internal Contact? GetContactOptionInput()
        {
            Console.Clear();
            var contacts = PhoneBookController.GetContactsList();
            ShowContactTable(contacts);
            if (contacts == null)
            {
                Console.WriteLine("There is no contacts added!");
                Console.WriteLine("Press any number to continue");
                Console.ReadLine();
                PhoneBookMenu.GetUserInput();
            }
            Console.WriteLine("Enter contact id wich you want to choose or press 0 to return");
            var option = Console.ReadLine();
            if(option == "0")
            {
                PhoneBookMenu.GetUserInput();
            }
            while (!Validators.ValidateNumber(option))
            {
                Console.WriteLine("You need to enter number! Please try again");
                option = Console.ReadLine();
            }
            var choosedContact = contacts.SingleOrDefault(x => x.Id == Convert.ToInt32(option));
            if (choosedContact == null)
            {
                Console.WriteLine("Chosed contact doesn't exist. Press any key to return");
                Console.ReadLine();
                PhoneBookMenu.GetUserInput();
            }
            return choosedContact;
        }
        static internal Contact UpdateContact()
        {
            var option = GetContactOptionInput();

            string? name = "";
            string? email = "";
            string? num = "";

            GetUserInput(ref name, ref email, ref num);

            option.Name = name;
            option.Email = email;
            option.PhoneNumber = num;

            return option;
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
        public static void GetUserInput(ref string? name, ref string? email, ref string? num)
        {
            Console.Clear();
            Console.WriteLine("What is the name of the contact?");
            name = Console.ReadLine();
            while (!Validators.ValidateName(name))
            {
                Console.WriteLine("Name entered incorrectly! Please try again!");
                name = Console.ReadLine();
            }

            Console.WriteLine("What is the email of the contact?");
            email = Console.ReadLine();
            while (!Validators.ValidateEmail(email))
            {
                Console.WriteLine("Email entered incorrectly! Please try again!");
                email = Console.ReadLine();
            }

            Console.WriteLine("What is the number of the contact?");
            num = Console.ReadLine();
            while (!Validators.ValidateNumber(num))
            {
                Console.WriteLine("Phone number entered incorrectly! Please try again!");
                num = Console.ReadLine();
            }
        }
        public static void GetEmailInput()
        {
            Console.Clear();
            PhoneBookController.ViewContacts();
            Console.WriteLine("Write id of the contact that you want to send message to or press 0 to return:");
            string? id = Console.ReadLine();
            if(id == "0")
            {
                return;
            }
            List<Contact> contacts = PhoneBookController.GetContactsList();
            Contact contactToSendTo = null;
            while (!Validators.ValidateNumber(id))
            {
                Console.WriteLine("Id need to be number. Try again");
                id = Console.ReadLine();
            }
            foreach (var contact in contacts)
            {
                if (contact.Id == Convert.ToInt32(id))
                {
                    contactToSendTo = contact;
                    break;
                }
            }
            if (contactToSendTo == null)
            {
                Console.WriteLine("There is no contact with this id. Press any key to return");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Write an email subject:");
            string? subject = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(subject))
            {
                Console.WriteLine("Subject cannot be empty. Try again.");
                subject = Console.ReadLine();
            }

            Console.WriteLine("Write a message that you want to send:");
            string? message = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Message cannot be empty. Try again");
                message = Console.ReadLine();
            }

            EmailService.SendEmail(contactToSendTo.Email, message, contactToSendTo.Name, subject);
        }
    }
}

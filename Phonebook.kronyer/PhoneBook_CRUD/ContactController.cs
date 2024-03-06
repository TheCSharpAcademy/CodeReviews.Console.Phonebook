using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhoneBook_CRUD
{
    //controller é so contato com a db
    internal class ContactController
    {
        internal static void AddContact(string name, string email, string phoneNumber)
        {
            Console.Clear();
            var newContact = new Contacts { Name = name, Email = email, PhoneNumber = phoneNumber };

            if (string.IsNullOrWhiteSpace(newContact.Name))
            {
                Console.WriteLine("O nome é obrigatório.");
                return;
            }

            if (!string.IsNullOrEmpty(newContact.Email) && !new EmailAddressAttribute().IsValid(newContact.Email) && !string.IsNullOrEmpty(newContact.PhoneNumber) && !new PhoneAttribute().IsValid(newContact.PhoneNumber))
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = Color.White;
                Console.WriteLine("Email and Phone number are invalid\n");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = Color.Black;
                Console.Clear();


                UserInterface.MainMenu();
            }
            if (!string.IsNullOrEmpty(newContact.Email) && !new EmailAddressAttribute().IsValid(newContact.Email))
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = Color.White;
                Console.WriteLine("Email is invalid\n");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = Color.Black;
                Console.Clear();

                UserInterface.MainMenu(); 
            }

            if (!string.IsNullOrEmpty(newContact.PhoneNumber) && !new PhoneAttribute().IsValid(newContact.PhoneNumber))
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = Color.White;
                Console.WriteLine("Phone number is invalid\n");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = Color.Black;
                Console.Clear();

                UserInterface.MainMenu();
            }

            using var db = new ContactsContext();
            db.Add(newContact);
            db.SaveChanges();
        }

        internal static void DeleteContact(Contacts contact)
        {
            using var db = new ContactsContext();
            // if doido aqui para are you sure?

            db.Remove(contact);
            db.SaveChanges();
        }

        internal static Contacts SearchContactById(int id)
        {

            using var db = new ContactsContext();
            var contact = db.Contacts.FirstOrDefault(x => x.Id == id);

            return contact;
        }

        internal static List<Contacts> ShowAllContacts()
        {
            using var db = new ContactsContext();
            var contacts = db.Contacts.ToList();

            return contacts;
        }


        internal static void ShowContact(Contacts contact)
        {
            Console.Clear();
            var panel = new Panel($@"Id: {contact.Id}
Name: {contact.Name}
Email: {contact.Email}
Phone: {contact.PhoneNumber}");
            panel.Header = new PanelHeader("Contact Info");
            panel.Padding = new Padding(1, 1, 1, 1);
            AnsiConsole.Write(panel);
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
            Console.Clear();
        }

        internal static void UpdateContact(string name, string email, string phoneNumber)
        {
            Console.Clear();
            var newContact = new Contacts { Name = name, Email = email, PhoneNumber = phoneNumber };

            if (string.IsNullOrWhiteSpace(newContact.Name))
            {
                Console.WriteLine("O nome é obrigatório.");
                return;
            }

            if (!string.IsNullOrEmpty(newContact.Email) && !new EmailAddressAttribute().IsValid(newContact.Email) && !string.IsNullOrEmpty(newContact.PhoneNumber) && !new PhoneAttribute().IsValid(newContact.PhoneNumber))
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = Color.White;
                Console.WriteLine("Email and Phone number are invalid\n");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = Color.Black;
                Console.Clear();


                UserInterface.MainMenu();
            }
            if (!string.IsNullOrEmpty(newContact.Email) && !new EmailAddressAttribute().IsValid(newContact.Email))
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = Color.White;
                Console.WriteLine("Email is invalid\n");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = Color.Black;
                Console.Clear();

                UserInterface.MainMenu();
            }

            if (!string.IsNullOrEmpty(newContact.PhoneNumber) && !new PhoneAttribute().IsValid(newContact.PhoneNumber))
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = Color.White;
                Console.WriteLine("Phone number is invalid\n");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = Color.Black;
                Console.Clear();

                UserInterface.MainMenu();
            }


            using var db = new ContactsContext();
            db.Update(newContact);
            db.SaveChanges();
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using PhoneBook.Data;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class CrudController
    {
        internal static void Add()
        {
            Console.Clear();
            Console.WriteLine("Adding new contact.\n");
            Console.WriteLine("Please enter the following information:\n");
            Console.Write("Name: ");
            string userInputName = Console.ReadLine();
            Console.Write("\nEmail: ");
            string userInputEmail = Console.ReadLine();
            bool isValidEmail = Validation.IsValidMail(userInputEmail);
            Console.Write("\nPhoneNumber (exact format 0475/12.23.45): ");
            string userInputPhoneNumber = Console.ReadLine();
            bool isValidPhoneNumber = Validation.IsValidPhoneNumber(userInputPhoneNumber);

            if (isValidEmail == false || isValidPhoneNumber == false)
            {
                Console.WriteLine("\nInvalid email or phone number, returning to main menu.");
                Console.ReadLine();
                MenuBuilder.MainMenu();
            }
            using PhoneBookContext context = new PhoneBookContext();

            ContactModel newContact = new ContactModel()
            {
                Name = $"{userInputName}",
                Email = $"{userInputEmail}",
                PhoneNumber = $"{userInputPhoneNumber}"
            };
            

            Console.Write("\nAre you sure you want to add this contact? (y/n): ");
            string userYN = Console.ReadLine();
            switch (userYN.ToLower())
            {
                case "y":
                    context.Add(newContact);
                    context.SaveChanges();
                    Console.WriteLine("Contact added. Returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                    break;
                case "n":
                    Console.WriteLine("Contact was NOT added. Returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input. Returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                    break;
            }
        }

        internal static void Delete()
        {
            Console.Clear();
            Console.WriteLine("Deleting a contact.\n");

            using PhoneBookContext context = new PhoneBookContext();

            var contacts = context.Contacts;

            foreach (var contact in contacts)
            {
                Console.WriteLine($"Id: {contact.Id}\nName: {contact.Name}\nEmail: {contact.Email}\nPhonenumber: {contact.PhoneNumber}");
                Console.WriteLine("--------");
            }

            Console.Write("\nEnter the name of the contact you want to delete: ");
            string userInputName = Console.ReadLine();

            var toDelete = context.Contacts
                .Where(c => c.Name == $"{userInputName}")
                .FirstOrDefault();

            
            Console.Write("\nAre you sure you want to delete this contact? (y/n): ");
            string userYN = Console.ReadLine();
            switch (userYN.ToLower())
            {
                case "y":
                    if (toDelete is ContactModel)
                    {
                        context.Remove(toDelete);
                    }
                    context.SaveChanges();
                    Console.WriteLine("Contact deleted. Returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                    break;
                case "n":
                    Console.WriteLine("Contact was NOT deleted. Returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input. Returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                    break;
            }

            Console.WriteLine("\nPress enter to return to main menu.");
            Console.ReadLine();
            MenuBuilder.MainMenu();
        }

        internal static void Read()
        {
            Console.Clear();
            Console.WriteLine("Viewing all contacts.\n");
            
            using PhoneBookContext context = new PhoneBookContext();

            var contacts = context.Contacts;

            foreach (var contact in contacts )
            {
                Console.WriteLine($"Id: {contact.Id}\nName: {contact.Name}\nEmail: {contact.Email}\nPhonenumber: {contact.PhoneNumber}");
                Console.WriteLine("--------");
            }
            Console.WriteLine("\nPress enter to return to main menu.");
            Console.ReadLine();
            MenuBuilder.MainMenu();
        }

        internal static void Update()
        {
            Console.Clear();
            Console.WriteLine("Updating a contact.\n");

            using PhoneBookContext context = new PhoneBookContext();

            var contacts = context.Contacts;

            foreach (var contact in contacts)
            {
                Console.WriteLine($"Id: {contact.Id}\nName: {contact.Name}\nEmail: {contact.Email}\nPhonenumber: {contact.PhoneNumber}");
                Console.WriteLine("--------");
            }

            Console.Write("\nEnter the id of the contact you want to update: ");
            string userInputId = Console.ReadLine();
            int parsedUserInputId = 0;
            if (int.TryParse(userInputId, out parsedUserInputId))
            {
                Console.Write("Enter the new name of the contact: \n");
                string newName = Console.ReadLine();
                Console.Write("Enter the new email of the contact: \n");
                string newEmail = Console.ReadLine();
                bool isValidMail = Validation.IsValidMail(newEmail);
                Console.Write("Enter the new phone number of the contact: \n");
                string newPhoneNumber = Console.ReadLine();
                bool isValidPhoneNumber = Validation.IsValidPhoneNumber(newPhoneNumber);

                if (isValidMail == false || isValidPhoneNumber == false)
                {
                    Console.WriteLine("\nInvalid email or phone number, returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                }

                var toUpdate = context.Contacts
                    .Where(c => c.Id == parsedUserInputId)
                    .FirstOrDefault();
                if (toUpdate  is ContactModel && toUpdate != null)
                {
                    toUpdate.Name = newName;
                    toUpdate.Email = newEmail;
                    toUpdate.PhoneNumber = newPhoneNumber;

                    context.SaveChanges();

                    Console.WriteLine("Contact updated, returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                }
                else
                {
                    Console.WriteLine("Contact not found! Returning to main menu.");
                    Console.ReadLine();
                    MenuBuilder.MainMenu();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer. Returning to main menu.");
                Console.ReadLine();
                MenuBuilder.MainMenu();
            }
        }
    }
}

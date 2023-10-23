using ConsoleTableExt;
using PhoneBook.Data;
using PhoneBook.Models;
using PhoneBook.Utils;

namespace PhoneBook.UI
{
    public static class UserInterface
    {
        public static void Start()
        {
            PhoneBookDbContext context = new PhoneBookDbContext();

            PhoneBookRepository db = new PhoneBookRepository(context);


            bool keepGoing = true;
            while (keepGoing)
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

                Console.Write(">");
                string? command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        keepGoing = false;
                        break;
                    case "1":
                        GetAllContacts();
                        break;
                    case "2":
                        InsertContact();
                        break;
                    case "3":
                        DeleteContact();
                        break;
                    case "4":
                        UpdateContact();
                        break;
                    default:
                        Console.WriteLine("Invalid command.Try again.");
                        break;
                }

            }

            void UpdateContact()
            {

                string? input = GetInputId();

                int id = int.Parse(input);

                string? name = GetName();

                string? email = GetEmail();

                string? phoneNumber = GetPhoneNumber();

                var updatedContact = db.UpdateContact(id, new Contact
                {
                    Id = id,
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber
                });

                if (updatedContact != null)
                {
                    Console.WriteLine("Record updated!");
                }
                else
                {
                    Console.WriteLine("Error: Fail to update record!");
                }
            }

            void DeleteContact()
            {

                string? input = GetInputId();

                int id = int.Parse(input);

                var deleteContact = db.DeleteContact(id);

                if (deleteContact != null)
                {
                    Console.WriteLine("Record deleted!");
                }
                else
                {
                    Console.WriteLine("Error: Fail to delete record!");
                }

            }

            void InsertContact()
            {

                string? name = GetName();
                if (name == null) CloseApp();

                string? email = GetEmail();

                string? phoneNumber = GetPhoneNumber();



                var insertRecord = db.AddContact(new PhoneBook.DTOs.ContactDTO
                {
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Name = name,

                });

                if (insertRecord == null)
                {
                    Console.WriteLine("Fail to insert record to db");
                }
                else
                {

                    Console.WriteLine("Record inserted to db");
                }


            }

            void GetAllContacts()
            {
                var tableData = new List<List<object>>();
                var contacts = db.GetContacts();

                foreach (var contact in contacts)
                {
                    tableData.Add(new List<object> { contact.Id, contact.Name, contact.Email, contact.PhoneNumber });
                }

                ConsoleTableBuilder
                .From(tableData)
                .WithColumn("Id", "Name", "Email", "Phone Number")
                .ExportAndWriteLine();
            }


            string? GetName()
            {
                Console.Write("Enter your name: ");
                string? name = Console.ReadLine()?.Trim();
                if (name == "0") return null;

                while (!Validate.IsValidString(name))
                {
                    Console.WriteLine("Invalid name.Try again.");
                    Console.Write("Enter your name: ");
                    name = Console.ReadLine();
                }

                return name;
            }

            string GetEmail()
            {
                Console.Write("Enter your email(Format: test@gmail.com, test@hotmail.no etc): ");
                string? email = Console.ReadLine()?.Trim();

                while (!Validate.IsValidString(email) || !Validate.IsValidEmail(email))
                {
                    Console.WriteLine("Invalid name.Try again.");
                    Console.Write("Enter your email: ");
                    email = Console.ReadLine();
                }

                return email;
            }

            string GetPhoneNumber()
            {
                Console.Write("Enter your phone number(Format: 0123456789, 012-345-6789, and (012) -345-6789): ");
                string? phoneNumber = Console.ReadLine()?.Trim();

                while (!Validate.IsValidString(phoneNumber) || !Validate.IsValidPhoneNumber(phoneNumber))
                {
                    Console.WriteLine("Invalid phoneNumber.Try again.");
                    Console.Write("Enter your phone number: ");
                    phoneNumber = Console.ReadLine();
                }

                return phoneNumber;
            }

            string GetInputId()
            {
                Console.Write("Enter an id: ");
                string? input = Console.ReadLine();

                while (!Validate.IsValidString(input) || !Validate.IsValidId(input))
                {
                    Console.WriteLine("Invalid id.Try again.");
                    Console.Write("Enter an id: ");
                    input = Console.ReadLine();
                }

                return input;
            }

            void CloseApp()
            {
                keepGoing = false;

            }
        }
    }
}

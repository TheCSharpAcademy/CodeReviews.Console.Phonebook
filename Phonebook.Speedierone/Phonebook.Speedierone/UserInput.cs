using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook
{
    internal class UserInput
    {
        public static void AddContact()
        {
            Console.Clear();
            using var db = new ContactContext();
            Console.WriteLine("Please enter name of new contact.");
            var name = Console.ReadLine();
            
            Console.WriteLine("Please enter phone number for new contact.");
            var number = Console.ReadLine().ToString();

            Console.WriteLine("Please enter an email address. Leave blank if you do not wish to add one.");
            var email = Console.ReadLine();

            Console.WriteLine("Please enter a contact group. Leave blank if you do not wish to add one.");
            var group = Console.ReadLine();
            
            db.Add(new Contact { Name = name, PhoneNumber = number, Email = email, ContactGroup = group });
            db.SaveChanges();
            Console.WriteLine($"{name} added to phonebook. Press any button to continue.");
            Console.ReadLine();
        }
        public static void ViewContacts()
        {
            Console.Clear();
            using var db = new ContactContext();

            var contacts = db.Contacts
                .OrderBy(x => x.Name);

            List<Contact> contact = new List<Contact>();

            foreach (Contact x in contacts)
            {
                contact.Add(new Contact
                {
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    ContactGroup = x.ContactGroup
                });
            }
            TableDesigner.DisplayTable(contact);           
        }
        public static void DeleteContact()
        {
            Console.Clear();
            ViewContacts();
            Console.WriteLine("\n\nPlease select name of person you wish to delete.");
            var name = Console.ReadLine();
            using (var db = new ContactContext())
            {
                var deleteName = db.Contacts.FirstOrDefault(x => x.Name == name);
                if (deleteName != null)
                {
                    db.Contacts.Remove(deleteName);
                    db.SaveChanges();
                    Console.WriteLine($"{name} deleted from phonebook. Press any key to continue.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"{name} does not exist in phonebook. Please try again.");
                    name = Console.ReadLine();
                }                
            }
        }
        public static void UpdateContact()
        {
            Console.Clear();
            ViewContacts();
            Console.WriteLine("Please enter name of contact you wish to update.");
            var name = Console.ReadLine();

            using (var db = new ContactContext())
            {
                var record = db.Contacts.FirstOrDefault(x => x.Name == name);                          

                if (record != null)
                {
                    Console.WriteLine("Please choose the option for what you would like to update for this contact.");
                    Console.WriteLine("0 - Return to main menu.");
                    Console.WriteLine("1 - Name.");
                    Console.WriteLine("2 - Phone number");
                    Console.WriteLine("3 - Email.");
                    Console.WriteLine("4 - Contact Group.");
                    var command = Console.ReadLine();

                    switch (command)
                    {
                        case "0":
                            MainMenu.ShowMenu();
                            break;
                        case "1":
                            Console.WriteLine("Please enter updated name.");
                            var updateName = Console.ReadLine();
                            record.Name = updateName;
                            db.Update(record);
                            db.SaveChanges();
                            Console.WriteLine($"{updateName} has been updated in phonebook. Press any key to continue.");
                            Console.ReadLine();
                            break;
                        case "2":
                            Console.WriteLine("Please enter updated number.");
                            var number = Console.ReadLine().ToString();
                            record.PhoneNumber = number;                         
                            db.Update(record);
                            db.SaveChanges();
                            Console.WriteLine($"Phonenumber has been update for {record.Name}. Press any key to continue.");
                            Console.ReadLine();
                            break;
                        case "3":
                            Console.WriteLine("Please enter updated email.");
                            var email = Console.ReadLine();
                            record.Email = email;
                            db.Update(record);
                            db.SaveChanges();
                            Console.WriteLine($"Email has been updated for {record.Name}. Press any key to continue.");
                            Console.ReadLine();
                            break;
                        case "4":
                            Console.WriteLine("Please enter updated Contact Group.");
                            var contactGroup = Console.ReadLine();
                            record.ContactGroup = contactGroup;
                            db.Update(record);
                            db.SaveChanges();
                            Console.WriteLine($"Contact group updated for {record.Name}. Press any key to continue.");
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Invalid entry, try again.");
                            command = Console.ReadLine();
                            break;
                    }                                                                    
                }
                else
                {
                    Console.WriteLine("Contact does not exist. Please try again.");
                    name = Console.ReadLine();
                }
            }           
        }

        internal static void ViewByGroup()
        {
            Console.WriteLine("Which group would you like to view?");
            var group = Console.ReadLine();

            using var db = new ContactContext();

            var contactGroup = db.Contacts
                .Where(x => x.ContactGroup == group);

            List<Contact> contacts = new();

            foreach (Contact x in contactGroup)
            {
                contacts.Add(new Contact
                {
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    ContactGroup = x.ContactGroup
                });
            }
            TableDesigner.DisplayTable(contacts);
            Console.WriteLine("\nPress any button to continue.");
            Console.ReadLine();
            
        }
    }
}

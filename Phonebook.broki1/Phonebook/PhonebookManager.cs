using Microsoft.IdentityModel.Tokens;
using Phonebook.Data;
using Phonebook.Model;

namespace Phonebook;

internal class PhonebookManager
{
    private static PhonebookContext context = new();

    internal static void AddContact()
    {
        var name = UserInput.GetName();
        var email = UserInput.GetEmail();
        var phoneNumer = UserInput.GetPhoneNumber();

        var contact = new Contact
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumer
        };

        context.Contacts.Add(contact);

        var result = context.SaveChanges();

        if (result == 1)
        {
            Console.WriteLine("\ncontact added\npress any key to continue");
        }
        else
        {
            Console.WriteLine("\ncontact not added\npress any key to continue");
        }

        Console.ReadKey();
    }

    internal static Contact? ReadContact()
    {
        bool contactFound = false;
        Contact contact = null;

        while (!contactFound)
        {
            var input = UserInput.GetReadInput();

            var contacts = context.Contacts.Where(x => x.Name.Contains(input) || x.Email.Contains(input) || x.PhoneNumber.Contains(input))
                .ToList();

            if (input.ToLower().Equals("exit"))
            {
                contact = null;
                contactFound = true;
            }
            else if (contacts == null || contacts.Count == 0)
            {
                Console.WriteLine("\nno contacts found");
                Console.WriteLine("\npress any key to continue");
                Console.ReadKey();
            }
            else
            {
                VisualizationTool.PrintContacts(contacts);
                Console.WriteLine("\nenter ID of contact to email, or press enter to return to main menu:");
                var contactId = Console.ReadLine().Trim();

                if (contactId.IsNullOrEmpty())
                {
                    contact = null;
                    contactFound = true;
                    continue;
                }

                while (!ValidationEngine.ValidID(contactId))
                {
                    Console.WriteLine("\ninvalid input, please enter ID of contact:");
                    contactId = Console.ReadLine().Trim();
                }

                contact = context.Contacts.Where(x => x.Id == Int32.Parse(contactId)).ToList()[0];

                contactFound = true;

                Console.WriteLine("\npress 'e' to email contact, or any other key to continue");
                var choice = Console.ReadLine();

                if (!choice.IsNullOrEmpty())
                {
                    if (choice == "e")
                    {
                        var email = UserInput.CreateEmail(contact.Email);

                        EmailController.SendEmail(email);
                    }
                }
            }
        }

        return contact;
    }

    internal static void UpdateContact()
    {
        VisualizationTool.PrintContacts(context.Contacts.ToList());

        var contactID = UserInput.GetContactID();

        var contact = context.Contacts.Where(x => x.Id == contactID).ToList();

        VisualizationTool.PrintContacts(contact);

        UserInput.GetUpdate(contact[0]);

        context.SaveChanges();

        Console.WriteLine("\ncontact updated\n\npress any key to continue");
        Console.ReadKey();
    }
    internal static void DeleteContact()
    {
        VisualizationTool.PrintContacts(context.Contacts.ToList());

        var contactID = UserInput.GetContactID();

        var contact = context.Contacts.Where(x => x.Id == contactID).First();

        Console.WriteLine("\nare you sure you want to delete this contact? press '1' to confirm, press '0' to cancel");

        var userChoice = UserInput.GetUpdateChoice();

        if (userChoice == "1")
        {
            context.Contacts.Remove(contact);
            context.SaveChanges();
            Console.WriteLine("\ncontact deleted");
        }
        else
        {
            Console.WriteLine("\ncancelled");
        }

        Console.WriteLine("\n\npress any key to continue");
        Console.ReadKey();
    }
}

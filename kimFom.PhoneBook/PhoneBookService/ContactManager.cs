using PhoneBookConsole.Data;
using PhoneBookConsole.Input;
using PhoneBookConsole.Models;

namespace PhoneBookConsole.PhoneBookService;

public class ContactManager
{
    private readonly UserInput _input = new UserInput();
    private readonly IDbManager _efDbManager = new EfDbManager();

    public void LaunchProgram()
    {
        Menus.DisplayMainMenu();
        var choice = _input.GetInput();
        while (choice != "0")
        {
            switch (choice)
            {
                case "1":
                    ViewAllContacts();
                    break;
                case "2":
                    AddNewContact();
                    break;
                case "3":
                    EditContact();
                    break;
                case "4":
                    DeleteContact();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            Menus.DisplayMainMenu();
            choice = _input.GetInput();
        }
    }

    private void ViewAllContacts()
    {
        Console.Clear();
        var contactList = _efDbManager.GetContacts();
        foreach (var contact in contactList)
        {
            Console.Write($"Name: {contact.Name} Phone Number: {contact.PhoneNumber} Email: {contact.Email}\n");
            Console.WriteLine();
        }

        Console.Write("Press Enter to continue: ");
        Console.ReadLine();
    }

    private void AddNewContact()
    {
        Console.Clear();
        Console.Write("Enter name: ");
        var name = _input.GetInput();
        Console.Write("Enter phone number: ");
        var phoneNumber = _input.GetInput();
        Console.Write("Enter email: ");
        var email = _input.GetInput();

        var contact = new Contact { Name = name, PhoneNumber = phoneNumber, Email = email };
        _efDbManager.AddNewContact(contact);
    }

    private void EditContact()
    {
        Console.Clear();
        Console.Write("Enter name of contact to edit: ");
        var oldName = _input.GetInput();
        Console.Write("Enter new name: ");
        var newName = _input.GetInput();
        Console.Write("Enter new phone number: ");
        var newPhoneNumber = _input.GetInput();
        Console.Write("Enter new email: ");
        var newEmail = _input.GetInput();

        var oldContact = new Contact { Name = oldName };
        var newContact = new Contact { Name = newName, PhoneNumber = newPhoneNumber, Email = newEmail };
        _efDbManager.UpdateContact(oldContact, newContact);
    }

    private void DeleteContact()
    {
        Console.Clear();
        Console.Write("Enter name of contact to delete: ");
        var contactToDelete = _input.GetInput();

        var contact = new Contact { Name = contactToDelete };
        _efDbManager.DeleteContact(contact);
    }
}
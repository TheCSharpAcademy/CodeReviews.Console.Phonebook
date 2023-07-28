using PhoneBookConsole.Data;
using PhoneBookConsole.Input;
using PhoneBookConsole.Models;

namespace PhoneBookConsole.PhoneBookService;

public class ContactService : IContactService
{
    private readonly IUserInput _input = new UserInput();
    private readonly IDbManager _efDbManager = new EfDbManager();

    public void ViewAllContacts()
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

    public void AddNewContact()
    {
        Console.Clear();
        var name = _input.GetName();
        var phoneNumber = _input.GetPhoneNumber();
        var email = _input.GetEmail();

        var contact = new Contact { Name = name, PhoneNumber = phoneNumber, Email = email };
        _efDbManager.AddNewContact(contact);
    }

    public void EditContact()
    {
        Console.Clear();
        ViewAllContacts();
        Console.WriteLine("Enter name of contact to edit");
        var oldName = _input.GetName();
        Console.WriteLine("Enter new details of contact");
        var newName = _input.GetName();
        var newPhoneNumber = _input.GetPhoneNumber();
        var newEmail = _input.GetEmail();

        var oldContact = new Contact { Name = oldName };
        var newContact = new Contact { Name = newName, PhoneNumber = newPhoneNumber, Email = newEmail };
        _efDbManager.UpdateContact(oldContact, newContact);
    }

    public void DeleteContact()
    {
        Console.Clear();
        ViewAllContacts();
        Console.WriteLine("Enter name of contact to delete");
        var contactToDelete = _input.GetName();

        var contact = new Contact { Name = contactToDelete };
        _efDbManager.DeleteContact(contact);
    }
}
using Phonebook.Models;
using Phonebook.Repositories;
using Phonebook.Views;

namespace Phonebook.Controllers;

public class PhoneBookController
{
    private readonly PhoneBookRepository repository = new();
    private readonly PhoneBookView view = new();

    public void AddContact()
    {
        bool retry;
        do
        {
            var contactName = view.GetContactName();
            var email = view.GetContactEmail();
            var phoneNumber = view.GetPhoneNumber();

            try
            {
                repository.AddContact(new Contact { Name = contactName, Email = email, PhoneNumber = phoneNumber });
                retry = false;
            }
            catch (ArgumentException e)
            {
                view.ShowError(e.Message);
                retry = view.AskConfirm("Retry?");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } while (retry);
    }

    public void UpdateContact()
    {
        var retry = false;
        do
        {
            var contacts = repository.GetAll();
            if (contacts.Count == 0)
            {
                view.ShowError("No contacts found.");
                continue;
            }

            var contact = view.ShowMenu(contacts);

            contact.Name = view.GetContactName(contact.Name);
            contact.Email = view.GetContactEmail(contact.Email);
            contact.PhoneNumber = view.GetPhoneNumber(contact.PhoneNumber);

            try
            {
                repository.UpdateContact(contact);
                retry = false;
            }
            catch (ArgumentException e)
            {
                view.ShowError(e.Message);
                retry = view.AskConfirm("Retry?");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } while (retry);
    }

    public void ListContacts()
    {
        var contacts = repository.GetAll();
        if (contacts.Count == 0)
        {
            view.ShowError("No contacts found.");
            return;
        }

        view.ShowTable(contacts);
    }

    public void DeleteContact()
    {
        var contacts = repository.GetAll();
        if (contacts.Count == 0)
        {
            view.ShowError("No contacts found.");
            return;
        }

        var contact = view.ShowMenu(contacts);
        repository.DeleteContact(contact);
    }
}
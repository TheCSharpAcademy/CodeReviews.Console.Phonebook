using PhoneBook.DouglasFir.Models;
using PhoneBook.DouglasFir.Services;
using PhoneBook.DouglasFir.Utilities;
using System.Text.RegularExpressions;

namespace PhoneBook.DouglasFir.Handlers;

public class ContactCommandHandler
{
    private readonly ContactService _contactService;

    public ContactCommandHandler(ContactService contactService)
    {
        _contactService = contactService;
    }

    public void HandleAddContact()
    {
        var name = UserInput.PromptForNonEmptyString("Enter name:");
        var phoneNumber = UserInput.PromptForNonEmptyString("Enter phone number:");

        var contact = new Contact { Name = name, PhoneNumber = phoneNumber };
        _contactService.AddContact(contact);

        Util.DisplaySuccessMessage("Contact added successfully!");
    }

    public void HandleUpdateContact()
    {
        var contacts = _contactService.GetAllContacts();

        if (contacts.Count() == 0)
        {
            Util.DisplayWarningMessage("No contacts found.");
            return;
        }

        TableVisualizationEngine.ShowContactsTableWithId(_contactService.GetAllContacts());

        var id = UserInput.PromptForInteger("Enter the ID of the contact you want to update:");

        try
        {
            var contact = _contactService.GetContactById(id);
            contact!.Name = UserInput.PromptForNonEmptyString("Enter name:");
            contact!.PhoneNumber = UserInput.PromptForNonEmptyString("Enter phone number:");

            _contactService.UpdateContact(contact);
        }
        catch (Exception ex)
        {
            Util.DisplayExceptionErrorMessage("An error occurred while updating the contact.", ex.Message);
            return;
        }

        Util.DisplaySuccessMessage("Contact updated successfully!");
    }

    public void HandleDeleteContact()
    {
        var contacts = _contactService.GetAllContacts();

        if (contacts.Count() == 0)
        {
            Util.DisplayWarningMessage("No contacts found.");
            return;
        }

        TableVisualizationEngine.ShowContactsTableWithId(_contactService.GetAllContacts());

        var id = UserInput.PromptForInteger("Enter the ID of the contact you want to delete:");

        try
        {
            _contactService.DeleteContact(id);
        }
        catch (Exception ex)
        {
            Util.DisplayExceptionErrorMessage("An error occurred while deleting the contact.", ex.Message);
            return;
        }

        Util.DisplaySuccessMessage("Contact deleted successfully!");
    }

    public void HandleViewContacts()
    {
        var contacts = _contactService.GetAllContacts();

        if (contacts.Count() == 0)
        {
            Util.DisplayWarningMessage("No contacts found.");
            return;
        }

        TableVisualizationEngine.ShowContactsTable(_contactService.GetAllContacts());
    }

    private bool IsValidEmail(string email)
    {
        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }
}

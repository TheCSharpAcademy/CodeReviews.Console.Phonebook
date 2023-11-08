using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Phonebook.K_MYR.Models;
using Spectre.Console;

namespace Phonebook.K_MYR;

internal class ContactsService
{
    internal void AddContact()
    {
        var name = GetContactName();
        var phoneNumber = GetPhoneNumberInput();
        var emailAdress = GetEmailInput();
        var categoryId = GetCategoryInput().CategoryId;

        try
        {
            using var db = new ContactsContext();
            db.Add(new Contact
            {
                Name = name,
                EmailAdress = emailAdress,
                PhoneNumber = phoneNumber,
                CategoryId = categoryId
            });
            db.SaveChanges();
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
        }
    }

    internal void DeleteContact()
    {
        try
        {
            using var db = new ContactsContext();

            if (!db.Contacts.Any())
            {
                AnsiConsole.Write(new Panel("No Contacts Were Found. Press Any Key To Return").BorderColor(Color.DarkOrange3_1));
                Console.ReadKey();
                return;
            }

            var contact = GetContactInput("Which Contact Do You Want To Delete?");
            db.Remove(contact);
            db.SaveChanges();
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
        }
    }

    internal void UpdateContact()
    {
        try
        {
            using var db = new ContactsContext();

            if (!db.Contacts.Any())
            {
                AnsiConsole.Write(new Panel("No Contacts Were Found. Press Any Key To Return").BorderColor(Color.DarkOrange3_1));
                Console.ReadKey();
                return;
            }

            var contact = GetContactInput("Which Contact Do You Want To Update?");

            if (AnsiConsole.Confirm("Update Name?"))
                contact.Name = GetContactName();

            if (AnsiConsole.Confirm("Update Email-Adress?"))
                contact.EmailAdress = GetEmailInput();

            if (AnsiConsole.Confirm("Update Phone Number?"))
                contact.PhoneNumber = GetPhoneNumberInput();

            if (AnsiConsole.Confirm("Update Category?"))
                contact.CategoryId = GetCategoryInput().CategoryId;
                

            db.Update(contact);
            db.SaveChanges();
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
        }
    }

    internal List<ContactDTO> GetAllContacts()
    {
        try
        {
            using var db = new ContactsContext();
            return db.Contacts
                        .Include(c => c.Category)
                        .Select(c => new ContactDTO
                        {
                            FullName = c.Name,
                            EmailAdress = c.EmailAdress,
                            PhoneNumber = c.PhoneNumber,
                            CategoryName = c.Category!.Name
                        })
                        .ToList();
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
            return new List<ContactDTO>();
        }
    }

    internal ContactDTO? GetContact()
    {
        try
        {
            using var db = new ContactsContext();

            if (!db.Contacts.Any())
                return null;

            var contact = GetContactInput("Which Contact Do You Want To View?");

            return new ContactDTO { FullName = contact.Name, EmailAdress = contact.EmailAdress, CategoryName = contact.Category!.Name, PhoneNumber = contact.PhoneNumber };
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
            return null;
        }
    }

    private Contact GetContactInput(string message = "")
    {
        Console.Clear();

        using var db = new ContactsContext();

        var contacts = db.Contacts.Include(c => c.Category);

        var contact = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                            .AddChoices(contacts.Select(x => x.Name).ToArray())
                                            .Title(message));
        return contacts.Single(x => x.Name == contact);
    }

    private Category GetCategoryInput()
    {
        Console.Clear();

        using var db = new ContactsContext();

        var category = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                            .Title("Choose a Category:")
                                            .AddChoices(db.Categories.Select(c => c.Name).ToList()));

        return db.Categories.Single(x => x.Name == category);
    }

    private string GetEmailInput()
    {
        string email;

        do
        {
            email = AnsiConsole.Ask<string>("Please Enter A Valid Email Adress:").Trim();
        } while (!Validator.EmailIsValid(email));

        return email;
    }

    private string GetPhoneNumberInput()
    {
        string phoneNumber;
        do
        {
            phoneNumber = AnsiConsole.Ask<string>("Please Enter A Valid Phone Number (Format: +[[Country Code]][[Number]]): ").Trim();
        } while (!Validator.PhoneNumberIsValid(phoneNumber));
        return phoneNumber;
    }

    private string GetContactName()
    {
        string contactName;
        do
        {
            contactName = AnsiConsole.Ask<string>("Contact Name (max 50):").Trim();
        } while (contactName.Length > 50);

        return contactName;
    }
}

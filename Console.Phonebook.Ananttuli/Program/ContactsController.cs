using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Program.Contacts;
using Program.ContactsCategories;
using Spectre.Console;

namespace Program;

public class ContactsController
{
    public static async Task ListContacts(PhonebookContext db)
    {
        var contacts = await db.Contacts
            .Include(c => c.ContactCategories)
            .ThenInclude(c => c.Category)
            .ToListAsync();

        var table = new Spectre.Console.Table();

        table.AddColumns(["Id", "Name", "Email", "Phone", "Category(s)"]);

        foreach (var contact in contacts)
        {
            var id = contact.ContactId.ToString();
            var name = contact.Name;
            var email = contact.Email ?? "-";
            var phone = contact.PhoneNumber ?? "-";
            string categories = string.Join(
                ", ",
                contact.ContactCategories
                    .Select(contactCategory => contactCategory.Category.Name) ?? []
            );

            table.AddRow([id, name, email, phone, categories]);
        }

        AnsiConsole.Write(table);
    }

    public static int? PromptForContactId()
    {
        string contactIdInput = AnsiConsole.Prompt(
            new TextPrompt<string>(
                "Contact ID? [grey](Leave empty to cancel)[/]"
            )
            .Validate(input => int.TryParse(input, out int res))
            .AllowEmpty()
            .DefaultValue("")
        );

        if (contactIdInput.Trim().Equals("") || contactIdInput == null)
        {
            return null;
        }

        return int.Parse(contactIdInput);
    }

    public static async Task<Contact?> FindContactById(PhonebookContext db, int contactId)
    {
        var existingContact = await db.Contacts.FindAsync(contactId);

        if (existingContact == null)
        {
            AnsiConsole.MarkupLine($"[red]Could not find ID {contactId}[/]");
            return null;
        }

        return existingContact;
    }

    public static async Task CreateOrUpdateContact(PhonebookContext db, int? contactId = null)
    {
        Contact? existingContact = null;

        if (contactId.HasValue)
        {
            existingContact = await FindContactById(db, contactId.Value);
        }

        string name = AnsiConsole.Ask<string>("Full name");
        string email = ReadEmail();
        string phone = ReadPhone();
        List<ContactCategory> contactCategories = [];

        if (existingContact != null)
        {
            existingContact.Name = string.IsNullOrEmpty(name) ? existingContact.Name : name;
            existingContact.Email = string.IsNullOrEmpty(email) ? existingContact.Email : null;
            existingContact.PhoneNumber = string.IsNullOrEmpty(email) ? existingContact.PhoneNumber : null;
            existingContact.ContactCategories.AddRange(contactCategories);

            db.Update(existingContact);
        }
        else
        {
            db.Add(
                new Contact
                {
                    Name = name,
                    Email = string.IsNullOrWhiteSpace(email) ? null : email,
                    PhoneNumber = string.IsNullOrWhiteSpace(phone) ? null : phone,
                    ContactCategories = contactCategories
                }
            );
        }
        await db.SaveChangesAsync();
    }

    public async static Task EditContact(PhonebookContext db)
    {
        await ListContacts(db);

        AnsiConsole.MarkupLine("Edit contact");
        int? contactId = PromptForContactId();

        if (contactId == null)
        {
            return;
        }
        await CreateOrUpdateContact(db, contactId);
    }

    public async static Task DeleteContact(PhonebookContext db)
    {
        await ListContacts(db);

        AnsiConsole.MarkupLine("Delete contact");
        int? contactId = PromptForContactId();

        if (!contactId.HasValue)
        {
            return;
        }

        var existingContact = await FindContactById(db, contactId.Value);

        if (existingContact == null)
        {
            return;
        }

        db.Contacts.Remove(existingContact);
        await db.SaveChangesAsync();
    }

    private static string ReadEmail()
    {
        return AnsiConsole.Prompt(
                new TextPrompt<string>("Email (E.g. x@y.com) [grey](Press enter to skip)[/]")
                .AllowEmpty()
                .Validate(input =>
                {
                    var isValid = string.IsNullOrWhiteSpace(input) ||
                        new System.ComponentModel
                            .DataAnnotations.EmailAddressAttribute()
                            .IsValid(input);

                    return isValid ? ValidationResult.Success() :
                        ValidationResult.Error();
                })
                .ValidationErrorMessage("Please enter correct format")
            );
    }

    private static string ReadPhone()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Phone (Enter only digits or '+') [grey](Press enter to skip)[/]")
            .AllowEmpty()
            .Validate(input =>
            {
                var isValid = string.IsNullOrWhiteSpace(input) ||
                new System.ComponentModel
                    .DataAnnotations.PhoneAttribute()
                    .IsValid(input);

                return isValid ? ValidationResult.Success() :
                    ValidationResult.Error();
            })
            .ValidationErrorMessage("Please enter correct format")
                );
    }

}
using Microsoft.EntityFrameworkCore;
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

    private static Contact PromptContactInput(Contact? existingContact = null)
    {
        string name = AnsiConsole.Ask<string>("Full name", existingContact?.Name ?? "");
        string email = ReadEmail(existingContact?.Email);
        string phone = ReadPhone(existingContact?.PhoneNumber);
        List<ContactCategory> contactCategories = [];

        return new Contact
        {
            Name = name,
            Email = string.IsNullOrWhiteSpace(email) ? null : email,
            PhoneNumber = string.IsNullOrWhiteSpace(phone) ? null : phone,
            ContactCategories = contactCategories
        };
    }

    public static async Task CreateContact(PhonebookContext db)
    {
        Contact contactInput = PromptContactInput();

        db.Add(contactInput);
        await db.SaveChangesAsync();
    }

    public static async Task UpdateContact(PhonebookContext db, int contactId)
    {
        Contact? existingContact = await FindContactById(db, contactId);

        if (existingContact == null)
        {
            return;
        }

        Contact contactInput = PromptContactInput(existingContact);


        existingContact.Name = contactInput.Name ?? existingContact.Name;
        existingContact.Email = contactInput.Email ?? existingContact.Email ?? null;
        existingContact.PhoneNumber = contactInput.PhoneNumber ?? existingContact.PhoneNumber ?? null;
        existingContact.ContactCategories.AddRange(contactInput.ContactCategories);

        db.Update(existingContact);
        await db.SaveChangesAsync();
    }

    public async static Task EditContact(PhonebookContext db)
    {
        await ListContacts(db);

        AnsiConsole.MarkupLine("Edit contact");
        int? contactId = PromptForContactId();

        if (!contactId.HasValue || contactId == null)
        {
            return;
        }

        await UpdateContact(db, contactId.Value);
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

    private static string ReadEmail(string? existingEmail)
    {
        return AnsiConsole.Prompt(
                new TextPrompt<string>("Email (E.g. x@y.com) [grey](Press enter to skip)[/]")
                .DefaultValue(existingEmail ?? "")
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

    private static string ReadPhone(string? existingPhone)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Phone (Enter only digits or '+') [grey](Press enter to skip)[/]")
            .DefaultValue(existingPhone ?? "")
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
using Spectre.Console;
using PhoneBook.Data;
using PhoneBook.Models;
using NumberVerifierLibrary;
using EmailVerifierLibrary;

namespace PhoneBook.UserInterfaceControllers;

internal class AddContactMenu
{
    private const double EmailConfidenceThreshold = 0.4;
    private readonly Contact _newContact;
    private PhoneBookContext PhoneBookDb { get; set; }
    public AddContactMenu(PhoneBookContext phoneBookDb)
    {
        PhoneBookDb = phoneBookDb;
        _newContact = new Contact();
    }

    public async Task PromptNewContact()
    {

        AnsiConsole.MarkupLine("[bold green]New Contact\n[/]");

        _newContact.ContactName = AnsiConsole.Ask<string>("Contact Name:");
        _newContact.ContactTitle = AnsiConsole.Ask<string>("Contact Title E.g.(Family,Work,..):");

        _newContact.ContactPhone = AnsiConsole.Ask<string>("Contact Phone Without Country Prefix:");
        var countryCode = AnsiConsole.Ask<string>("Country Code (First two letters of your country name -Jordan = JO, United States = US)\nIF YOU ARE HAVING TROUBLE KNOWING YOUR COUNTRY CODE look it up on google:");
        _newContact.ContactPhoneStatus = await VerifiyPhoneNumber(countryCode);

        _newContact.ContactEmail = AnsiConsole.Ask<string>("Contact Email:");
        _newContact.ContactEmailStatus = await VerifiyEmail();

        await SaveNewContactAsync();
        IndicateAddSuccess();
    }

    private async Task<bool> VerifiyPhoneNumber(string countryCode)
    {
        var validatePhone = new ValidatePhoneNumber(countryCode, _newContact.ContactPhone);

        if (!validatePhone.IsPhoneNumberFormatValid())
            return false;

        bool isValid = await validatePhone.IsPhoneNumberValidAsync();
        string status = isValid ? "[green]Valid[/]" : "[red]Invalid[/]";
        AnsiConsole.MarkupLine($"This number {_newContact.ContactPhone} is {status}");

        return isValid;
    }
    private async Task<bool> VerifiyEmail()
    {
        var validateEmail = new ValidateEmailAddress(_newContact.ContactEmail);
        await validateEmail.InitializeAsync();

        if (!validateEmail.IsEmailAddressFormatValid())
            return false;

        var correctedEmail = await validateEmail.CheckEmailPossibleTypoFixAsync();
        if (!string.IsNullOrEmpty(correctedEmail))
            _newContact.ContactEmail = correctedEmail;

        var confidenceScore = validateEmail.GetEmailAddressValidConfidenceScore();
        if ((confidenceScore > EmailConfidenceThreshold) && (validateEmail.EmailCanRecieveMail()))
            return true;
        return false;
    }

    private async Task SaveNewContactAsync()
    {
        await PhoneBookDb.AddAsync(_newContact);
        await PhoneBookDb.SaveChangesAsync();
    }

    private void IndicateAddSuccess()
    {
        AnsiConsole.MarkupLine("\n[green]Contact added successfully![/]");
        AnsiConsole.MarkupLine("(Press Enter to continue)");
        Console.ReadLine();
    }
}
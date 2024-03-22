using PhoneBook.Dejmenek.Enums;
using PhoneBook.Dejmenek.Models;
using Spectre.Console;

namespace PhoneBook.Dejmenek.Services;

public class UserInteractionService
{
    static string[] countryList = {
        "AC", "AD", "AE", "AF", "US", "AG", "AI", "AS", "BB", "BM", "BS", "CA",
        "DM", "DO", "GD", "GU", "JM", "KN", "KY", "LC", "MP", "MS", "PR", "SX",
        "TC", "TT", "VC", "VG", "VI", "AL", "AM", "AO", "AR", "AT", "AU", "CC",
        "CX", "AW", "FI", "AX", "AZ", "BA", "BD", "BE", "BF", "BG", "BH", "BI", "BJ",
        "GP", "BL", "MF", "BN", "BO", "CW", "BQ", "BR", "BT", "BW", "BY", "BZ", "CD",
        "CF", "CG", "CH", "CI", "CK", "CL", "CM", "CN", "CO", "CR", "CU", "CV", "CY",
        "CZ", "DE", "DJ", "DK", "DZ", "EC", "EE", "EG", "MA", "EH", "ER", "ES", "ET",
        "FJ", "FK", "FM", "FO", "FR", "GA", "GB", "GG", "IM", "JE", "GE", "GF", "GH",
        "GI", "GL", "GM", "GN", "GQ", "GR", "GT", "GW", "GY", "HK", "HN", "HR", "HT",
        "HU", "ID", "IE", "IL", "IN", "IO", "IQ", "IR", "IS", "IT", "VA", "JO", "JP",
        "KE", "KG", "KH", "KI", "KM", "KP", "KR", "KW", "RU", "KZ", "LA", "LB", "LI",
        "LK", "LR", "LS", "LT", "LU", "LV", "LY", "MC", "MD", "ME", "MG", "MH", "MK",
        "ML", "MM", "MN", "MO", "MQ", "MR", "MT", "MU", "MV", "MW", "MX", "MY", "MZ",
        "NA", "NC", "NE", "NF", "NG", "NI", "NL", "NO", "SJ", "NP", "NR", "NU", "NZ",
        "OM", "PA", "PE", "PF", "PG", "PH", "PK", "PL", "PM", "PS", "PT", "PW", "PY",
        "QA", "RE", "YT", "RO", "RS", "RW", "SA", "SB", "SC", "SD", "SE", "SG", "SH",
        "TA", "SI", "SK", "SL", "SM", "SN", "SO", "SR", "SS", "ST", "SV", "SY", "SZ",
        "TD", "TG", "TH", "TJ", "TK", "TL", "TM", "TN", "TO", "TR", "TV", "TW", "TZ",
        "UA", "UG", "UY", "UZ", "VE", "VN", "VU", "WF", "WS", "XK", "YE", "ZA", "ZM", "ZW"
    };

    public string GetContactName()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter contact's name")
                .ValidationErrorMessage("Your input must not be empty!")
                .Validate(Validation.IsValidString)
            );
    }

    public string GetCategoryName()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter category's name")
                .ValidationErrorMessage("Your input must not be empty")
                .Validate(Validation.IsValidString)
            );
    }

    public Category GetCategory(List<Category> categories)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<Category>()
                    .Title("Select contact's category")
                    .UseConverter(category => $"{category.Name}")
                    .AddChoices(categories)
            );
    }

    public void GetUserInputToContinue()
    {
        AnsiConsole.MarkupLine("Press Enter to continue...");
        Console.ReadLine();
    }

    public string GetPhoneNumber()
    {
        string chosenCountry = GetCountry();
        return AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter a phone number with country code, including area code (e.g., +1 (212) 555 1212 for USA, +44 (20) 7946 0123 for UK): ")
                            .Validate((phoneNumber) => Validation.IsValidPhoneNumber(phoneNumber, chosenCountry))
                    );
    }

    public string GetCountry()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select country code")
                .AddChoices(countryList)
        );
    }

    public string GetEmail()
    {
        return AnsiConsole.Prompt(
                new TextPrompt<string>("Enter email for example test@gmail.com")
                    .ValidationErrorMessage("This is not a valid email format!")
                    .Validate(Validation.IsValidEmail)
            );
    }

    public string GetEmailBody()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter details in the message body")
            );
    }

    public string GetEmailSubject()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the subject line for your email")
            );
    }

    public string GetUsername()
    {
        return AnsiConsole.Prompt(
                new TextPrompt<string>("Enter username")
                    .ValidationErrorMessage("Your input must not be empty!")
                    .Validate(Validation.IsValidString)
            );
    }

    public string GetPassword()
    {
        return AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter password")
                        .ValidationErrorMessage("Your input must not be empty!")
                        .Validate(Validation.IsValidString)
                );
    }

    public bool GetConfirmation(string title)
    {
        return AnsiConsole.Confirm(title);
    }

    public Contact GetContact(List<Contact> contacts)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<Contact>()
                    .Title("Select your contact")
                    .UseConverter(contact => $"{contact.Name} {contact.PhoneNumber}")
                    .AddChoices(contacts)
            );
    }

    public MenuOptions GetMenuOption()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(Enum.GetValues<MenuOptions>())
            );
    }

    public ManageCategoriesOptions GetManageCategoriesOption()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<ManageCategoriesOptions>()
                    .Title("What would you like to do with contact's categories?")
                    .AddChoices(Enum.GetValues<ManageCategoriesOptions>())
            );
    }

    public ManageContactsOptions GetManageContactsOptions()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<ManageContactsOptions>()
                    .Title("What would you like to do with contacts?")
                    .AddChoices(Enum.GetValues<ManageContactsOptions>())
            );
    }
}

using Spectre.Console;
using System.Net.Mail;
using PhoneNumbers;
using System.Globalization;
using Phonebook.Models;
using Phonebook.Data;

namespace Phonebook;

public class UserInput
{
    public int MainMenu()
    {
        Console.Clear();
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose an option:")
                .AddChoices(
                    "0 - Exit Application",
                    "1 - Add New Contact",
                    "2 - Delete Contact",
                    "3 - Update Contact",
                    "4 - View Contacts",
                    "5 - Add Category",
                    "6 - View Categories",
                    "7 - Delete Category",
                    "8 - Update Category",
                    "9 - Send Email"
                ));
        int menuChoice = int.Parse(choice.Split('-')[0].Trim());

        return menuChoice;
    }

    public string GetName(string type)
    {
        return AnsiConsole.Ask<string>($"Enter {type} name: ");
    }

    public string GetContactEmail()
    {
        string message = "Enter a valid email address (in format: example@example.com): ";

        string nameChoice = AnsiConsole.Prompt(
        new TextPrompt<string>(message)
        .Validate((n) =>
        {
            try
            {
                var email = new MailAddress(n);
                return ValidationResult.Success();
            }
            catch (FormatException)
            {
                return ValidationResult.Error($"[red]Enter a valid email. Please.[/]");
            }
        }));
        return nameChoice;
    }

    public string GetCountryCode()
    {
        string code = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter two character country code (e.g. US or FR)")
        .Validate((n) =>
        {
            try
            {
                var region = new RegionInfo(n.ToUpper());
                return ValidationResult.Success();
            }
            catch (ArgumentException)
            {
                return ValidationResult.Error($"[red]Enter a valid country code. Please.[/]");
            }
        }));
        return code.ToUpper();
    }

    public string GetPhoneNumber()
    {
        string countryCode = GetCountryCode();
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        string number = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter a valid phone number")
        .Validate((n) =>
        {
            var testNumber = phoneNumberUtil.Parse(n, countryCode);
            if (phoneNumberUtil.IsValidNumber(testNumber))
                return ValidationResult.Success();
            else
                return ValidationResult.Error($"[red]Enter a valid phone number for country code {countryCode}. Seriously.[/]");
        }));
        var phoneNumber = phoneNumberUtil.Parse(number, countryCode);
        return phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
    }

    internal Contact GetContact(List<Contact> contacts, string message)
    {
        Console.Clear();
        Contact chosenContact = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
                .Title(message)
                .UseConverter(contact => contact.Name)
                .AddChoices(contacts));

        return chosenContact;
    }

    internal Category GetCategory()
    {
        Console.Clear();
        List<Category> categories = CategoryDataManager.GetCategories();
        categories.Add(new Category { Name = "Create a new category" });
        Category category = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
                .Title("Choose a category: ")
                .UseConverter(category => category.Name)
                .AddChoices(categories));
        return category;
    }

    internal bool GetConfirmation(string message)
    {
        return AnsiConsole.Confirm(message);
    }

    internal int ChooseContactToUpdate()
    {
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose what to change:")
                .AddChoices(
                    "0 - Back to Main Menu (uncommitted changes will be lost!)",
                    "1 - Name",
                    "2 - Phone Number",
                    "3 - Email",
                    "4 - Category",
                    "5 - Commit Changes"
                ));

        return int.Parse(choice.Split('-')[0].Trim());
    }

    internal bool BackToMainMenu()
    {
        Console.WriteLine($"Press 0 to return to main menu");
        ConsoleKeyInfo button = Console.ReadKey(true);
        if (button.Key == ConsoleKey.NumPad0 || button.Key == ConsoleKey.D0)
            return true;

        return false;
    }

    internal void PressToContinue()
    {
        Console.WriteLine($"Press any key to continue...");
        Console.Read();
    }

    internal Email GetEmailDetails()
    {
        Email email = new Email(
            EmailDataManager._config["EmailSettings:Username"] ?? "N/A",
            "",
            AnsiConsole.Ask<string>($"Enter email Subject: "),
            AnsiConsole.Ask<string>($"Enter email Body: ")
        );
        return email;
    }

    internal bool Confirm(string prompt)
    {
        bool confirmation = false;
        confirmation = AnsiConsole.Prompt(
        new TextPrompt<bool>(prompt)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }
}

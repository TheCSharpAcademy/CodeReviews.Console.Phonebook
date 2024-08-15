using jollejonas.Phonebook.Models;
using jollejonas.Phonebook.Validation;
using Spectre.Console;
using System.Globalization;

namespace jollejonas.Phonebook.UserInput;
public class ContactInput
{
    public static string GetValidatedInput(string prompt, Func<string, bool> validationFunction)
    {
        while (true)
        {
            Console.WriteLine($"{prompt} (or 'q' to cancel):");
            string input = Console.ReadLine().Trim();
            if (input.Equals("q", StringComparison.OrdinalIgnoreCase)) return null;

            if (validationFunction(input))
            {
                return input;
            }
            else
            {
                Console.WriteLine($"Invalid input. Please try again.");
            }
        }
    }

    public static string GetName()
    {
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(GetValidatedInput("Write contact name", InputValidation.IsValidName));
    }

    public static string GetPhoneNumber()
    {
        return GetValidatedInput("Write contact phonenumber", InputValidation.IsValidPhoneNumber);
    }

    public static string GetEmail()
    {
        return GetValidatedInput("Write contact email", InputValidation.IsValidEmail);
    }

    public static string GetNote()
    {
        return GetValidatedInput("Write contact note", InputValidation.IsValidNote);
    }
    public static int GetCategoryId(List<Category> categories)
    {
        var categorySelection = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
                .Title("Select category")
                .PageSize(6)
                .AddChoices(categories)
                .UseConverter(c => c.Name)
        );

        return categorySelection.Id;
    }

    public static Contact GetContact(List<Contact> contacts)
    {
        if (contacts == null) return null;
        var contactSelection = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
                .Title("Select contact")
                .PageSize(6)
                .AddChoices(contacts)
                .UseConverter(c => c.Name)
        );

        return contactSelection;
    }

    public static bool ConfirmEditDelete()
    {
        var confirm = AnsiConsole.Confirm("Are you sure you want to edit/delete this contact?");

        return confirm;
    }

    public static string GetSubject()
    {
        Console.WriteLine("Write email subject:");
        return Console.ReadLine();
    }

    public static string GetBody()
    {
        Console.WriteLine("Write your message:");
        return Console.ReadLine();
    }
}

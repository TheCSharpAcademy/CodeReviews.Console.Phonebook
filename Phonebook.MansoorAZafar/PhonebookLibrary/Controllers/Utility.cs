using PhonebookLibrary.Databases;
using PhonebookLibrary.Models;
using Spectre.Console;
using System.Text.RegularExpressions;
namespace PhonebookLibrary.Controllers;


internal static class Utility
{
    internal static T GetSelectionInput<T>(T[] enumValues, Func<T, string>? alternateNames = null) where T: notnull
    {
        return AnsiConsole.Prompt
            (
                new SelectionPrompt<T>()
                .Title("Select your choice")
                .PageSize(15)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(enumValues)
                .UseConverter
                (
                    item => alternateNames != null
                                            ? alternateNames(item) 
                                            : item.ToString()
                                            ?? string.Empty
                )
            );
    }

    internal static void PressToContinue()
    {
        System.Console.WriteLine("\nPress any key to continue...");
        System.Console.ReadLine();
    }

    internal static Contact CreateContact()
    {
        string name = AnsiConsole.Prompt<string>(
            new TextPrompt<string>("What is the Contact name\n> ")
        );

        string email = GetEmail();

        string phoneNumber = GetPhoneNumber();

        return new Contact(name, email, phoneNumber);
    }

    private static string GetPhoneNumber()
    {
        string? res;
        System.Console.Write("Please Enter your Phone number without any spaces or -'s\n(ex. 6258889595)\n> ");

        while((bool)(res = System.Console.ReadLine())?.Any(ch => Char.IsLetter(ch)) 
            || res.Length != 10)
            System.Console.Write("Please enter a valid Phone Number\n> ");
        
        return res ?? "N/A";
    }

    private static string GetEmail()
    {
        string? res;
        System.Console.Write("What is the Contact email\n> ");
        const string pattern = @"@.+\.+.";
        Regex regex = new(pattern);

        while (!regex.IsMatch(res = System.Console.ReadLine() ?? "N/A"))
            System.Console.Write("Invalid Email Address\nEnsure this format [username]@[domain].[top-level domain]\n> ");

        return res ?? "N/A";
    }

    internal static int GetValidID(string text="Please enter the ID to be deleted\n> ")
    {
        int id = -1;
        System.Console.Write(text);
        while (!int.TryParse(System.Console.ReadLine(), out id)
            || !Database.IDExists(id))
        {
            if (id == 0) return -1;
            System.Console.Write("Please enter a valid ID\n> ");
        }
            
        return id;
    }
}


using Microsoft.IdentityModel.Tokens;
using Phonebook.Console.Controllers;
using Phonebook.Console.Helpers;
using Phonebook.Console.Models;
using Spectre.Console;

namespace Phonebook.Console.UserInterface;

public class UI {
    public static string GetPhoneNumber() => AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the contact's [green]phone number[/][grey] formatted as '555-555-5555'[/]:")
            .ValidationErrorMessage("[red]That's not a valid phone number[/]")
            .Validate(AppValidator.IsValidPhoneNumber));

    public static string GetPhoneNumberOrDefault(string defaultPhoneNumber) { 
        var response = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter the contact's [green]phone number[/][grey] formatted as '555-555-5555'. Or leave as '{defaultPhoneNumber}'[/]:")
            .ValidationErrorMessage("[red]That's not a valid phone number[/]")
            .AllowEmpty()
            .Validate(AppValidator.IsValidPhoneNumberOrEmpty));

        if (response.IsNullOrEmpty()) {
            return defaultPhoneNumber;
        } else {
            return response;
        }
    }

    public static string GetEmail(string prompt) => AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .ValidationErrorMessage("[red]That's not a valid email address[/]")
            .Validate(AppValidator.IsValidEmail));

    public static string GetEmailOrDefault(string defaultEmail) { 
        var response = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter the contact's [green]email[/][grey] formatted as 'name@gmail.com'. Or leave as '{defaultEmail}'[/]:")
            .ValidationErrorMessage("[red]That's not a valid email address[/]")
            .AllowEmpty()
            .Validate(AppValidator.IsValidEmailOrEmpty)); 

        if (response.IsNullOrEmpty()) {
            return defaultEmail;
        } else {
            return response;
        }
    }

    public static string GetName() => AnsiConsole.Prompt(
        new TextPrompt<string>("Enter the contact's [green]full name[/]")
        .ValidationErrorMessage("[red]That's not a full name[/]")
        .Validate(AppValidator.IsValidFullName));

    public static string GetResponse(string prompt) => AnsiConsole.Prompt(
        new TextPrompt<string>(prompt)
        .AllowEmpty());

   public static string GetNameOrDefault(string defaultName) { 
        var response = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter the contact's [green]full name. Or leave as '{defaultName}'[/]")
            .ValidationErrorMessage("[red]That's not a full name[/]")
            .AllowEmpty()
            .Validate(AppValidator.IsValidFullNameOrEmpty)); 

        if (response.IsNullOrEmpty()) {
            return defaultName;
        } else {
            return response;
        }
   }

    public static string MainMenu() {
        AnsiConsole.Clear(); 
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[yellow]Contacts[/] [green]Main Menu[/]")
            .AddChoices([
                MainController.Exit, MainController.CreateContact, MainController.UpdateContact, 
                MainController.DeleteContact, MainController.ViewContacts, MainController.SendEmail
            ]));
    }

    public static void ConfirmMessage(string message)
    {
        AnsiConsole.Write(message + " Press 'enter' to continue");
        AnsiConsole.Prompt(new TextPrompt<string>("").AllowEmpty()); 
    }

    public static void ViewContacts(List<Contact> contacts)
    {
        var table = new Table();

        table.AddColumns("ID", "Name", "Email", "Phone Number");

        foreach(var contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name ?? "", contact.Email ?? "", contact.PhoneNumber ?? "");
        }

        AnsiConsole.Write(table);
    }

    public static int GetValidNumber(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>(prompt)
        );
    }

    public static void Write(string message) => AnsiConsole.MarkupLine(message);
}
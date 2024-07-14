using PhoneBook.Extension;
using PhoneBook.Models;
using PhoneBook.Utilities;
using PhoneNumbers;
using Spectre.Console;

namespace PhoneBook.UserInteraction
{
    public class UserInteractions
    {
        public static void Exit()
        {
            AnsiConsole.MarkupLine("\n[bold][red]Exiting from application[/][/]\n");
            Environment.Exit(0);
        }

        public static Category GetCategoryDetails()
        {
            var name = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter [green]Category Name[/][/]:")
                    .PromptStyle("blue")
                    .ValidationErrorMessage("[red]Name cannot be empty[/]")
                    .Validate(name =>
                    {
                        return !string.IsNullOrWhiteSpace(name);
                    })
            );
            var category = new Category
            {
                Name = name.TrimAndLower().ToTitleCase() ?? "",
            };

            return category;
        }

        public static Contact GetContactDetails()
        {
            var name = AnsiConsole.Prompt(
                new TextPrompt<string>("\n[bold]Enter [green]Contact Name[/][/]:")
                    .PromptStyle("blue")
                    .ValidationErrorMessage("[red]Name cannot be empty[/]")
                    .Validate(name =>
                    {
                        return!string.IsNullOrWhiteSpace(name);
                    })
            );

            var formattedNumber = string.Empty;
            var phoneNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter [green]Phone Number(Format: (+code)-number)[/][/]:")
                    .PromptStyle("blue")
                    .ValidationErrorMessage("[red]Please enter a valid phone number[/]")
                    .Validate(number =>
                    {
                        return ValidationHelper.PhoneNumberIsValid(number, out formattedNumber);
                    })
            );
            
            var email = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter [green]Email Address[/][/]:")
                    .PromptStyle("blue")
                    .ValidationErrorMessage("[red]Please enter a valid email[/]")
                    .Validate(ValidationHelper.EmailIsValid)
            );

            var contact = new Contact
            {
                Name = name.TrimAndLower().ToUpper(),
                PhoneNumber = formattedNumber,
                Email = email,
                Category = new Category()
            };

            return contact;
        }
    }
}
using PhoneBook.Models;
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
                Name = name
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

            var phoneNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter [green]Phone Number[/][/]:")
                    .PromptStyle("blue")
                    .ValidationErrorMessage("[red]Phone number cannot be empty[/]")
                    .Validate(number =>
                    {
                        return!string.IsNullOrWhiteSpace(number);
                    })
            );
            
            var email = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter [green]Email Address[/][/]:")
                    .PromptStyle("blue")
                    .ValidationErrorMessage("[red]Email address cannot be empty[/]")
                    .Validate(email =>
                    {
                        return!string.IsNullOrWhiteSpace(email);
                    })
            );

            var contact = new Contact
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
                Category = new Category()
            };

            return contact;
        }

        internal static void DisplayContacts(Category category)
        {
            if (category.Contacts != null)
            {
            var table = new Table()
                .AddColumn("Contact ID")
                .AddColumn("Name")
                .AddColumn("Phone Number")
                .AddColumn("Email");

            foreach (var contact in category.Contacts)
            {
                table.AddRow(contact.ContactId.ToString(), contact.Name, contact.PhoneNumber, contact.Email);
            }

            AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold][red]No contacts found in this category.[/]");
            }
        }
    }
}
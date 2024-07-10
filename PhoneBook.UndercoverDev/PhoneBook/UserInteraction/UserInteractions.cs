using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.UserInteraction
{
    public class UserInteractions
    {
        public static void Exit()
        {
            AnsiConsole.WriteLine("Exiting from application\n");
            Environment.Exit(0);
        }

        public static Category GetCategoryDetails()
        {
            var name = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]Category Name[/]:")
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

        public static void GetContactDetails(string message)
        {
            AnsiConsole.WriteLine($"[bold][yellow]{message}[/]");
        }
    }
}
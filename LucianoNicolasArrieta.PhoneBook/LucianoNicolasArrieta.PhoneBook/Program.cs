using LucianoNicolasArrieta.PhoneBook;
using LucianoNicolasArrieta.PhoneBook.Persistence;
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        bool running = true;
        ContactController controller = new ContactController();

        while (running)
        {
            AnsiConsole.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("Welcome to [green]Phone Book App[/], What would you like to do?")
                .AddChoices(
                    MenuOptions.AddContact,
                    MenuOptions.UpdateContact,
                    MenuOptions.DeleteContact,
                    MenuOptions.ViewContacts,
                    MenuOptions.SendEmailThroughGmail,
                    MenuOptions.Exit));

            switch (option)
            {
                case MenuOptions.AddContact:
                    controller.Insert();
                    AnsiConsole.Markup("[underline green]Hello[/]");
                    break;
                case MenuOptions.UpdateContact:
                    controller.Update();
                    break;
                case MenuOptions.DeleteContact:
                    controller.Delete();
                    break;
                case MenuOptions.ViewContacts:
                    controller.ViewAll();
                    break;
                case MenuOptions.SendEmailThroughGmail:
                    EmailSender sender = new EmailSender();
                    sender.sendEmail();
                    break;
                case MenuOptions.Exit:
                    running = false;
                    AnsiConsole.Markup("[bold yellow]See you![/]");
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}

enum MenuOptions
{
    AddContact,
    UpdateContact,
    DeleteContact,
    ViewContacts,
    SendEmailThroughGmail,
    Exit
}
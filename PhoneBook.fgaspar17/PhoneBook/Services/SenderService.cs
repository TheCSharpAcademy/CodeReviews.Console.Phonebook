using PhoneBookLibrary;
using Spectre.Console;

namespace PhoneBook;

public static class SenderService
{
    public static void Send(SenderType senderType)
    {
        MenuPresentation.PresentMenu("[yellow]Sending Message[/]");

        bool isCancelled;
        string name, mail, subject, body, message;

        ContactService.ShowContactTable();

        ExistingModelValidator<string, Contact> existingContact = new ExistingModelValidator<string, Contact>
        {
            ErrorMsg = "Contact Name doesn't exist.",
            GetModel = ContactController.GetContactByName
        };

        (isCancelled, name) = ContactService.AskForContactName(existingContact);
        if (isCancelled) return;

        ISender sender = SenderFactory.CreateSender(senderType, name);

        if (sender == null) return;

        (isCancelled, message) = AskForMessage();
        if (isCancelled) return;

        if (sender.Send(message))
        {
            AnsiConsole.MarkupLine($"[green]Message sent to {name}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed to send the message.[/]");
        }

        Prompter.PressKeyToContinuePrompt();
    }

    private static (bool IsCancelled, string Result) AskForMessage()
    {
        string message = "Enter a Message";
        return Prompter.PromptWithValidation(message);
    }
}
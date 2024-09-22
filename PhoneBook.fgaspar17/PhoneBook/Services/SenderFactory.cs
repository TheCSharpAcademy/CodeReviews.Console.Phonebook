using PhoneBookLibrary;
using Spectre.Console;

namespace PhoneBook;

public static class SenderFactory
{
    public static ISender CreateSender(SenderType senderType, string name)
    {
        bool isValid;
        ISender sender;

        switch (senderType)
        {
            case SenderType.Mail:
                (isValid, string mail) = HasValidMail(name);
                if (!isValid) return null;

                (bool isCancelled, string subject) = AskForSubject();
                if (isCancelled) return null;

                sender = new GmailSender(name, subject, mail);
                break;
            case SenderType.Sms:
                (isValid, string phone) = HasValidPhone(name);
                if (!isValid) return null;

                sender = new TwilioSmsSender(phone);
                break;
            default:
                Console.WriteLine($"Option {senderType} not supported.");
                return null;
        }

        return sender;
    }

    private static (bool IsValid, string Mail) HasValidMail(string name)
    {
        string mail = ContactController.GetContactByName(name).Email;
        if (string.IsNullOrEmpty(mail))
        {
            AnsiConsole.MarkupLine($"[red]The Contact {name} doesn't have a valid mail.[/]");
            Prompter.PressKeyToContinuePrompt();
            return (false, null);
        }

        return (true, mail);
    }

    private static (bool IsValid, string Phone) HasValidPhone(string name)
    {
        string phone = ContactController.GetContactByName(name).PhoneNumber;
        if (string.IsNullOrEmpty(phone))
        {
            AnsiConsole.MarkupLine($"[red]The Contact {name} doesn't have a valid phone number.[/]");
            Prompter.PressKeyToContinuePrompt();
            return (false, null);
        }

        return (true, phone);
    }

    private static (bool IsCancelled, string Result) AskForSubject()
    {
        string message = "Enter a Subject";
        return Prompter.PromptWithValidation(message);
    }
}
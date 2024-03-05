using Phonebook.Controllers;
using Phonebook.Models;
using Spectre.Console;
using static Phonebook.Models.Enums;

namespace Phonebook.Services;

internal class EmailHistoryService
{
    internal static void ShowEmailHistory()
    {
        var exitMenu = false;
        while (!exitMenu)
        {
            Console.Clear();
            var emailHistory = ShowEmailHistoryTable();
            if (emailHistory == null)
            {
                return;
            }
            else
            {

                var option = AnsiConsole.Prompt(
                new SelectionPrompt<EmailHistoryMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    EmailHistoryMenu.ViewSingleEmail,
                    EmailHistoryMenu.Back));
                switch (option)
                {
                    case EmailHistoryMenu.ViewSingleEmail:
                        int id = PickEmailHistory();
                        EmailHistory emailToView = EmailHistoryController.GetSingleEmailById(id);
                        ShowSingleEmailHistory(emailToView);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case EmailHistoryMenu.Back:
                        exitMenu = true;
                        break;
                }
            }
        }

    }

    private static void ShowSingleEmailHistory(EmailHistory emailToView)
    {
        Console.Clear();

        string body = EmailServices.RemoveHtmlTags(emailToView.EmailBody);

        var table = new Spectre.Console.Table()
        .AddColumn($@"Email")
            .AddRow(new Panel($@"To: {emailToView.ToEmail}"))
            .AddRow(new Panel($@"To: {emailToView.ToEmail}"))
            .AddRow(new Panel($@"Subject: {emailToView.Subject}"))
            .AddRow(new Panel($@"Body: {body}"))
            .Width(60)
            .HideHeaders();

        AnsiConsole.Write(table);
    }

    private static int PickEmailHistory()
    {
        Console.Clear();
        ShowEmailHistoryTable();

        List<int> ids = GetIds();

        int id = AnsiConsole.Prompt(
        new TextPrompt<int>("Please put in the ID of the email you wish to view.")
            .ValidationErrorMessage("[red]That's not a valid ID[/]")
            .Validate(id =>
            {
                if (ids.Contains(id))
                {
                    return ValidationResult.Success();
                }
                else
                {
                    return ValidationResult.Error("[red]Not a valid ID[/]");
                }

            }));

        return id;
    }

    private static List<int> GetIds()
    {
        var emailHistory = EmailHistoryController.GetEmailHistory();
        List<int> ids = new List<int>();
        foreach (EmailHistory email1 in emailHistory)
        {
            ids.Add(email1.EmailHistoryId);
        }
        return ids;
    }

    internal static List<EmailHistory> ShowEmailHistoryTable()
    {
        var emailHistory = EmailHistoryController.GetEmailHistory();

        if (emailHistory == null)
        {
            Console.WriteLine("There was an issue pulling history.  Make sure there is history to pull. Press any Key to continue,");
            Console.ReadKey();
            return emailHistory;
        }
        else
        {
            var table = new Spectre.Console.Table();

            table.AddColumns("ID", "Contact", "To Email", "Subject", "Sent");


            foreach (EmailHistory email1 in emailHistory)
            {

                table.AddRow($@"{email1.EmailHistoryId}",
                                    $@"{email1.ContactName}",
                                    $@"{email1.ToEmail}",
                                    $@"{email1.Subject}",
                                    $@"{email1.SentTS}");
            }
            AnsiConsole.Write(table);
        }

        return emailHistory;

    }
}

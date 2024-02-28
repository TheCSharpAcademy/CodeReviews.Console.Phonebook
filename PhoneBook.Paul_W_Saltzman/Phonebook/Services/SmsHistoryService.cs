
using Phonebook.Controllers;
using Phonebook.Models;
using Spectre.Console;
using static Phonebook.Models.Enums;

namespace Phonebook.Services;

internal class SmsHistoryService
{
    internal static void ShowSmsHistory()
    {
        var exitMenu = false;
        while (!exitMenu) 
        {
            Console.Clear();
            var smsHistory = ShowSmsHistoryTable();
            if (smsHistory == null)
            {
                return;
            }
            else
            {

                var option = AnsiConsole.Prompt(
                new SelectionPrompt<SmsHistoryMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    SmsHistoryMenu.ViewSingleText,
                    SmsHistoryMenu.Back));
                switch (option)
                {
                    case SmsHistoryMenu.ViewSingleText:
                        int id = PickTextHistory();
                        SmsHistory smsToView = SmsHistoryController.GetSingleSmsById(id);
                        ShowSingleSmsHistory(smsToView);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case SmsHistoryMenu.Back:
                        exitMenu = true;
                        break;
                }
            }
        }
    }

    private static void ShowSingleSmsHistory(SmsHistory smsToView)
    {
        Console.Clear();

        var table = new Spectre.Console.Table()
        .AddColumn($@"Email")
            .AddRow(new Panel($@"Contact: {smsToView.ContactName}"))
            .AddRow(new Panel($@"To: {smsToView.ToNumber}"))
            .AddRow(new Panel($@"Text: {smsToView.Body}"))
            .AddRow(new Panel($@"Sent: {smsToView.SentTS}"))
            .AddRow(new Panel($@"Sid: {smsToView.MessageSid}"))
            .Width(60)
            .HideHeaders();

        AnsiConsole.Write(table);
    }

    private static int PickTextHistory()
    {
        Console.Clear();
        ShowSmsHistoryTable();

        List<int> ids = GetIds();

        int id = AnsiConsole.Prompt(
        new TextPrompt<int>("Please put in the ID of the Text you wish to view.")
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
        var smsHistory = SmsHistoryController.GetSmsHistory();
        List<int> ids = new List<int>();
        foreach (SmsHistory text in smsHistory)
        {
            ids.Add(text.SMSHistoryId);
        }
        return ids;
    }

    private static List<SmsHistory> ShowSmsHistoryTable()
    {
        List<SmsHistory> smsHistory = SmsHistoryController.GetSmsHistory();

        if (smsHistory == null)
        {
            Console.WriteLine("There was an issue pulling history.  Make sure there is history to pull. Press any Key to continue,");
            Console.ReadKey();
            return smsHistory;
        }
        else
        {
            var table = new Spectre.Console.Table();

            table.AddColumns("ID", "Contact", "To Number","Message", "Sent");


            foreach (SmsHistory text in smsHistory)
            {
                string truncatedText = $"{text.Body}".Substring(0, Math.Min(15, $"{text.Body}".Length));

                table.AddRow($@"{text.SMSHistoryId}",
                                    $@"{text.ContactName}",
                                    $@"{text.ToNumber}",
                                    $@"{truncatedText}",
                                    $@"{text.SentTS}");
            }
            AnsiConsole.Write(table);
        }

        return smsHistory;
    }
}

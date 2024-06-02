using Spectre.Console;
using Phonebook.Models;


namespace Phonebook;

internal class Display
{
    internal static string GetSelection(string title, List<string> list)
    {
        var selection = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(title).AddChoices(list).HighlightStyle(new Style(foreground: Color.White)));
        return selection;
    }

    internal static void ShowContacts(string[] columns, List<Contact> contacts)
    {
        var table = new Table();
        foreach (var column in columns)
        {
            table.AddColumn(column);
        }
        foreach(var contact in contacts)
        {
            if(contact.ContactEmailid!=null)
                table.AddRow(contact.Id.ToString(), contact.ContactName, contact.ContactPhoneno, contact.ContactEmailid);
            else table.AddRow(contact.Id.ToString(), contact.ContactName, contact.ContactPhoneno, "null");
        }
        AnsiConsole.Write(table);
    }
}

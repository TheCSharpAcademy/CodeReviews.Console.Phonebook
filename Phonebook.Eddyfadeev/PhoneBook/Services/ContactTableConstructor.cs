using PhoneBook.Extensions;
using PhoneBook.Interfaces.Services;
using PhoneBook.Model;
using Spectre.Console;

namespace PhoneBook.Services;

/// <summary>
/// Concrete implementation of <see cref="IContactTableConstructor"/>
/// </summary>
internal class ContactTableConstructor : IContactTableConstructor
{
    /// <inheritdoc cref="IContactTableConstructor.CreateContactTable"/>
    public Table CreateContactTable(Contact contact)
    {
        var table = CreateTable();
        string title = DefineHeader(contact);
        
        InitializeTable(table, title);
        PopulateTable(table, contact);
        
        return table;
    }
    
    private static Table CreateTable() => new ();

    private static void InitializeTable(Table table, string title)
    {
        table.Border(TableBorder.Rounded);
        table.Title(title);
        table.AddColumns("Info type", "Info");
        table.HideHeaders();
        table.HideFooters();
    }

    private static string DefineHeader(Contact contact)
    {
        const string header = "Contact card";
        string contactName = contact.ExtractName();
        
        var fullHeader = $"{header}\n{contactName}";

        return fullHeader;
    }

    private static void PopulateTable(Table table, Contact contact)
    {
        var contactData = contact.ToDictionary();

        foreach (KeyValuePair<string, string> dataPair in contactData)
        {
            table.AddRow(dataPair.Key, dataPair.Value);
        }
    }
}
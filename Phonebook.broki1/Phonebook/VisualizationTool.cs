using ConsoleTableExt;
using Phonebook.Model;

namespace Phonebook;

internal class VisualizationTool
{

    internal static void PrintContacts(List<Contact> contacts)
    {
        Console.WriteLine();
        ConsoleTableBuilder.From(contacts).WithTitle("CONTACTS").WithFormat(ConsoleTableBuilderFormat.Alternative).ExportAndWriteLine();
    }

}

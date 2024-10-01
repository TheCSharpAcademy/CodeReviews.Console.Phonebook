using Spectre.Console;
using System.Runtime.InteropServices.JavaScript;
namespace PhonebookLibrary.Views;

internal static class DataViewer
{
    internal static void DisplayTable<T>(List<T> information, string[] headers)
    {
        Table table = new();

        foreach (string header in headers)
            table.AddColumns(header);

        foreach(T item in information)
        {
            List<string> row = new();

            foreach(string header in headers)
            {
                var property = typeof(T).GetProperty(header);
                row.Add(property.GetValue(item).ToString() ?? "N/A");
            }
            table.AddRow(row.ToArray());
        }

        AnsiConsole.Write(table);
        System.Console.WriteLine();
    }

    internal static void DisplayHeader(string title, string alignment="center")
    {
        var heading = new Rule($"[red]{title}[/]");
        heading.Justification = alignment?.ToLower() switch
        {
            "left" => Justify.Left,
            "right" => Justify.Right,
            _ => Justify.Center
        };
        AnsiConsole.Write(heading);
        System.Console.WriteLine();
    }

    internal static void Figlet(string text, string alignment="left", string color="red")
    {
        Justify justification = alignment switch
        {
            "left" => Justify.Left,
            "right" => Justify.Right,
            _ => Justify.Center
        };

        Color colour = color switch
        {
            "blue" => Color.Blue,
            "green" => Color.Green,
            "yellow" => Color.Yellow,
            _ => Color.Red
        };

        AnsiConsole.Write(
            new FigletText(text)
                .Justify(justification)
                .Color(colour)
        );
    }
}

using Phone_Book.Lawang.Models;
using Spectre.Console;

namespace Phone_Book.Lawang.View;

public static class Visual
{
    public static void ShowTitle(string title)
    {
        var panel = new Panel(new FigletText($"{title}").Color(Color.Red))
            .BorderColor(Color.Aquamarine3)
            .PadTop(1)
            .PadBottom(1)
            .Header(new PanelHeader("[blue3 bold]APPLICATION[/]"))
            .Border(BoxBorder.Double)
            .Expand();

        AnsiConsole.Write(panel);
    }

    public static void ShowOperationTitle(string title)
    {
        var panel = new Panel(title)
            .BorderColor(Color.Salmon1)
            .PadTop(4)
            .PadBottom(4)
            .Header(new PanelHeader("[olive] Operation [/]"))
            .Expand();

        AnsiConsole.Write(panel);
    }
    public static void ShowResult(string color, int rowsAffected)
    {
        Panel panel = new Panel(new Markup($"[{color} bold]{rowsAffected} rows Affected[/]\n[grey](Press 'Enter' to Continue.)[/]"))
                        .Padding(1, 1, 1, 1)
                        .Header("Result")
                        .Border(BoxBorder.Rounded);

        AnsiConsole.Write(panel);
        Console.ReadLine();
    }
    
    public static void ShowTable(IEnumerable<Contact> contacts)
    {
        Console.Clear();

        if(contacts.Count() == 0)
        {
            Panel nullPanel = new Panel(new Markup("[red bold]CONTACTS IS EMPTY!!![/]"))
                .Border(BoxBorder.Heavy)
                .BorderColor(Color.IndianRed1_1)
                .Padding(1, 1, 1, 1)
                .Header("Result");

            
            AnsiConsole.Write(nullPanel);
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Expand()
            .BorderColor(Color.Aqua)
            .ShowRowSeparators();



        table.AddColumns(new TableColumn[]
        {
            new TableColumn("[darkgreen bold]Id[/]").Centered(),
            new TableColumn("[darkcyan bold]Name[/]").Centered(),
            new TableColumn("[darkcyan bold]Email[/]").Centered(),
            new TableColumn("[darkcyan bold]Phone Number[/]").Centered()
        });

        foreach(var contact in contacts)
        {
            table.AddRow(
                new Markup($"[cyan1]{contact.Id}[/]").Centered(),
                new Markup($"[turquoise2]{contact.Name}[/]").Centered(),
                new Markup($"[turquoise2]{contact.Email}[/]").Centered(),
                new Markup($"[turquoise2]{contact.PhoneNumber}[/]").Centered()
            );
        }

        AnsiConsole.Write(table);
    }
}

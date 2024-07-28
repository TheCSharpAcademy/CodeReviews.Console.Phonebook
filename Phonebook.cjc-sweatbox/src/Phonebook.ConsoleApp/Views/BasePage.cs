using Spectre.Console;

namespace Phonebook.ConsoleApp.Views;

/// <summary>
/// The base class for any page view.
/// </summary>
internal abstract class BasePage
{
    #region Constants

    protected static readonly string ApplicationTitle = "Phonebook";

    protected static readonly string PromptTitle = "Select an [blue]option[/]...";

    private static readonly Rule DividerLine = new Rule().RuleStyle("blueviolet").LeftJustified();

    #endregion
    #region Methods - Protected

    protected static void WriteFooter()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Markup($"Press any [blue]key[/] to continue...");
        Console.ReadKey();
    }

    protected static void WriteHeader(string title)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(DividerLine);
        AnsiConsole.MarkupLine($"[bold blueviolet]{ApplicationTitle}[/]: [slateblue3]{title}[/]");
        AnsiConsole.Write(DividerLine);
        AnsiConsole.WriteLine();
    }

    #endregion
}

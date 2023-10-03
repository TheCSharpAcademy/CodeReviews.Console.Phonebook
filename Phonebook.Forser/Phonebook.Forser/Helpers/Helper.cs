public class Helper
{
    internal static Style HighLightStyle => new(
        Color.LightGreen,
        Color.Black,
        Decoration.None
    );

    internal static void RenderTitle(string title)
    {
        var rule = new Rule($"[green]{title}[/]");
        AnsiConsole.Write(rule);
    }
}
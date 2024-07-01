namespace Program.Utils;

public class Text
{
    public static string Error(string text)
    {
        return $"[red]{text}[/]";
    }

    public static string Success(string text)
    {
        return $"[green]{text}[/]";
    }

    public static string Markup(string text, string tag)
    {
        return $"[{tag}]{text}[/]";
    }
}
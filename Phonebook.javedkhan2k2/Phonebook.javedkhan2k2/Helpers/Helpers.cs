namespace Phonebook;

internal static class Helpers
{
    internal static string[] ConvertToArray<T>(IEnumerable<T> items, Func<T, string> selector)
    {
        if (items == null)
        {
            return Array.Empty<string>();
        }
        return items.Select(selector).ToArray();
    }    

}
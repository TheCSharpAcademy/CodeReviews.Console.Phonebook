internal class Database
{
    internal static void Initialize()
    {
        using var context = new ContactContext();
        context.Database.EnsureCreated();
    }
}

namespace PhoneBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PhoneBookContext.InitializeDatabase();
            PhoneBookMenu.GetUserInput();
        }
    }
}

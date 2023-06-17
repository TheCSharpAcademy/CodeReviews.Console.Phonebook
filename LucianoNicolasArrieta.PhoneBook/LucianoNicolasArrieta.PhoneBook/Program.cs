using LucianoNicolasArrieta.PhoneBook;
using LucianoNicolasArrieta.PhoneBook.Persistence;
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        Menu menu = new Menu();
        menu.CategoryMenu();
    }
}
// See https://aka.ms/new-console-template for more information
using PhoneBook.Data.Models;
using PhoneBook.Models.Enums;
using Spectre.Console;

Console.WriteLine("Hello, World!");

var option = AnsiConsole.Prompt(
    new SelectionPrompt<MenuOptions>()
    .Title("What would you like to do?")
    .AddChoiceGroup(
        MenuOptions.AddProduct,
        MenuOptions.DeleteProduct,
        MenuOptions.UpdateProduct,
        MenuOptions.ViewProduct,
        MenuOptions.ViewAllProducts,
        MenuOptions.Quit));

using (var context = new PhonebookDbContext())
{

}
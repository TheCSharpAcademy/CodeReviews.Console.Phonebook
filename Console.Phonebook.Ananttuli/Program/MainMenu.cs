using Spectre.Console;

namespace Program;

public class MainMenu
{
    public const string Exit = "Exit";
    public const string ViewContacts = "View Contacts";
    public const string CreateContact = "Add New Contact";
    public const string EditContact = "Edit Contact";
    public const string DeleteContact = "Delete Contact";
    public const string ViewCategories = "View Categories";
    public const string CreateCategory = "Add New Category";
    public const string EditCategory = "Edit Category";
    public const string DeleteCategory = "Delete category";

    public static string Prompt()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .EnableSearch()
            .Title("\n\t[green]Menu[/]")
            .AddChoices(
                Exit,
                ViewContacts,
                CreateContact,
                EditContact,
                DeleteContact,
                ViewCategories,
                CreateCategory,
                EditCategory,
                DeleteCategory
            )
        );
    }
}
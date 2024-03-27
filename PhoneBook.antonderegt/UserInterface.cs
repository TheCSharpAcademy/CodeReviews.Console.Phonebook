using Spectre.Console;

namespace PhoneBook;

public class UserInterface
{
    public static void MainMenu()
    {
        AnsiConsole.Clear();
        bool keepRunning = true;

        while (keepRunning)
        {

            MenuOption option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                    .Title("What do you want to [blue]do[/]?")
                    .AddChoices(
                        MenuOption.AddContact,
                        MenuOption.ViewContacts,
                        MenuOption.UpdateContact,
                        MenuOption.RemoveContact,
                        MenuOption.Quit
            ));

            switch (option)
            {
                case MenuOption.AddContact:
                    AddContact();
                    break;
                case MenuOption.ViewContacts:
                    ViewContacts();
                    break;
                case MenuOption.UpdateContact:
                    UpdateContact();
                    break;
                case MenuOption.RemoveContact:
                    RemoveContact();
                    break;
                case MenuOption.Quit:
                    Environment.Exit(0);
                    return;
                default:
                    break;
            }
        }
    }

    public static void AddContact()
    {
        string name = GetName();
        string email = GetEmail();
        string phoneNumber = GetPhoneNumber();

        Contact contact = new() { Name = name, Email = email, PhoneNumber = phoneNumber };

        DataAccess.AddContact(contact);

        AnsiConsole.Markup("[green]Contact added. [/] Press enter to return to menu...");
        Console.ReadLine();
        AnsiConsole.Clear();
    }

    public static string GetName()
    {
        return AnsiConsole.Ask<string>("What is the [blue]name[/]?").Trim();
    }

    public static string GetEmail()
    {
        AnsiConsole.MarkupLine("Format: <name>@<domain>");
        string email = AnsiConsole.Ask<string>("What is the [blue]email[/]?");

        while (!Validate.IsValidEmail(email))
        {
            AnsiConsole.MarkupLine("[red]Invalid email.[/] Try again...");
            email = AnsiConsole.Ask<string>("What is the [blue]email[/]?");
        }

        return email.Trim();
    }

    public static string GetPhoneNumber()
    {
        AnsiConsole.MarkupLine("Format: + followed by 9 digits.");
        string phoneNumber = AnsiConsole.Ask<string>("What is the [blue]phone number[/]?");

        while (!Validate.IsValidPhoneNumber(phoneNumber))
        {
            AnsiConsole.MarkupLine("[red]Invalid phone number.[/] Try again...");
            phoneNumber = AnsiConsole.Ask<string>("What is the [blue]phone number[/]?");
        }

        return phoneNumber.Trim();
    }

    public static void ViewContacts()
    {
        IEnumerable<Contact> contacts = DataAccess.GetContacts();

        Table table = new()
        {
            Title = new TableTitle("Your contacts")
        };
        table.AddColumn("Name");
        table.AddColumn("Phone number");
        table.AddColumn("Email");

        foreach (Contact contact in contacts)
        {
            table.AddRow(contact.Name, contact.PhoneNumber, contact.Email);
        }

        table.RoundedBorder();
        AnsiConsole.Write(table);

        AnsiConsole.Markup("Press enter to return to menu...");
        Console.ReadLine();
        AnsiConsole.Clear();
    }

    public static void UpdateContact()
    {
        IEnumerable<Contact> contacts = DataAccess.GetContacts();
        string[] contactNames = contacts.Select(contact => contact.Name).ToArray();

        string contactName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which contact do you want to [blue]update[/]?")
                    .AddChoices(contactNames));

        Contact contact = contacts.Where(contact => contact.Name == contactName).First();

        UpdateMenuOption option = AnsiConsole.Prompt(
            new SelectionPrompt<UpdateMenuOption>()
                .Title("What do you want to [blue]update[/]?")
                .AddChoices(
                    UpdateMenuOption.Name,
                    UpdateMenuOption.Email,
                    UpdateMenuOption.PhoneNumber,
                    UpdateMenuOption.Quit
        ));

        switch (option)
        {
            case UpdateMenuOption.Name:
                contact.Name = GetName();
                break;
            case UpdateMenuOption.Email:
                contact.Email = GetEmail();
                break;
            case UpdateMenuOption.PhoneNumber:
                contact.PhoneNumber = GetPhoneNumber();
                break;
            case UpdateMenuOption.Quit:
                return;
        }

        DataAccess.UpdateContact(contact);

        AnsiConsole.Markup("[green]Contact udpated.[/] Press enter to return to menu...");
        Console.ReadLine();
        AnsiConsole.Clear();
    }

    public static void RemoveContact()
    {
        IEnumerable<Contact> contacts = DataAccess.GetContacts();
        string[] contactNames = contacts.Select(contact => contact.Name).ToArray();

        string contactName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which contact do you want to [blue]remove[/]?")
                    .AddChoices(contactNames));

        if (!AnsiConsole.Confirm($"Are you sure you want to delete {contactName}?"))
        {
            AnsiConsole.Clear();
            return;
        }

        Contact contact = contacts.Where(contact => contact.Name == contactName).First();
        DataAccess.RemoveContact(contact);

        AnsiConsole.Clear();
    }
}
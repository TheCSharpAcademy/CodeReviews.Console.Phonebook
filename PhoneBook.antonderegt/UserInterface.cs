using Spectre.Console;

namespace PhoneBook;

public class UserInterface
{
    public static async Task MainMenu()
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
                    await AddContactAsync();
                    break;
                case MenuOption.ViewContacts:
                    await ViewContactsAsync();
                    break;
                case MenuOption.UpdateContact:
                    await UpdateContactAsync();
                    break;
                case MenuOption.RemoveContact:
                    await RemoveContactAsync();
                    break;
                case MenuOption.Quit:
                    Environment.Exit(0);
                    return;
                default:
                    break;
            }
        }
    }

    public static async Task AddContactAsync()
    {
        string name = GetName();
        string email = GetEmail();
        string phoneNumber = GetPhoneNumber();

        Contact contact = new() { Name = name, Email = email, PhoneNumber = phoneNumber };

        await DataAccess.AddContactAsync(contact);

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
        AnsiConsole.MarkupLine("Format: + <country code> <number>.");
        string phoneNumber = AnsiConsole.Ask<string>("What is the [blue]phone number[/]?");

        while (!Validate.IsValidPhoneNumber(phoneNumber.Trim()))
        {
            AnsiConsole.MarkupLine("[red]Invalid phone number.[/] Try again...");
            phoneNumber = AnsiConsole.Ask<string>("What is the [blue]phone number[/]?");
        }

        return phoneNumber.Trim();
    }

    public static async Task ViewContactsAsync()
    {
        IEnumerable<Contact> contacts = await DataAccess.GetContactsAsync();

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

    public static async Task UpdateContactAsync(Contact? contact = null)
    {
        if (contact == null)
        {
            contact = await GetContactAsync();
            if (contact == null)
            {
                return;
            }
        }

        UpdateMenuOption option = AnsiConsole.Prompt(
            new SelectionPrompt<UpdateMenuOption>()
                .Title("What do you want to [blue]update[/]?")
                .AddChoices(
                    UpdateMenuOption.Name,
                    UpdateMenuOption.Email,
                    UpdateMenuOption.PhoneNumber,
                    UpdateMenuOption.DoneUpdating
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
            case UpdateMenuOption.DoneUpdating:
                return;
        }

        await DataAccess.UpdateContactAsync(contact);


        if (AnsiConsole.Confirm("[green]Contact udpated.[/] Do you want to update another property?"))
        {
            AnsiConsole.Clear();
            await UpdateContactAsync(contact);
            return;
        }

        AnsiConsole.Clear();
    }

    public static async Task<Contact?> GetContactAsync()
    {
        IEnumerable<Contact> contacts = await DataAccess.GetContactsAsync();

        if (!contacts.Any())
        {
            AnsiConsole.MarkupLine("[red]You don't have contacts yet.[/] Press enter to return to menu...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return null;
        }

        string[] contactNames = contacts.Select(contact => contact.Name).ToArray();

        string contactName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which contact do you want to [blue]update[/]?")
                    .AddChoices(contactNames));

        return contacts.Where(contact => contact.Name == contactName).First();
    }

    public static async Task RemoveContactAsync()
    {
        IEnumerable<Contact> contacts = await DataAccess.GetContactsAsync();

        if (!contacts.Any())
        {
            AnsiConsole.MarkupLine("[red]You don't have contacts yet.[/] Press enter to return to menu...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return;
        }

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
        await DataAccess.RemoveContactAsync(contact);

        AnsiConsole.Clear();
    }
}
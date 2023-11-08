namespace PhoneBook.Console.Views;

using PhoneBook.Console.DataLayer;
using PhoneBook.Console.Model;
using Spectre.Console;

public class DeleteContact
{
    private readonly PhoneContext _dbContext;

    public DeleteContact(PhoneContext _context)
    {
        _dbContext = _context ?? throw new ArgumentNullException(nameof(_context));
    }

    public void Delete()
    {
        while (true)
        {

            // FETCH ROWS FROM THE DATABASE
            List<Contact> contacts = _dbContext.GetContacts();

            // ADD TEMP CONTACT OBJECT JUST FOR USE ON THIS CLASS
            contacts.Add(new Contact() { Id = -1, Name = "NONE", Email = "NONE", PhoneNumber = "NONE" });

            AnsiConsole.Clear();
            var prompt = new SelectionPrompt<Contact>
            {
                PageSize = 10,
                Title = "Please select the contact that you want deleted or NONE to exit"
            };

            foreach (var contact in contacts)
            {
                prompt.AddChoice(contact);
            }

            var choice = AnsiConsole.Prompt(prompt);
            if (choice.Id == -1)
                break;

            if (!Helper.Confirm($"Do you wish to delete {choice} (Y/N) |>"))
            {
                AnsiConsole.MarkupLine("Okay .. Returning for you to select a contact or NONE to exit");
                Thread.Sleep(500);
                contacts.Clear();
                continue;
            }
            else
            {
                if (_dbContext.Remove(choice))
                {
                    AnsiConsole.MarkupLine($"Contact => ${choice} was deleted.");
                    contacts.Clear();
                }
            }

            if (!Helper.Confirm("Do you wish to delete another contact (Y/N) |> "))
            {
                AnsiConsole.MarkupLine("Returning you to the main menu");
                Thread.Sleep(500);
                break;
            }
        }
    }
}
namespace PhoneBook.Console.Views;

using PhoneBook.Console.DataLayer;
using PhoneBook.Console.Model;
using Spectre.Console;

public class EditContacts
{
    private readonly PhoneContext _dbContext;
    private readonly string FullNameQuestion = "Enter Full Name |> ";
    private readonly string EmailQuestion = "Enter Your Email in the form of info@example.com |> ";
    private readonly string PhoneQuestion = "Enter your Phone Number in the form of (555)123-1234 OR 555-123-1234 |> ";

    public EditContacts(PhoneContext _context)
    {
        _dbContext = _context;
    }

    public void Edit()
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
                Title = "Please select the contact that you want Edit or NONE to exit"
            };

            foreach (var contact in contacts)
            {
                prompt.AddChoice(contact);
            }

            var contactChoice = AnsiConsole.Prompt(prompt);
            Contact newContact = contactChoice;
            if (contactChoice.Id == -1)
                break;

            AnsiConsole.MarkupInterpolated($"Editing => {contactChoice}");
            AnsiConsole.WriteLine();

            var choice = AnsiConsole.Prompt(
                new TextPrompt<string>("Change One of the following..")
                .InvalidChoiceMessage("[red]Invalid Choice[/]")
                .DefaultValue("Name")
                .AddChoice("Name").AddChoice("Email").AddChoice("Phone").AddChoice("NONE"));

            AnsiConsole.WriteLine();
            choice = choice.ToUpperInvariant();

            switch (choice)
            {
                case "NAME":
                    newContact.Name = EditForm(contactChoice.Name, choice);
                    break;
                case "EMAIL":
                    newContact.Email = EditForm(contactChoice.Email, choice);
                    break;
                case "PHONE":
                    newContact.PhoneNumber = EditForm(contactChoice.PhoneNumber, choice);
                    break;
                case "NONE":
                    break;
            }


            if (_dbContext.Edit(contactChoice, newContact))
            {
                AnsiConsole.WriteLine("Contact has been updated.");
                if (Helper.Confirm("Do you wish to edit another contact (Y/N)? |>"))
                {
                    continue;
                }
                break;
            }
        }
    }

    private string EditForm(string value, string choice)
    {
        while (true)
        {
            string editValue = "";
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLineInterpolated($"Current {choice} => [white]{value}[/]");
            switch (choice)
            {
                case "NAME":
                    editValue = Helper.AskFullName(FullNameQuestion);
                    break;
                case "EMAIL":
                    editValue = Helper.AskEmail(EmailQuestion);
                    break;
                case "PHONE":
                    editValue = Helper.AskPhone(PhoneQuestion);
                    break;
            }

            if (!Helper.Confirm($"Is {editValue} Correct (Y/N) |>"))
            {
                AnsiConsole.WriteLine("Oops. Lets fix it.");
                Thread.Sleep(1000);
                continue;
            }
            return editValue;
        }
    }
}
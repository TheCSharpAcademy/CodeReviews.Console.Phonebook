namespace PhoneBook.Console.Views;

using PhoneBook.Console.DataLayer;
using PhoneBook.Console.Model;
using Spectre.Console;

public class AddContacts
{
    private readonly string FullNameQuestion = "Enter Full Name |> ";
    private readonly string EmailQuestion = "Enter Your Email in the form of info@example.com |> ";
    private readonly string PhoneQuestion = "Enter your Phone Number in the form of (555)123-1234 OR 555-123-1234 |> ";
    private readonly PhoneContext _dbContext;
    private Contact? Contact { get; set; }

    public AddContacts(PhoneContext? dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void CreateContact()
    {
        string fullName;
        // BUILD FORM TO HELP THE USER TO ADD A NEW CONTACT TO THE DB
        AnsiConsole.Clear();

        while (true)
        {
            AnsiConsole.MarkupLine("[bold white]Contact Information Entry Form[/]");
            AnsiConsole.WriteLine();
            fullName = Helper.AskFullName(FullNameQuestion);
            AnsiConsole.WriteLine();
            if (!Helper.Confirm($"Your Name is {fullName} (Y/N)"))
            {
                AnsiConsole.WriteLine("oope..lets fix it");
                Thread.Sleep(1000);
                continue;
            }
            // EMAIL AND PHONE NUMBER
            // Email Validation
            var email = Helper.AskEmail(EmailQuestion);
            // Phone Validation
            var phone = Helper.AskPhone(PhoneQuestion);
            // validation has been successful up to this point.
            // CREATE A TABLE FOR RESULTS
            var table = new Table();

            table.AddColumns("Question", "Answer");
            table.AddRow("Full Name", fullName);
            table.AddRow("Email", email);
            table.AddRow("Phone", phone);

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine();
            if (!Helper.Confirm("Is Information correct (Y/N)?"))
            {
                AnsiConsole.MarkupLine("Lets fix this then since it is incorrect.");
                Thread.Sleep(1000);
                continue;
            }

            // SAVE THE INFO THEN SINCE IT IS CORRECT
            Contact = new()
            {
                Name = fullName,
                Email = email,
                PhoneNumber = phone
            };


            if (_dbContext.Add(Contact))
            {
                Thread.Sleep(300);
                AnsiConsole.WriteLine("Contact Saved Successfully.");
                if (!AnsiConsole.Confirm("Do you wish to add another contact (Y/N)? |>"))
                {
                    Thread.Sleep(200);
                    break;
                }
            }
        }
    }
}
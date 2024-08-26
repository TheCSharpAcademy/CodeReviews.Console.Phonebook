using AutoMapper;
using DataAccess;
using Model;
using Spectre.Console;

namespace PhoneBook;
internal class Logic
{
    public static IMapper mapper = AutoMapperConfig.InitializeAutoMapper();
    public static void Do(int userInput, out bool openApp)
    {
        openApp = true;
        switch (userInput)
        {
            case 0:
                Console.Clear();
                Thread.Sleep(1000);
                AnsiConsole.Write(new Markup("[red]Exiting...[/]\n"));
                Environment.Exit(0);
                break;
            case 1:
                CreatedContact();
                break;
            case 2:
                GetAllContacts();
                break;
            case 3:
                UpdateContact();
                break;
            case 4:
                DeleteContact();
                break;
        }
    }

    private static void UpdateContact()
    {
        int userID = 0;
        string choice, choice2 = string.Empty;

        Console.Clear();
        using (MyDbContext db = new MyDbContext())
        {
            Dictionary<string, int> users = new Dictionary<string, int>();
            IEnumerable<Contact> contacts = db.contacts.OrderBy(x => x.Name).ToList();

            if (contacts.Count() == 0)
            {
                Thread.Sleep(500);
                Console.WriteLine("Empty contact list");
            }
            else
            {
                foreach (Contact element in contacts)
                {
                    userID++;
                    if (element.Name != null) users.Add(element.Name, userID);
                }
                choice = SelectionPrompt.Selection(users);

                Dictionary<string, int> field = new Dictionary<string, int>()
            {
                { "Name", 0 },
                { "Email", 1 },
                { "Number", 2 }
            };
                choice2 = SelectionPrompt.Selection(field);

                switch (field[choice2])
                {
                    case 0:
                        Validation.IsValidName(out string? userName);
                        Contact? contactName = db.contacts.Single(x => x.Name == choice);
                        contactName.Name = userName;
                        db.SaveChanges();
                        Console.Clear();
                        Thread.Sleep(500);
                        Console.WriteLine("Updated");
                        break;
                    case 1:
                        Validation.IsValidEmail(out string? userEmail);
                        Contact? contactEmail = db.contacts.Single(x => x.Name == choice);
                        contactEmail.Email = userEmail;
                        Console.Clear();
                        Thread.Sleep(500);
                        Console.WriteLine("Updated");
                        db.SaveChanges();
                        break;
                    case 2:
                        Validation.IsValidNumber(out int userNumber);
                        Contact? contactNumber = db.contacts.Single(x => x.Name == choice);
                        contactNumber.PhoneNumber = $"{userNumber}";
                        Console.Clear();
                        db.SaveChanges();
                        Thread.Sleep(500);
                        Console.WriteLine("Updated");
                        break;
                }
            }   
        }
    }

    private static void CreatedContact()
    {
        Console.Clear();
        using (MyDbContext db = new MyDbContext())
        {
            Validation.IsValidName(out string? userName);
            Validation.IsValidEmail(out string? userEmail);
            Validation.IsValidNumber(out int userNumber);

            ContactDTO contactDTO = new ContactDTO() { Name = userName, Email = userEmail, PhoneNumber = $"{userNumber}" };
            Contact contact = mapper.Map<Contact>(contactDTO);
            db.contacts.Add(contact);
            db.SaveChanges();

            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("Contact Created");
        }    
    }

    private static void DeleteContact()
    {
        int userID = 0;
        string choice = string.Empty;

        Console.Clear();
        using (MyDbContext db = new MyDbContext())
        {
            Dictionary<string, int> users = new Dictionary<string, int>();
            IEnumerable<Contact> contacts = db.contacts.OrderBy(x => x.Name).ToList();

            if (contacts.Count() == 0)
            {
                Thread.Sleep(500);
                Console.WriteLine("Empty contact list");
            }
            else
            {
                foreach (Contact element in contacts)
                {
                    userID++;
                    if (element.Name != null) users.Add(element.Name, userID);
                }
                choice = SelectionPrompt.Selection(users);

                Contact? contact = db.contacts.SingleOrDefault(x => x.Name == choice);
                db.contacts.Remove(contact);
                db.SaveChanges();

                Thread.Sleep(500);
                Console.WriteLine("Contact Deleted");
            } 
        }
    }

    private static void GetAllContacts()
    {
        Console.Clear();
        using (MyDbContext db = new MyDbContext())
        {
            IEnumerable<Contact> contact = db.contacts.OrderBy(x => x.Name).ToList();
            List<ContactDTO> DTOList = mapper.Map<List<ContactDTO>>(contact.ToList());

            if (DTOList.Count == 0)
            {
                Thread.Sleep(500);
                Console.WriteLine("Empty contact list");
            }
            else
            {
                Table table = new Table();
                table.Border = TableBorder.Ascii;

                table.AddColumn("Name");
                table.AddColumn("Email");
                table.AddColumn("Number");

                foreach (ContactDTO element in DTOList)
                {
                    table.AddRow($"{element.Name}", $"{element.Email}", $"{element.PhoneNumber}");
                }
                AnsiConsole.Write(table);

                //Email + SMS
                Dictionary<string, int> choices = new Dictionary<string, int>()
                {
                    { "Send Email", 0 },
                    { "Send SMS", 1 },
                    { "Go back", 2 }
                };

                string choice = SelectionPrompt.Selection(choices);
                int userInput = choices[choice];

                if (userInput == 2)
                {
                    Console.Clear();
                    return;
                }
                else if (userInput == 0)
                {
                    int userID = 0;
                    Dictionary<string, int> emails = new Dictionary<string, int>();

                    foreach (ContactDTO element in DTOList)
                    {
                        userID++;
                        if (element.Email != null) emails.Add(element.Email, userID);
                    }
                    choice = SelectionPrompt.Selection(emails);

                    Console.WriteLine($"To: {choice}");
                    Console.Write("Subject: ");
                    string? subject = Console.ReadLine();
                    Console.Write("Body: ");
                    string? body = Console.ReadLine();

                    if (subject != null && body != null) EmailService.SendEmail(choice, subject, body);
                }
                else
                {
                    int userID = 0;
                    Dictionary<string, int> numbers = new Dictionary<string, int>();

                    foreach (ContactDTO element in DTOList)
                    {
                        userID++;
                        if (element.PhoneNumber != null) numbers.Add(element.PhoneNumber, userID);
                    }
                    choice = SelectionPrompt.Selection(numbers);

                    Console.Write("Message: ");
                    string? message = Console.ReadLine();
                    MessagingService.SendSMS($"{choice}", $"{message}");
                }
            }
        }
    }
}

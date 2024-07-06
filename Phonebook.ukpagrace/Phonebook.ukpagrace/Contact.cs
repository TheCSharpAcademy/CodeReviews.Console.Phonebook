using Microsoft.EntityFrameworkCore;
using Phonebook.ukpagrace.Mail;
using Phonebook.ukpagrace.Model;
using Phonebook.ukpagrace.Utility;
using Spectre.Console;

namespace Phonebook.ukpagrace.Controller
{
    internal class ContactController
    {

        readonly UserInput userInput = new ();
        public void Create()
        {
            using var db = new ApplicationContext();
            var name = AnsiConsole.Ask<string>("Enter [green] name[/]?");
            var email = userInput.Email();
            var number = userInput.PhoneNumber();
            var category = userInput.Category();
            var selectedCategory = db.Category.FirstOrDefault(c => c.Name == category);
            if (selectedCategory == null)
            {
                Console.WriteLine("No Category Found");
                return;
            }
            db.Add(new Contact()
            {
                Name = name,
                Email = email,
                PhoneNumber = number,
                CategoryID = selectedCategory.Id
            });


            db.SaveChanges();

            Email mail = new ();
            AnsiConsole.Status()
                .Start("Sending email...", ctx =>
                {
                    Task.Run(() =>
                    {
                        mail.SendEmail(email);
                    }).GetAwaiter().GetResult();

                    ctx.Status("Email sent successfully!");
                });
               AnsiConsole.MarkupLine("[green]Contact added and email sent successfully![/]");
        }

        public void List()
        {
            using var db = new ApplicationContext();

            var grid = new Grid();

            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();

            grid.AddRow(new Text[]{
                new Text("Id", new Style(Color.Red, Color.Black)).LeftJustified(),
                new Text("Name", new Style(Color.Green, Color.Black)).Centered(),
                new Text("Email", new Style(Color.Blue, Color.Black)).Centered(),
                new Text("Phone Number", new Style(Color.Red, Color.Black)).Centered(),
                new Text("Category Name", new Style(Color.Blue, Color.Black)).Centered()
            });

            var contacts = db.Contacts.Include(e => e.Category).ToList();

            foreach (var contact in contacts) {
                grid.AddRow(new Text[]{
                new Text($"{contact.Id}", new Style(Color.Red, Color.Black)).LeftJustified(),
                new Text($"{contact.Name}", new Style(Color.Green, Color.Black)).Centered(),
                new Text($"{contact.Email}", new Style(Color.Red3_1, Color.Black)).Centered(),
                new Text($"{contact.PhoneNumber}", new Style(Color.Green, Color.Black)).Centered(),
                new Text($"{contact.Category.Name}", new Style(Color.Blue, Color.Black)).Centered()
            });
            }

            AnsiConsole.Write(grid);
        }

        public void Update()
        {
            using var db = new ApplicationContext();
            List();
            var id = AnsiConsole.Ask<int>("Enter an [green] id [/]?");
            var name = AnsiConsole.Ask<string>("Enter [green] name[/]?");
            var email = userInput.Email();
            var number = userInput.PhoneNumber();

            var category = userInput.Category();
            var selectedCategory = db.Category.FirstOrDefault(c => c.Name == category);
            if (selectedCategory == null)
            {
                Console.WriteLine("No Category Found");
                return;
            }

            var selectedContact = db.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (selectedContact != null)
            {
                selectedContact.Name = name;
                selectedContact.Email = email;
                selectedContact.PhoneNumber = number;
                selectedContact.CategoryID = selectedCategory.Id;
                db.SaveChanges();
                AnsiConsole.MarkupLine("[green]Updated Contact![/]");
            }
            else
            {
                Console.WriteLine("Contact not found");
            }
        }

        public void Delete()
        {
            using var db = new ApplicationContext();
            List();
            var id = AnsiConsole.Ask<int>("Enter an [green] id [/]?");


            var selectedContact = db.Contacts.FirstOrDefault(contact => contact.Id == id);
            if (selectedContact != null)
            {
                db.Remove(selectedContact);
                db.SaveChanges();
                AnsiConsole.MarkupLine("[green]Deleted Contact![/]");
            }
            else
            {
                Console.WriteLine("Contact not found");
            }
        }
    }
}

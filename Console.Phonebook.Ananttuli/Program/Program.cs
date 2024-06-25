using Program.Categories;
using Program.Contacts;
using Program.Utils;

namespace Program;

public class Program
{
    public static void Main()
    {
        using var db = new PhonebookContext();

        StartApp(db).GetAwaiter().GetResult();
        // db.Add(
        //     new Category { Name = "Friends" }
        // );

        // db.SaveChanges();

        // var cat = db.Categories.OrderBy(c => c.CategoryId).First();

        // Console.WriteLine($"Cat {cat.CategoryId} {cat.Name}");
    }

    public static async Task StartApp(PhonebookContext db)
    {
        bool isAppRunning = true;

        while (isAppRunning)
        {
            var option = MainMenu.Prompt();

            switch (option)
            {
                case MainMenu.Exit:
                    isAppRunning = false;
                    break;
                case MainMenu.ViewContacts:
                    await ContactsController.ListContacts(db);
                    break;
                case MainMenu.CreateContact:
                    await ContactsController.CreateOrUpdateContact(db);
                    break;
                case MainMenu.EditContact:
                    await ContactsController.EditContact(db);
                    break;
                case MainMenu.DeleteContact:
                    await ContactsController.DeleteContact(db);
                    break;
                case MainMenu.ViewCategories:
                    break;
                case MainMenu.CreateCategory:
                    break;
                case MainMenu.EditCategory:
                    break;
                case MainMenu.DeleteCategory:
                    break;
                default:
                    break;
            }

            if (isAppRunning)
            {
                ConsoleUtil.PressAnyKeyToClear();
            }
        }
    }




}
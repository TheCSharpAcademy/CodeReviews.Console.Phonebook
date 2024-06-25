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
                    await ContactsController.CreateContact(db);
                    break;
                case MainMenu.EditContact:
                    await ContactsController.EditContact(db);
                    break;
                case MainMenu.DeleteContact:
                    await ContactsController.DeleteContact(db);
                    break;
                case MainMenu.ViewCategories:
                    await CategoriesController.ListCategories(db);
                    break;
                case MainMenu.CreateCategory:
                    await CategoriesController.CreateCategory(db);
                    break;
                case MainMenu.EditCategory:
                    await CategoriesController.EditCategory(db);
                    break;
                case MainMenu.DeleteCategory:
                    await CategoriesController.DeleteCategory(db);
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
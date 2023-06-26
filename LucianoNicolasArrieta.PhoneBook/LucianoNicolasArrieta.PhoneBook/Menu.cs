using LucianoNicolasArrieta.PhoneBook.Models;
using LucianoNicolasArrieta.PhoneBook.Persistence;
using Spectre.Console;

namespace LucianoNicolasArrieta.PhoneBook
{
    public class Menu
    {
        public void CategoryMenu()
        {
            bool running = true;
            CategoryController categoryController = new CategoryController();
            UserInput userInput = new UserInput();

            while (running)
            {
                Console.Clear();
                categoryController.ViewCategories();
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<CategoryMenuOptions>()
                    .AddChoices(
                        CategoryMenuOptions.SelectCategory,
                        CategoryMenuOptions.AddCategory,
                        CategoryMenuOptions.UpdateCategory,
                        CategoryMenuOptions.DeleteCategory,
                        CategoryMenuOptions.ViewAllContacts,
                        CategoryMenuOptions.ExitApp));

                switch (option)
                {
                    case CategoryMenuOptions.SelectCategory:
                        Category category_selected = categoryController.GetCategoryByID();
                        ContactMenu(category_selected);
                        break;
                    case CategoryMenuOptions.AddCategory:
                        Category category = userInput.CategoryInput();
                        categoryController.Insert(category);
                        break;
                    case CategoryMenuOptions.UpdateCategory:
                        categoryController.Update();
                        break;
                    case CategoryMenuOptions.DeleteCategory:
                        categoryController.Delete();
                        break;
                    case CategoryMenuOptions.ViewAllContacts:
                        ContactController contactController = new ContactController();
                        contactController.ViewAll();
                        break;
                    case CategoryMenuOptions.ExitApp:
                        running = false;
                        AnsiConsole.Markup("[bold yellow]See you![/]");
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ContactMenu(Category category)
        {
            bool running = true;
            ContactController contactController = new ContactController();
            UserInput userInput = new UserInput();

            while (running)
            {
                Console.Clear();
                contactController.ViewContactsByCategory(category);
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<ContactMenuOptions>()
                    .AddChoices(
                        ContactMenuOptions.AddContact,
                        ContactMenuOptions.UpdateContact,
                        ContactMenuOptions.DeleteContact,
                        ContactMenuOptions.SendMailThroughGmailToContact,
                        ContactMenuOptions.Exit));

                switch (option)
                {
                    case ContactMenuOptions.AddContact:
                        contactController.InsertInCategory(category);
                        break;
                    case ContactMenuOptions.UpdateContact:
                        contactController.Update();
                        break;
                    case ContactMenuOptions.DeleteContact:
                        contactController.Delete();
                        break;
                    case ContactMenuOptions.SendMailThroughGmailToContact:
                        EmailSender emailSender = new EmailSender();
                        emailSender.sendEmail();
                        break;
                    case ContactMenuOptions.Exit:
                        CategoryMenu();
                        break;
                    default:
                        break;
                }
            }
        }

    }

    enum CategoryMenuOptions
    {
        SelectCategory,
        AddCategory,
        UpdateCategory,
        DeleteCategory,
        ViewAllContacts,
        ExitApp
    }

    enum ContactMenuOptions
    {
        AddContact,
        UpdateContact,
        DeleteContact,
        SendMailThroughGmailToContact,
        Exit
    }
}

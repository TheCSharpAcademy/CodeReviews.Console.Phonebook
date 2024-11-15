
using PhoneBook.mefdev.Models;
using PhoneBook.mefdev.Controllers;
using Spectre.Console;

namespace PhoneBook.mefdev;

internal class UserInterface: BaseController
{
    private readonly ContactController _contactController;
    private readonly CategoryController _categoryController;
    private readonly NotificationController _notificationController;

    public UserInterface(ContactController contactController, CategoryController categoryController, NotificationController notificationController)
    {
        _contactController = contactController;
        _categoryController = categoryController;
        _notificationController = notificationController;
    }
    public void MainMenu()
    {
        while (true)
        {
            AnsiConsole.Write(new FigletText("Phone Book").Color(Color.DodgerBlue1).Centered());

            AnsiConsole.Clear();

            var mainChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MainOptionMenu>()
                    .Title("Choose an option:")
                    .AddChoices(Enum.GetValues(typeof(MainOptionMenu)).Cast<MainOptionMenu>())
            );

            switch (mainChoice)
            {
                case MainOptionMenu.Quit:
                    DisplayMessage("Exiting the app...", "red");
                    Environment.Exit(0);
                    break;

                case MainOptionMenu.ManageContacts:
                    HandleCRUDMenu(CRUDMenu.AddContact, CRUDMenu.ViewContacts);
                    break;

                case MainOptionMenu.ManageCategories:
                    HandleCRUDMenu(CRUDMenu.AddCategory, CRUDMenu.ViewCategories);
                    break;

                case MainOptionMenu.SendNotifications:
                    HandleNotificationMenu();
                    break;

                default:
                    DisplayMessage("Invalid choice. Please select a valid option.", "red");
                    break;
            }
        }
    }

    public void HandleCRUDMenu(CRUDMenu firstCrudOperation, CRUDMenu lastCrudOperation)
    {
        while (true)
        {
            AnsiConsole.Clear();
            DisplayMessage("Select an operation: ", "DodgerBlue1");

            var choices = Enum.GetValues(typeof(CRUDMenu))
                              .Cast<CRUDMenu>()
                              .Where(op => (int)op >= (int)firstCrudOperation && (int)op <= (int)lastCrudOperation)
                              .ToList();

            choices.Add(CRUDMenu.BackToMainMenu);

            var selectedChoice = AnsiConsole.Prompt(
                new SelectionPrompt<CRUDMenu>()
                    .Title("Choose a CRUD operation:")
                    .AddChoices(choices)
            );

            if (selectedChoice == CRUDMenu.BackToMainMenu)
            {
                return;
            }
            HandleOperation(selectedChoice);
        }
    }


    public void HandleOperation(CRUDMenu operation)
    {
        switch (operation)
        {
            case CRUDMenu.AddContact:
                _contactController.AddItem();
                break;
            case CRUDMenu.UpdateContact:
                _contactController.UpdateItem();
                break;
            case CRUDMenu.DeleteContact:
                _contactController.DeleteItem();
                break;
            case CRUDMenu.ViewContact:
                _contactController.ViewItem();
                break;
            case CRUDMenu.ViewContacts:
                _contactController.ViewItems();
                break;

            case CRUDMenu.AddCategory:
                _categoryController.AddItem();
                break;
            case CRUDMenu.UpdateCategory:
                _categoryController.UpdateItem();
                break;
            case CRUDMenu.DeleteCategory:
                _categoryController.DeleteItem();
                break;
            case CRUDMenu.ViewCategory:
                break;
            case CRUDMenu.ViewCategories:
                _categoryController.ViewItems();
                break;

            default:
                DisplayMessage("Invalid operation.", "red");
                break;
        }
    }

    private void HandleNotificationMenu()
    {
        while (true)
        {
            AnsiConsole.Clear();
            DisplayMessage("Select a notification operation:", "DodgerBlue1");

            var choices = new List<string> { "Send Notification", "Back to Main Menu" };

            var selectedChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an operation:")
                    .AddChoices(choices)
            );

            if (selectedChoice.Equals("Back to Main Menu"))
            {
                return;
            }
            _notificationController.SendNotification();
        }
    }
}
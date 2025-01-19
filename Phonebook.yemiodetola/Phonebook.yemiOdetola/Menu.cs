using Spectre.Console;

namespace Phonebook.yemiOdetola;
public class Menu
{

  private readonly ContactsRepository contactsRepository;
  private readonly ContactController contactController;
  private readonly CategoryController categoryController;

  public Menu()
  {
    contactsRepository = new ContactsRepository();
    contactController = new ContactController(contactsRepository);
    categoryController = new CategoryController(contactsRepository);
  }
  public async Task Show()
  {
    bool isAppRunning = true;

    while (isAppRunning)
    {
      var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
      .Title("Select an option")
      .AddChoices(
        MenuOptions.AddContact,
        MenuOptions.DeleteContact,
        MenuOptions.UpdateContact,
        MenuOptions.ViewAllContacts,
        MenuOptions.CreateCategory,
        MenuOptions.GetAllCategories,
        MenuOptions.Exit
        )
      );

      switch (option)
      {
        case MenuOptions.AddContact:
          await contactController.AddContact();
          break;
        case MenuOptions.DeleteContact:
          await contactController.DeleteContact();
          break;
        case MenuOptions.UpdateContact:
          await contactController.UpdateContact();
          break;
        case MenuOptions.ViewAllContacts:
          await contactController.GetAllContacts();
          break;
        case MenuOptions.CreateCategory:
          await categoryController.CreateCategory();
          break;
        case MenuOptions.GetAllCategories:
          await categoryController.GetAllCategories();
          break;
        case MenuOptions.Exit:
          isAppRunning = false;
          break;
      }
    }
  }
}
enum MenuOptions
{
  AddContact,
  DeleteContact,
  UpdateContact,
  ViewContact,
  ViewAllContacts,
  CreateCategory,
  GetAllCategories,
  Exit
}
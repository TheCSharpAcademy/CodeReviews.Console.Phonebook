using Spectre.Console;
using TwilightSaw.Phonebook.Controller;
using TwilightSaw.Phonebook.Helpers;
using TwilightSaw.Phonebook.Model;

namespace TwilightSaw.Phonebook.View;

public class CategoryMenu(
    ContactController contactController,
    EmailController emailController,
    CategoryController categoryController,
    MessageController messageController)
{
    public void Menu(bool isDeleted, Category chosenCategory)
    {
        Console.Clear();
        AnsiConsole.Write(new Rule($"[aqua]{chosenCategory.Name}[/]"));
        var list = chosenCategory.Name == "All contacts"
            ? contactController.Read()
            : contactController.Read(chosenCategory);
        if (chosenCategory.Name == "Add new category")
        {
            AddCategory();
            return;
        }

        var contact = UserInput.CreateContactChoosingList(list, "Return", chosenCategory.Name);
        switch (contact.Name)
        {
            case "Return":
                return;
            case "Add new contact":
                AddNewContact(contact, chosenCategory);
                return;
            case "Delete this category":
                categoryController.Delete(chosenCategory);
                return;
            case "Change the name of this category":
                ChangeCategory(chosenCategory);
                return;
        }
        new ContactMenu(contactController, emailController, messageController).Menu(contact, isDeleted);
    }

    protected void AddNewContact(Contact contact, Category chosenCategory)
    {
        Validation.Validate(
            () => contact = chosenCategory.Name == "All contacts"
                ? AddContact()
                : AddCategoryContact(chosenCategory), true);
    }

    protected void AddCategory()
    {
        var newCategory = UserInput.Create("Insert category name");
        if (newCategory == "0") return;
        categoryController.Create(new Category(newCategory));
    }

    protected Contact AddContact()
    {
        var k = UserInput.CreateContact();
        if (k.Name == "0") return null;
        Validation.Validate(() => contactController.Add(k), false, out var contact);
        return contact.Invoke();
    }

    protected Contact AddCategoryContact(Category chosenCategory)
    {
        Validation.Validate(() => contactController.AddToCategory(UserInput.CreateContact(), chosenCategory), false,
            out var contact);
        return contact.Invoke();
    }

    protected void ChangeCategory(Category chosenCategory)
    {
        var newName = UserInput.Create("Insert new category name");
        if (newName == "0") return;
        categoryController.Update(chosenCategory, newName);
    }
}
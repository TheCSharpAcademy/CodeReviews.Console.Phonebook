namespace PhoneBook;

class CategoryController
{
    private readonly PhoneBookContext phoneBookContext;
    private ContactController contactController;

    public CategoryController(PhoneBookContext phoneBookContext, ContactController contactController)
    {
        this.phoneBookContext = phoneBookContext;
        this.contactController = contactController;
    }

    public void ShowList()
    {
        ShowList(null);
    }

    public void ShowList(string? message)
    {
        var categories = phoneBookContext.Category.OrderBy(c => c.Name).ToList();
        var view = new CategoryListView(this, categories);
        view.SetMessage(message);
        view.Show();
    }

    public void ShowAdd()
    {
        // TODO
        ShowList();
    }

    public void ShowContacts(Category category)
    {
        phoneBookContext.Entry(category).Collection(c => c.Contacts).Load();
        contactController.ShowList(category);
    }

    public void ShowEdit(Category category)
    {
        // TODO
        ShowList();
    }

    public void ShowDelete(Category category)
    {
        // TODO
        ShowList();
    }

    public static void ShowExit()
    {
        var view = new ExitView();
        view.Show();
    }
}
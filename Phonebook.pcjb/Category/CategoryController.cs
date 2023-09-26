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
        var view = new CategoryAddView(this);
        view.Show();
    }

    public void Create(string? name)
    {
        if (String.IsNullOrEmpty(name))
        {
            ShowList($"ERROR - The new category must have a name.");
            return;
        }

        try
        {
            var category = new Category() { Name = name };
            phoneBookContext.Add(category);
            phoneBookContext.SaveChanges();
            ShowList($"OK - New category '{name}' added.");
        }
        catch (Exception)
        {
            ShowList($"ERROR - Failed to add new category.");
        }

    }

    public void ShowContacts(Category category)
    {
        phoneBookContext.Entry(category).Collection(c => c.Contacts).Load();
        contactController.ShowList(category);
    }

    public void ShowEdit(Category category)
    {
        var view = new CategoryEditView(this, category);
        view.Show();
    }

    public void Update(Category category, string? newName)
    {
        if (String.IsNullOrEmpty(newName))
        {
            ShowList($"ERROR - The new name of the category must not be empty.");
            return;
        }

        try
        {
            category.Name = newName;
            phoneBookContext.SaveChanges();
            ShowList($"OK - Category name changed to '{category.Name}'");
        }
        catch (Exception)
        {
            ShowList($"ERROR - Failed to update category.");
        }

    }

    public void ShowDelete(Category category)
    {
        var view = new CategoryDeleteView(this, category);
        view.Show();
    }

    public void Delete(Category category)
    {
        phoneBookContext.Entry(category).Collection(c => c.Contacts).Load();
        if (category.Contacts.Count > 0)
        {
            ShowList($"Cannot delete non-empty category '{category.Name}'. Please delete contacts first.");
            return;
        }
        
        try
        {
            phoneBookContext.Remove(category);
            phoneBookContext.SaveChanges();
            ShowList($"OK - Category '{category.Name}' deleted.");
        }
        catch (Exception)
        {
            ShowList($"ERROR - Failed to delete category '{category.Name}'.");
        }
    }

    public static void ShowExit()
    {
        var view = new ExitView();
        view.Show();
    }
}
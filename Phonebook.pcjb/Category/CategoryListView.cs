namespace PhoneBook;

class CategoryListView : BaseView
{
    private readonly CategoryController controller;
    private readonly List<Category> categories;
    private int pointer;

    public CategoryListView(CategoryController controller, List<Category> categories)
    {
        this.controller = controller;
        this.categories = categories;
    }

    public override void Body()
    {
        Console.WriteLine($"Categories:");

        if (HasCategories())
        {
            WriteCategoryList();
        }
        else
        {
            Console.WriteLine("No categories found.");
        }

        Console.WriteLine("---");
        if (HasCategories())
        {
            Console.WriteLine("Press arrow-up/-down to scroll through the list of categories.");
            Console.WriteLine("Press arrow-right to view the contacts of the category marked with '->'.");
        }
        Console.WriteLine("Press 'a' to add a new category.");
        if (HasCategories())
        {
            Console.WriteLine("Press 'e' to edit the category marked with '->'.");
            Console.WriteLine("Press 'd' to delete the category marked with '->'.");
        }
        Console.WriteLine("Press 'x' to exit.");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.DownArrow:
                PointerDown();
                break;
            case ConsoleKey.UpArrow:
                PointerUp();
                break;
            case ConsoleKey.RightArrow:
                Contacts();
                break;
            case ConsoleKey.A:
                controller.ShowAdd();
                break;
            case ConsoleKey.E:
                Edit();
                break;
            case ConsoleKey.D:
                Delete();
                break;
            case ConsoleKey.X:
                CategoryController.ShowExit();
                break;
            default:
                Show();
                break;
        }
    }

    private bool HasCategories()
    {
        return categories != null && categories.Count > 0;
    }

    private void WriteCategoryList()
    {
        int itemsAfterPointer = categories.Count - 1 - pointer;
        int maxVisibleItemsBeforeAfter = 3;
        int first = pointer - Math.Min(maxVisibleItemsBeforeAfter, pointer);
        int last = pointer + Math.Min(maxVisibleItemsBeforeAfter, itemsAfterPointer);
        if (pointer - first < maxVisibleItemsBeforeAfter)
        {
            last += Math.Min(maxVisibleItemsBeforeAfter - (pointer - first), itemsAfterPointer);
        }
        if (last - pointer < maxVisibleItemsBeforeAfter)
        {
            first -= Math.Min(maxVisibleItemsBeforeAfter - (last - pointer), pointer);
        }
        if (first < 0) first = 0;
        if (first >= categories.Count) first = categories.Count - 1;
        if (last < 0) last = 0;
        if (last >= categories.Count) last = categories.Count - 1;

        for (int i = first; i <= last; i++)
        {
            var pointerSymbol = (i == pointer) ? "->" : "  ";
            Console.WriteLine($"{pointerSymbol} {categories[i].Name}");
        }
    }

    private void PointerDown()
    {
        pointer++;
        if (HasCategories() && pointer > categories.Count - 1)
        {
            pointer = categories.Count - 1;
        }
        Show();
    }

    private void PointerUp()
    {
        pointer--;
        if (pointer < 0)
        {
            pointer = 0;
        }
        Show();
    }

    private void Contacts()
    {
        if (HasCategories())
        {
            controller.ShowContacts(categories[pointer]);
        }
        else
        {
            Show();
        }
    }

    private void Edit()
    {
        if (HasCategories())
        {
            controller.ShowEdit(categories[pointer]);
        }
        else
        {
            Show();
        }
    }

    private void Delete()
    {
        if (HasCategories())
        {
            controller.ShowDelete(categories[pointer]);
        }
        else
        {
            Show();
        }
    }
}
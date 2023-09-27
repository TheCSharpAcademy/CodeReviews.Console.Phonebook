namespace PhoneBook;

class CategoryDeleteView : BaseView
{
    private readonly CategoryController controller;
    private readonly Category category;

    public CategoryDeleteView(CategoryController controller, Category category)
    {
        this.controller = controller;
        this.category = category;
    }

    public override void Body()
    {
        Console.WriteLine("Delete Category");
        Console.WriteLine($"Name: {category.Name}");
        Console.WriteLine("Are you sure? [y/n]");
        switch (Console.ReadKey().KeyChar.ToString())
        {
            case "y":
                controller.Delete(category);
                break;
            default:
                controller.ShowList();
                break;
        }
    }
}
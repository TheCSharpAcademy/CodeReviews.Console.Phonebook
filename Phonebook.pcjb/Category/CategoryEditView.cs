namespace PhoneBook;

class CategoryEditView : BaseView
{
    private readonly CategoryController controller;
    private readonly Category category;

    public CategoryEditView(CategoryController controller, Category category)
    {
        this.controller = controller;
        this.category = category;
    }

    public override void Body()
    {
        string? newName;
        bool isValid;

        Console.WriteLine("Edit Category");

        Console.WriteLine($"Old Name: {category.Name}");
        do
        {
            Console.Write("New Name: ");
            newName = Console.ReadLine();
            isValid = !String.IsNullOrEmpty(newName);
            if (!isValid)
            {
                Console.WriteLine("Please enter a name.");
            }
        } while (!isValid);

        controller.Update(category, newName);
    }
}
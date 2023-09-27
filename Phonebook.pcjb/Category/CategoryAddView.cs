namespace PhoneBook;

class CategoryAddView : BaseView
{
    private readonly CategoryController controller;

    public CategoryAddView(CategoryController controller)
    {
        this.controller = controller;
    }

    public override void Body()
    {
        string? name;
        bool isValid;

        Console.WriteLine("Add Category");
        do
        {
            Console.Write("Name: ");
            name = Console.ReadLine();
            isValid = !String.IsNullOrEmpty(name);
            if (!isValid)
            {
                Console.WriteLine("Please enter a name.");
            }
        } while (!isValid);

        controller.Create(name);
    }
}
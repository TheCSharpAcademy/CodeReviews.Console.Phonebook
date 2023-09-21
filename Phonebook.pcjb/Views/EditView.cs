namespace PhoneBook;

class EditView : BaseView
{
    private readonly Controller controller;

    public EditView(Controller controller)
    {
        this.controller = controller;
    }

    public override void Body()
    {
        Console.WriteLine("Edit Contact");
        Console.ReadLine();
        controller.ShowMenu();
    }
}
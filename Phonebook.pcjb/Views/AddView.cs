namespace PhoneBook;

class AddView : BaseView
{
    private readonly Controller controller;

    public AddView(Controller controller)
    {
        this.controller = controller;
    }
    public override void Body()
    {
        Console.WriteLine("Add Contact");
        Console.ReadLine();
        controller.ShowMenu();
    }
}
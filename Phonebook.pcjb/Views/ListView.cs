namespace PhoneBook;

class ListView : BaseView
{
    private readonly Controller controller;

    public ListView(Controller controller)
    {
        this.controller = controller;
    }

    public override void Body()
    {
        Console.WriteLine("All Contacts");
        Console.ReadLine();
        controller.ShowMenu();
    }
}
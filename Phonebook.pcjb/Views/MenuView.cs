namespace PhoneBook;

class MenuView : BaseView
{
    private readonly Controller controller;

    public MenuView(Controller controller)
    {
        this.controller = controller;
    }
    
    public override void Body()
    {
        Console.WriteLine("Main Menu");
        Console.WriteLine("1 - List Contacts");
        Console.WriteLine("2 - Add Contact");
        Console.WriteLine("3 - Edit Contact");
        Console.WriteLine("4 - Delete Contact");
        Console.WriteLine("0 - Exit");

        switch (Console.ReadKey().KeyChar.ToString())
        {
            case "1":
                controller.ShowList();
            break;
            case "2":
                controller.ShowAdd();
            break;
            case "3":
                controller.ShowEdit();
            break;
            case "4":
                controller.ShowDelete();
            break;
            case "0":
                controller.ShowExit();
            break;
            default:
                Show();
            break;
        }
    }
}
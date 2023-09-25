namespace PhoneBook;

class DeleteView : BaseView
{
    private readonly Controller controller;

    public DeleteView(Controller controller)
    {
        this.controller = controller;
    }

    public override void Body()
    {
        Console.WriteLine("Delete Contact");
        Console.ReadLine();
        controller.ShowList();
    }
}
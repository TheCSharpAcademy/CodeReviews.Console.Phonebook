namespace PhoneBook;

class DeleteView : BaseView
{
    private readonly Controller controller;
    private readonly Contact contact;

    public DeleteView(Controller controller, Contact contact)
    {
        this.controller = controller;
        this.contact = contact;
    }

    public override void Body()
    {
        Console.WriteLine("Delete Contact");
        Console.WriteLine($"Name: {contact.Name}");
        Console.WriteLine("Are you sure? [y/n]");
        switch (Console.ReadKey().KeyChar.ToString())
        {
            case "y":
                controller.Delete(contact.ContactID);
                break;
            default:
                controller.ShowList();
                break;
        }
    }
}
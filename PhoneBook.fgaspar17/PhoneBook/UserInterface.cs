namespace PhoneBook;

public class UserInterface
{
    public void Run()
    {
        ContactMenuHandler contactMenuHandler = new ContactMenuHandler();
        contactMenuHandler.Display();
    }
}
namespace PhoneBook;

class MainController
{
    private readonly PhoneBookContext phoneBookContext;
    private readonly CategoryController categoryController;
    private readonly ContactController contactController;
    private readonly MessageController messageController;

    public MainController(PhoneBookContext phoneBookContext)
    {
        this.phoneBookContext = phoneBookContext;
        categoryController = new CategoryController(this, phoneBookContext);
        contactController = new ContactController(this, phoneBookContext);
        messageController = new MessageController(this);
    }

    public void ShowCategories()
    {
        categoryController.ShowList();
    }

    public void ShowContacts(Category category)
    {
        phoneBookContext.Entry(category).Collection(c => c.Contacts).Load();
        contactController.ShowList(category);
    }

    public void ShowContactDetails(Contact contact, string? message)
    {
        contactController.ShowDetails(contact, message);
    }

    public void SendMail(Contact contact)
    {
        messageController.ShowCreateMail(contact);
    }

    public void SendSms(Contact contact)
    {
        messageController.ShowCreateSms(contact);
    }

}
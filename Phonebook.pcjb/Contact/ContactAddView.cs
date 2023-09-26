namespace PhoneBook;

class ContactAddView : BaseView
{
    private readonly ContactController controller;
    private readonly Category category;

    public ContactAddView(ContactController controller, Category category)
    {
        this.controller = controller;
        this.category = category;
    }

    public override void Body()
    {
        var contact = new ContactDto();
        bool isValid;

        Console.WriteLine("Add Contact");
        Console.WriteLine($"Category: {category.Name}");
        Console.WriteLine("Only the name is required. Press enter to skip fields.");
        do
        {
            Console.Write("Name: ");
            contact.Name = Console.ReadLine();
            isValid = !String.IsNullOrEmpty(contact.Name);
            if (!isValid)
            {
                Console.WriteLine("Please enter a name.");
            }
        } while (!isValid);

        do
        {
            Console.Write("Phone Number: ");
            contact.PhoneNumber = Console.ReadLine();
            isValid = String.IsNullOrEmpty(contact.PhoneNumber) || Validator.IsValidPhoneNumber(contact.PhoneNumber);
            if (!isValid)
            {
                Console.WriteLine($"Please enter a valid phone number ({Validator.PhoneNumberPatternDescription}).");
            }
        } while (!isValid);

        do
        {
            Console.Write("Mobile Number: ");
            contact.MobileNumber = Console.ReadLine();
            isValid = String.IsNullOrEmpty(contact.MobileNumber) || Validator.IsValidPhoneNumber(contact.MobileNumber);
            if (!isValid)
            {
                Console.WriteLine($"Please enter a valid mobile number ({Validator.PhoneNumberPatternDescription}).");
            }
        } while (!isValid);

        do
        {
            Console.Write("Email: ");
            contact.Email = Console.ReadLine();
            isValid = String.IsNullOrEmpty(contact.Email) || Validator.IsValidEmail(contact.Email);
            if (!isValid)
            {
                Console.WriteLine($"Please enter a valid email ({Validator.EmailPatternDescription}).");
            }
        } while (!isValid);

        controller.Create(category, contact);
    }
}
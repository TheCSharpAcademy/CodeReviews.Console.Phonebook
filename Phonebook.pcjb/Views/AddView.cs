using Microsoft.Win32.SafeHandles;

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
        var contact = new ContactDto();
        bool isValid;

        Console.WriteLine("Add Contact");

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
            Console.Write("Email: ");
            contact.Email = Console.ReadLine();
            isValid = String.IsNullOrEmpty(contact.Email) || Validator.IsValidEmail(contact.Email);
            if (!isValid)
            {
                Console.WriteLine("Please enter a valid email.");
            }
        } while (!isValid);

        do
        {
            Console.Write("Phone Number: ");
            contact.PhoneNumber = Console.ReadLine();
            isValid = String.IsNullOrEmpty(contact.PhoneNumber) || Validator.IsValidPhoneNumber(contact.PhoneNumber);
            if (!isValid)
            {
                Console.WriteLine("Please enter a valid phone number.");
            }
        } while (!isValid);

        controller.Create(contact);
    }
}
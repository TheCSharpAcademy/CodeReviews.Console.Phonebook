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
        string? name;
        string? email;
        string? phoneNumber;
        bool isValid;

        Console.WriteLine("Add Contact");

        do
        {
            Console.Write("Name: ");
            name = Console.ReadLine();
            isValid = !String.IsNullOrEmpty(name);
            if (!isValid)
            {
                Console.WriteLine("Please enter a name.");
            }
        } while (!isValid);

        do
        {
            Console.Write("Email: ");
            email = Console.ReadLine();
            isValid = String.IsNullOrEmpty(email) || Validator.IsValidEmail(email);
            if (!isValid)
            {
                Console.WriteLine("Please enter a valid email.");
            }
        } while (!isValid);

        do
        {
            Console.WriteLine("Phone Numer: ");
            phoneNumber = Console.ReadLine();
            isValid = String.IsNullOrEmpty(phoneNumber) || Validator.IsValidPhoneNumber(phoneNumber);
            if (!isValid)
            {
                Console.WriteLine("Please enter a valid phone number.");
            }
        } while (!isValid);

        Console.ReadLine();

        controller.ShowMenu();
    }
}
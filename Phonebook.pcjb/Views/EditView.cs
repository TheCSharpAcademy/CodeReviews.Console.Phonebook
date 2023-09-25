namespace PhoneBook;

class EditView : BaseView
{
    private readonly Controller controller;
    private readonly Contact contact;

    public EditView(Controller controller, Contact contact)
    {
        this.controller = controller;
        this.contact = contact;
    }

    public override void Body()
    {
        var editedContact = new ContactDto();
        bool isValid;

        Console.WriteLine("Edit Contact");

        Console.WriteLine($"Old Name: {contact.Name}");
        do
        {
            Console.Write("New Name: ");
            editedContact.Name = Console.ReadLine();
            isValid = !String.IsNullOrEmpty(editedContact.Name);
            if (!isValid)
            {
                Console.WriteLine("Please enter a name.");
            }
        } while (!isValid);

        Console.WriteLine($"Old Email: {contact.Email}");
        do
        {
            Console.Write("New Email: ");
            editedContact.Email = Console.ReadLine();
            isValid = String.IsNullOrEmpty(editedContact.Email) || Validator.IsValidEmail(editedContact.Email);
            if (!isValid)
            {
                Console.WriteLine("Please enter a valid email.");
            }
        } while (!isValid);

        Console.WriteLine($"Old Phone Number: {contact.PhoneNumber}");
        do
        {
            Console.Write("New Phone Number: ");
            editedContact.PhoneNumber = Console.ReadLine();
            isValid = String.IsNullOrEmpty(editedContact.PhoneNumber) || Validator.IsValidPhoneNumber(editedContact.PhoneNumber);
            if (!isValid)
            {
                Console.WriteLine("Please enter a valid phone number.");
            }
        } while (!isValid);

        controller.Update(contact.ContactID, editedContact);
    }
}
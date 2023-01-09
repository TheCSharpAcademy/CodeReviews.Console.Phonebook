using PhoneBookConsole.Validations;

namespace PhoneBookConsole.Input;

public class UserInput : IUserInput
{
    private readonly IInputValidation _inputValidation = new InputValidation();
    public string GetInput()
    {
        return Console.ReadLine()!.Trim();
    }

    public string GetName()
    {
        Console.Write("Enter name: ");
        var name = GetInput();
        while (!_inputValidation.ValidateName(name))
        {
            Console.WriteLine("Wrong input!");
            Console.Write("Enter name: ");
            name = GetInput();
        }

        return name;
    }

    public string GetPhoneNumber()
    {
        Console.Write("Enter phone number (format: +XXX XXXXXXXXXX eg. +234 1234567890, +7 1234567890, +95 1234567890): ");
        var phoneNumber = GetInput();
        while (!_inputValidation.ValidatePhoneNumber(phoneNumber))
        {
            Console.WriteLine("Wrong input!");
            Console.Write("Enter phone number (format: +XXX XXXXXXXXXX eg. +234 1234567890, +7 1234567890, +95 1234567890): ");
            phoneNumber = GetInput();
        }

        return phoneNumber;
    }

    public string GetEmail()
    {
        Console.Write("Enter email (format: XXXXXXX@XXXXX.XXXX eg. abc.def@mail.com, abcdef@mail-archive.com): ");
        var email = GetInput();
        while (!_inputValidation.ValidateEmail(email))
        {
            Console.WriteLine("Wrong input!");
            Console.Write("Enter email (format: XXXXXXX@XXXXX.XXXX): ");
            email = GetInput();
        }

        return email;
    }
}
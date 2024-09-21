namespace PhoneBook.Exceptions;

public class ReturnToMainMenuException : Exception
{
    private const string DefaultMessage = "Returning to the main menu...";
    public ReturnToMainMenuException() : base(DefaultMessage)
    {
    }
    
    public ReturnToMainMenuException(string message) : base(message)
    {
    }
    
    public ReturnToMainMenuException(string message, Exception inner) : base(message, inner)
    {
    }
}
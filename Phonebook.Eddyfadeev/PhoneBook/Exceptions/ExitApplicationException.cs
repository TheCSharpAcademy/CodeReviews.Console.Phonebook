namespace PhoneBook.Exceptions;

public class ExitApplicationException : Exception
{
    private const string DefaultMessage = "Exiting the application...";
    public ExitApplicationException() : base(DefaultMessage)
    {
    }
    
    public ExitApplicationException(string message) : base(message)
    {
    }
    
    public ExitApplicationException(string message, Exception inner) : base(message, inner)
    {
    }
}
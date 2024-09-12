namespace PhoneBookLibrary;

public interface ISender
{
    public string Receiver { get; set; }
    public bool Send(string message);
}
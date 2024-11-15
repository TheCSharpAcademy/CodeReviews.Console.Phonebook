namespace PhoneBook.mefdev.Service;

public interface INotificationSender
{
    void Send(string recipient, string message);
}
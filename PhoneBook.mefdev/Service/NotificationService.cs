using PhoneBook.mefdev.Models;

namespace PhoneBook.mefdev.Service;

internal class NotificationService
{
    private readonly INotificationSender _smsSender;
    private readonly INotificationSender _emailSender;

    public NotificationService(INotificationSender smsSender, INotificationSender emailSender)
    {
        _smsSender = smsSender;
        _emailSender = emailSender;
    }

    public async void Notify(Contact contact, string message, NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.SMS:
                _smsSender.Send(contact.Phone, message);
                break;
            case NotificationType.Email:
                _emailSender.Send(contact.Email, message);
                break;
            default:
                throw new ArgumentException("Invalid notification type");
        }
    }
}
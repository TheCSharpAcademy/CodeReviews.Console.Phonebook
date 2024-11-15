using PhoneBook.mefdev.Shared.Interfaces;
namespace PhoneBook.mefdev.Shared.ManageStates
{
    public class SentState : INotificationState
    {
        public void Handle(NotificationContext context, string recipient, string message)
        {
            Console.WriteLine($"Notification sent successfully to {recipient}.");
        }
    }
}

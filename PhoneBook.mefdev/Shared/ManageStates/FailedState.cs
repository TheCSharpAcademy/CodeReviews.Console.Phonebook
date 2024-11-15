using PhoneBook.mefdev.Shared.Interfaces;

namespace PhoneBook.mefdev.Shared.ManageStates
{
    public class FailedState : INotificationState
    {
        public void Handle(NotificationContext context, string recipient, string message)
        {
            Console.WriteLine("Notification failed to send. Please try again.");
        }
    }
}


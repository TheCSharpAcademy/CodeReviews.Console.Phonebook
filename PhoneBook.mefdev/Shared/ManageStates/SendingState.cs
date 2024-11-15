using PhoneBook.mefdev.Shared.Interfaces;
namespace PhoneBook.mefdev.Shared.ManageStates
{
    public class SendingState : INotificationState
    {
        public void Handle(NotificationContext context, string recipient, string message)
        {
            Console.WriteLine("Notification is being sent...");

            try
            {
                context.SendNotification(recipient, message);
                context.SetState(new SentState());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                context.SetState(new FailedState());
            }
        }
    }
}


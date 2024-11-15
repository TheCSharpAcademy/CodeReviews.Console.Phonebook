using PhoneBook.mefdev.Shared.Interfaces;
namespace PhoneBook.mefdev.Shared.ManageStates
{
	public class PendingState: INotificationState
	{
        public void Handle(NotificationContext context, string recipient, string message)
        {
            Console.WriteLine("Notification is pending...");
            // Transition to SendingState
            context.SetState(new SendingState());
        }
    }
}


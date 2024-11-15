using PhoneBook.mefdev.Service;
using PhoneBook.mefdev.Shared.Interfaces;

namespace PhoneBook.mefdev.Shared.ManageStates
{
    public class NotificationContext
    {
        private INotificationState _state;
        private readonly INotificationSender _smsSender;
        private readonly INotificationSender _emailSender;

        public NotificationContext(INotificationSender smsSender, INotificationSender emailSender)
        {
            _smsSender = smsSender;
            _emailSender = emailSender;
            _state = new PendingState();
        }

        public void SetState(INotificationState state)
        {
            _state = state;
        }

        public void HandleState(string recipient, string message)
        {
            _state.Handle(this, recipient, message);
        }

        public void SendNotification(string recipient, string message)
        {
            if (recipient.Contains("@"))
            {
                _emailSender.Send(recipient, message);
            }
            else
            {
                _smsSender.Send(recipient, message);
            }
        }
    }

}


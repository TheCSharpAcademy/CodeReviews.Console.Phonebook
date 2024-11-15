
using PhoneBook.mefdev.Shared.ManageStates;

namespace PhoneBook.mefdev.Shared.Interfaces;

public interface INotificationState
{
	void Handle(NotificationContext context, string recipient, string message);
}


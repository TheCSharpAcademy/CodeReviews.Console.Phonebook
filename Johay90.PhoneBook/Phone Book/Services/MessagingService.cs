public class MessagingService
{
    private IMessagingService _messagingService;

    public MessagingService(IMessagingService messagingService)
    {
        _messagingService = messagingService;
    }

    public void Send(Contact to, string title, string body)
    {
        _messagingService.Send(to, title, body);
    }
}

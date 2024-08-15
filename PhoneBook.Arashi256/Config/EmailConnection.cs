namespace PhoneBook.Arashi256.Config
{
    internal class EmailConnection
    {
        private string? _smtpServer;
        private string? _smtpUser;
        private string? _smtpPassword;
        private int _smtpPort;
        private bool _smtpSSLEnabled;
        public EmailConnection() 
        {
            AppManager appManager = new AppManager();
            _smtpServer = appManager.SMTPServer;
            _smtpUser = appManager.SMTPUser;
            _smtpPassword = appManager.SMTPPassword;
            _smtpPort = appManager.SMTPPort;
            _smtpSSLEnabled = appManager.SMTPSSL;
        }

        public string? SMTPServer { get { return _smtpServer; } }
        public string? SMTPUser { get { return _smtpUser; } }
        public string? SMTPPassword { get { return _smtpPassword; } }
        public int SMTPPort { get { return _smtpPort; } }
        public bool SMTPSSL { get { return _smtpSSLEnabled; } }
    }
}

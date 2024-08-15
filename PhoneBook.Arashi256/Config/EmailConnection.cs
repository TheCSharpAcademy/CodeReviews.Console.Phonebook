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
            _smtpServer = appManager.SmtpServer;
            _smtpUser = appManager.SmtpUser;
            _smtpPassword = appManager.SmtpPassword;
            _smtpPort = appManager.SmtpPort;
            _smtpSSLEnabled = appManager.SmtpSSL;
        }

        public string? SmtpServer { get { return _smtpServer; } }
        public string? SmtpUser { get { return _smtpUser; } }
        public string? SmtpPassword { get { return _smtpPassword; } }
        public int SmtpPort { get { return _smtpPort; } }
        public bool SmtpSSL { get { return _smtpSSLEnabled; } }
    }
}

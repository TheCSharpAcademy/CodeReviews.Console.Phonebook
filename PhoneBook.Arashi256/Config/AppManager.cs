using System.Collections.Specialized;

namespace PhoneBook.Arashi256.Config
{
    internal class AppManager
    {
        public string? DatabaseConnectionString { get; private set; }
        public string? SMTPServer { get; private set; }
        public string? SMTPUser { get; private set; }
        public string? SMTPPassword { get; private set; }
        public int SMTPPort { get; private set; }
        public bool SMTPSSL { get; private set; }

        private NameValueCollection? _appConfig;

        public AppManager()
        {
            try
            {
                _appConfig = System.Configuration.ConfigurationManager.AppSettings;
                if (_appConfig.Count == 0)
                {
                    Console.WriteLine("\nERROR: AppSettings is empty or cannot be read.\n");
                }
                else
                {
                    DatabaseConnectionString = _appConfig.Get("ConnectionString");
                    SMTPServer = _appConfig.Get("SmtpServer");
                    SMTPUser = _appConfig.Get("SmtpUser");
                    SMTPPassword = _appConfig.Get("SmtpPassword");
                    try
                    {
                        SMTPPort = Convert.ToInt32(_appConfig.Get("SmtpPort"));                     
                    } catch (FormatException e)
                    {
                        SMTPPort = 0;
                    }
                    try
                    {
                        SMTPSSL = Convert.ToBoolean(_appConfig.Get("SmtpSSL"));
                    }
                    catch (FormatException e)
                    {
                        SMTPSSL = false;
                    }
                }
            }
            catch (System.Configuration.ConfigurationErrorsException)
            {
                Console.WriteLine("\nERROR: Could not read app settings\n");
            }
        }
    }
}

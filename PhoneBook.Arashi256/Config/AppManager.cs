using System.Collections.Specialized;

namespace PhoneBook.Arashi256.Config
{
    internal class AppManager
    {
        public string? DatabaseConnectionString { get; private set; }
        public string? SmtpServer { get; private set; }
        public string? SmtpUser { get; private set; }
        public string? SmtpPassword { get; private set; }
        public int SmtpPort { get; private set; }
        public bool SmtpSSL { get; private set; }

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
                    SmtpServer = _appConfig.Get("SmtpServer");
                    SmtpUser = _appConfig.Get("SmtpUser");
                    SmtpPassword = _appConfig.Get("SmtpPassword");
                    try
                    {
                        SmtpPort = Convert.ToInt32(_appConfig.Get("SmtpPort"));                     
                    } catch (FormatException e)
                    {
                        SmtpPort = 0;
                    }
                    try
                    {
                        SmtpSSL = Convert.ToBoolean(_appConfig.Get("SmtpSSL"));
                    }
                    catch (FormatException e)
                    {
                        SmtpSSL = false;
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

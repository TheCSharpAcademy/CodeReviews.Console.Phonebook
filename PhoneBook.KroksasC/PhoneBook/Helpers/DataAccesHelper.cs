using System.Configuration;

namespace PhoneBook.Helpers
{
    internal class DataAccesHelper
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}

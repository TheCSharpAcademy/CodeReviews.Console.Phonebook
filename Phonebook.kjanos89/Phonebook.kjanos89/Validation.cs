using System.Net.Mail;

namespace Phonebook.kjanos89
{
    public class Validation
    {
        public bool CheckString(string str)
        {
            return !String.IsNullOrEmpty(str);
        }
        public bool CheckEmail(string str)
        {
            if (String.IsNullOrEmpty(str)) return false;
            try
            {
                var mailAddress = new MailAddress(str);
                return HasValidDomain(str);
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public bool HasValidDomain(string email)
        {
            string domain = email.Split('@').Last();
            int lastDotIndex = domain.LastIndexOf('.');
            if (lastDotIndex>=2)
            {
                string ending = domain.Substring(lastDotIndex + 1);
                foreach (char c in ending)
                {
                    if (Char.IsDigit(c))
                    {
                        return false; 
                    }
                }
            }
            return lastDotIndex > 0 && lastDotIndex < domain.Length - 2;
        }
    }
    
}

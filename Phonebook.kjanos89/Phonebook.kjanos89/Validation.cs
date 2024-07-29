using System.Net.Mail;

namespace Phonebook.kjanos89
{
    public class Validation
    {
        public bool CheckString(string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public bool CheckNumber(string str)
        {
            if(string.IsNullOrEmpty(str)) return false;
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckEmail(string str)
        {
            try
            {
                var mailAddress = new MailAddress(str);
                return mailAddress.Address == str && HasValidDomain(mailAddress.Host);
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
            if (lastDotIndex >= 2)
            {
                string ending = domain.Substring(lastDotIndex + 1);
                foreach (char c in ending)
                {
                    if (char.IsDigit(c))
                    {
                        return false;
                    }
                }
            }
            return lastDotIndex > 0 && lastDotIndex < domain.Length - 2;
        }

        public bool CheckName(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckPhoneNumber(string str)
        {
            if (str[0] == '+')
            {
                str = str.Substring(1);
            }
            if (str.Length < 3 || str.Length > 15)
            {
                return false;
            }
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

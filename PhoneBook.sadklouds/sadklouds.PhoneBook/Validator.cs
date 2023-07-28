namespace sadklouds.PhoneBook
{
    public static class Validator
    {
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            return true;
        }

        public static bool IsValidNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;

            foreach (char c in number)
            {
                if (!char.IsDigit(c)) return false;
            }

            if (number.Length != 10) return false;

            return true;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!email.Contains("@")) return false;
            return true;
        }
    }
}

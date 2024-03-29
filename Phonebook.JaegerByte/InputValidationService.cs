namespace Phonebook.JaegerByte
{
    internal class InputValidationService
    {
        public bool ValidateName(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                   Char.IsAsciiLetterUpper(input[0]) &&
                   !input.Any(char.IsDigit) &&
                   input.Length <= 20;
        }
        public bool ValidatePhonenumber(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                    input.All(Char.IsDigit) &&
                    input.Length <= 20;
        }
        public bool ValidateEmail(string input)
        {
            if (input.Contains("@"))
            {
                string[] split = input.Split('@');
                return split.Length == 2 &&
                    split[1].Contains('.') &&
                    !split[1].StartsWith('.') &&
                    !split[1].EndsWith('.');
            }
            return false;
        }
        public string GetInvalidMessage()
        {
            return $"invalid input.\nPress ANY key to try again";
        }
    }
}

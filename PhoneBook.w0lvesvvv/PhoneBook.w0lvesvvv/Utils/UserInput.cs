namespace PhoneBook.w0lvesvvv.Utils
{
    public static class UserInput
    {

        public static string RequestString(string message)
        {
            ConsoleUtils.DisplayMessage(message, true, true);
            return ReadString();
        }

        public static int? RequestNumber(string message)
        {
            ConsoleUtils.DisplayMessage(message, true, true);
            return ReadNumber();

        }

        public static int? ReadNumber()
        {
            string inputNumber = Console.ReadLine() ?? string.Empty;
            if (!UserInputValidation.ValidateNumber(inputNumber, out int parsedNumber)) return null;

            return parsedNumber;
        }

        public static string ReadString()
        {
            return Console.ReadLine() ?? string.Empty;
        }

    }
}

namespace sadklouds.PhoneBook
{
    public class UserInput
    {
        public static string GetContactName(string message)
        {
            Console.Write(message);
            string output = Console.ReadLine();

            while (Validator.IsValidName(output) == false)
            {
                Console.WriteLine("Invalid name");
                Console.WriteLine();
                Console.Write(message);
                output = Console.ReadLine();
            }
            return output;
        }

        public static string GetContactNumber()
        {
            Console.Write("Enter contact phone number: ");
            string output = Console.ReadLine();
            while (Validator.IsValidNumber(output) == false)
            {
                Console.WriteLine("Invalid phone number must contain 10 digits only");
                Console.Write("\nEnter contact phone number: ");
                output = Console.ReadLine();
            }
            return output;
        }

        public static string GetContactEmail()
        {
            Console.Write("Enter contact email: ");
            string output = Console.ReadLine();
            while (Validator.IsValidEmail(output) == false)
            {
                Console.WriteLine("Invalid Email");
                Console.Write("\nEnter contact email: ");
                output = Console.ReadLine();
            }
            return output;
        }
    }
}

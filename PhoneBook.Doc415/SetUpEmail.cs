using Org.BouncyCastle.Bcpg;
using Spectre.Console;
using System.Reflection;

namespace PhoneBook.Doc415;

internal class SetUpEmail
{
   static string smtpServer;
   static string userName;
   static string password;
   static string myEmail;
   static bool isFunctional=false;

    public static void SetUp()
    {
        if (!File.Exists("config.bin"))
        {
            if (!AnsiConsole.Confirm("Would you like to setup email sender?"))
            {
                isFunctional = false;
                return;
            }
            smtpServer = AnsiConsole.Ask<string>("Enter smtp Server  (example: smtp.gmail.com): ");
            userName = AnsiConsole.Ask<string>("Enter user name :");
            bool valid = false;
            do
            {
                password = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter [green]password[/]?")
                            .PromptStyle("red")
                            .Secret());
                string secondInput = AnsiConsole.Prompt(
                            new TextPrompt<string>("Reenter [green]password[/]?")
                            .PromptStyle("red")
                            .Secret());

                if (password == secondInput)
                    valid = true;
            } while (!valid);
            bool validEmail = false;
            do
            {
                myEmail = AnsiConsole.Ask<string>("Enter your email: ");
                validEmail = Validators.IsValidEmail(myEmail);
                if (!validEmail)
                    Console.WriteLine("Please enter valid email.");
            } while (!validEmail);

            isFunctional = true;

            using Stream stream = File.Open("config.bin", FileMode.Create, FileAccess.Write);
            using BinaryWriter writer = new(stream);
            writer.Write(smtpServer);
            writer.Write(userName);
            writer.Write(password);
            writer.Write(myEmail);
            Console.WriteLine("Setup Completed. Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
        else
        {
            using Stream stream= File.Open("config.bin",FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new(stream);
            smtpServer=reader.ReadString();
            userName=reader.ReadString();
            password=reader.ReadString();
            myEmail=reader.ReadString();
            isFunctional = true;
        }
    }

    public static bool isEmailFunctional()
    {
        return isFunctional;
    }
    public static string GetServer()
    {
        return smtpServer;
    }

    public static string GetUsername()
    {
        return userName;
    }

    public static string GetPassword()
    {
        return password;
    }

    public static string GetUserEmail()
    {
        return myEmail;
    }
}

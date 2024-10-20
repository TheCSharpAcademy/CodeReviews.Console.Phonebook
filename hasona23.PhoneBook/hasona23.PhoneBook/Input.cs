using hasona23.PhoneBook.Database;
using hasona23.PhoneBook.Models;
using Spectre.Console;

namespace hasona23.PhoneBook
{
    public  class Input
    {
        const int PhoneNumberLength = 12;
        public static Contact GetContact()
        {
            string name = AnsiConsole.Prompt(new TextPrompt<string>("Name: "));
            string phone = AnsiConsole.Prompt(new TextPrompt<string>("Phone: ").Validate((str)=> ValidatePhoneNumber(str) switch
            {
                true => ValidationResult.Success(),
                false => ValidationResult.Error($"Invalid Phone Number check length is {PhoneNumberLength} and has only numbers")
            }));
         
            string email = AnsiConsole.Prompt(new TextPrompt<string>("Email: ").Validate((email) => ValidateEmail(email) switch
            {
                true => ValidationResult.Success(),
                false => ValidationResult.Error("Invalid Email check length more than 15 and ends with @gmail.com or @yahoo.com or @outlook.com"),
            }));
            return new Contact(name,phone,email);
        }
        public static string GetOperation()
        {
            string option = AnsiConsole.Prompt(new SelectionPrompt<string>().AddChoices(["Add Contact", "Delete Contact","Update Contact","Get Contacts","Exit"])
                .Title("Choose Option"));
                return option;
        }
        public static Contact GetOptionalContact()
        {
            string name = AnsiConsole.Prompt(new TextPrompt<string>("[[Optional]] Name: ").AllowEmpty());
            
            string phone = AnsiConsole.Prompt(new TextPrompt<string>("[[Optional]] Phone: ").Validate((str) => ValidatePhoneNumber(str,true) switch
            {
                true => ValidationResult.Success(),
                false => ValidationResult.Error($"Invalid Phone Number check length is {PhoneNumberLength} and has only numbers")
            }).AllowEmpty());

            string email = AnsiConsole.Prompt(new TextPrompt<string>("[[Optional]] Email: ").Validate((email) => ValidateEmail(email,true) switch
            {
                true => ValidationResult.Success(),
                false => ValidationResult.Error("Invalid Email check length more than 15 and ends with @gmail.com or @yahoo.com or @outlook.com"),
            }).AllowEmpty());
            return new Contact(name,phone,email);
        }
        public static Contact ChooseContact()
        {
            return AnsiConsole.Prompt(new SelectionPrompt<Contact>().
                AddChoices(ContactsDB.GetAllContacts()).EnableSearch().SearchPlaceholderText("Seaching Enabled").MoreChoicesText("[green]Go Down for more option[/]"));

        }
        static bool ValidateEmail(string email, bool empty = false)
        {
            if (empty && string.IsNullOrEmpty(email))
                return true;
            bool suffix = email.EndsWith("@gmail.com") || email.EndsWith("@yahoo.com") || email.EndsWith("@outlook.com");
            bool lengthValidation = email.Length > 15;
            return suffix && lengthValidation;
        }
        static bool ValidatePhoneNumber(string phone,bool empty = false)
        {
            if (empty && string.IsNullOrEmpty(phone))
                return true;
            return phone.Length == PhoneNumberLength && Int64.TryParse(phone,out _);
        }
        
    }
}

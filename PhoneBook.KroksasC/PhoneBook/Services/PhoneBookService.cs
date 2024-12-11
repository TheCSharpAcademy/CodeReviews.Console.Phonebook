using PhoneBook.Models;

namespace PhoneBook.Services
{
    internal class PhoneBookService
    {
        static internal Contact UpdateContact()
        {
            Console.Clear();
            var option = UI.GetContactOptionInput();
            Console.Clear();
            Console.WriteLine("Updating contact: ");
            UI.ShowCurrentContact(option);
            Console.WriteLine();
            string? name = UI.GetContactName();
            string? email = UI.GetContactEmail();
            string? num = UI.GetContactPhone();
            option.Name = name; 
            option.Email = email;
            option.PhoneNumber = num;
            return option;
        }
        public static void SendEmail()
        {
            PhoneBookController.ViewContacts();
            EmailService.SendEmail(UI.GetEmailInput());
        }

    }
}

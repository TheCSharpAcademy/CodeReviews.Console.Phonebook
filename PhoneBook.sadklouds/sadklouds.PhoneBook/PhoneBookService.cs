namespace sadklouds.PhoneBook;
public class PhoneBookService
{
    private PhoneBookController controller = new();
    public void Run()
    {
        bool exit = false;
        while (exit == false)
        {  
            Console.Clear();
            Console.WriteLine("\n______Contacts______");
            controller.ReadAll();
            Console.WriteLine();
            Console.WriteLine("A) Add New Contact");
            Console.WriteLine("V) View Contact");
            Console.WriteLine("E) Edit Contact");
            Console.WriteLine("D) Delete Contact");
            Console.WriteLine("0) Exit");
            Console.WriteLine("____________");
            Console.Write("Select an option: ");
            string option = Console.ReadLine();
            switch (option.ToLower())
            {
                case "a":
                    controller.CreateContact();
                    break;
                case "v":
                    string currentName = UserInput.GetContactName("Enter contact name: ");
                    controller.ReadContact(currentName);
                    break;
                case "e":
                    controller.UpdateContact();
                    break;
                case "d":
                    controller.DeleteContact();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}

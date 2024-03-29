using Spectre.Console;
namespace Phonebook.JaegerByte
{
    internal class InputManager
    {
        static InputValidationService IvService { get; set; }
        public InputManager()
        {
            IvService = new InputValidationService();
        }
        public MenuOption GetMenuSelection()
        {
            SelectionPrompt<MenuOption> selectionPrompt = new SelectionPrompt<MenuOption>();
            selectionPrompt.Title = "Select option";
            foreach (MenuOption option in Enum.GetValues(typeof(MenuOption)))
            {
                selectionPrompt.AddChoice(option);
            }
            return AnsiConsole.Prompt(selectionPrompt);
        }
        public DataModel GetInsertInput()
        {
            string firstName;
            string lastName;
            string phoneNumber;
            string email;

            while (true)
            {
                Console.Clear();
                firstName = AnsiConsole.Ask<string>("Type in new first name: ");
                if (IvService.ValidateName(firstName))
                {
                    break;
                }
                else { AnsiConsole.Write(IvService.GetInvalidMessage()); Console.ReadKey(true); }
            }
            while (true)
            {
                Console.Clear();
                lastName = AnsiConsole.Ask<string>("Type in new last name: ");
                if (IvService.ValidateName(lastName))
                {
                    break;
                }
                else { AnsiConsole.Write(IvService.GetInvalidMessage()); Console.ReadKey(true); }
            }
            while (true)
            {
                Console.Clear();
                phoneNumber = AnsiConsole.Ask<string>("Type in new phonenumber: ");
                if (IvService.ValidatePhonenumber(phoneNumber))
                {
                    break;
                }
                else { AnsiConsole.Write(IvService.GetInvalidMessage()); Console.ReadKey(true); }
            }
            while (true)
            {
                Console.Clear();
                email = AnsiConsole.Ask<string>("Type in new email: ");
                if (IvService.ValidateEmail(email))
                {
                    break;
                }
                else { AnsiConsole.Write(Program.IvService.GetInvalidMessage()); Console.ReadKey(true); }
            }
            return new DataModel(firstName, lastName, phoneNumber, email);
        }
        public string GetSearchInput()
        {
            return AnsiConsole.Ask<string>("Type in search keyword: ");
        }
        public DataModel GetDeleteInput()
        {
            List<DataModel> allContacts = Program.DbService.GetAllContacts();
            allContacts.OrderBy(p => p.LastName).ToList();
            SelectionPrompt<DataModel> selectionPrompt = new SelectionPrompt<DataModel>();
            selectionPrompt.PageSize = 30;
            foreach (var item in allContacts)
            {
                selectionPrompt.AddChoice(item);
            }
            return AnsiConsole.Prompt(selectionPrompt);
        }
        public DataModel GetUpdateInput()
        {
            List<DataModel> allContacts = Program.DbService.GetAllContacts();
            allContacts.OrderBy(p => p.LastName).ToList();
            SelectionPrompt<DataModel> selectionPrompt = new SelectionPrompt<DataModel>();
            selectionPrompt.PageSize = 30;
            foreach (var item in allContacts)
            {
                selectionPrompt.AddChoice(item);
            }
            DataModel contactToUpdate = AnsiConsole.Prompt(selectionPrompt);
            DataModel newData = GetInsertInput();
            contactToUpdate.FirstName = newData.FirstName;
            contactToUpdate.LastName = newData.LastName;
            contactToUpdate.Phonenumber = newData.Phonenumber;
            contactToUpdate.Email = newData.Email;
            return contactToUpdate;
        }
    }
}

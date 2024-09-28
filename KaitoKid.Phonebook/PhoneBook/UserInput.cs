using Spectre.Console;
using System.Text;

namespace PhoneBook
{
    public class UserInput
    {
        Validation validation = new();

        public void MainMenuChoice()
        {
            while (true)
            {
                string choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[blue] MAIN MENU[/]")
                    .PageSize(7)
                    .AddChoices(
                    new[]
                    {
                    "[springgreen2]1. Add Contact[/]", "[springgreen2]2. View Contacts[/]", 
                        "[springgreen2]3. Edit Contact[/]", "[springgreen2]4. Delete Contact[/]", 
                        "[yellow4]5. Send Email[/]", "[yellow4]6. Send SMS[/]", "[red]7. Exit[/]"
                    }));

                int opt = int.Parse(choice.Substring(choice.IndexOf(']') + 1, 1));

                if (opt == 7)
                {
                    AnsiConsole.Markup("[red] Exiting ....... [/]");
                    Thread.Sleep(1000);
                    return;
                }

                PhonebookService.PerformOperation(opt);
            }

        }

        internal int ViewMenuChoice()
        {
            string choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("[blue]VIEW CONTACTS[/]")
                            .PageSize(3)
                            .AddChoices(new[]
                            {
                                "[springgreen2]1. View All Contacts[/]", "[springgreen2]2. View Contact By Category[/]", "[red]3. Back to Main Menu[/]"
                            }));
            return int.Parse(choice.Substring(choice.IndexOf(']') + 1, 1));
        }

        internal int UpdateMenuChoice()
        {
            string choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[blue]EDIT CONTACTS[/]")
                .PageSize(5)
                .AddChoices(new[]
                {
                    "[springgreen2]1. Edit Contact Name[/]", "[springgreen2]2. Edit Contact Number[/]", "[springgreen2]3. Edit Email Id[/]","[springgreen2]4. Edit Category[/]","[red]5. Back to Main Menu[/]"
                }));

            return int.Parse(choice.Substring(choice.IndexOf(']') + 1, 1));
        }

        internal string GetEmail()
        {
            string email;
            while (true)
            {
                email = Console.ReadLine();
                if (validation.IsValidEmail(email))
                {
                    break;
                }
                Console.Write("Enter Contact email: ");
            }
            return email;
        }

        public int GetId()
        {
            int id;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    AnsiConsole.Markup("[red]Enter integer[/]\n");
                    Console.Write("Enter again: ");
                }
                break;
            }
            return id;
        }

        internal string GetNumber()
        {
            string phoneNumber;
            while (true)
            {
                phoneNumber = Console.ReadLine();
                if (validation.IsValidNumber(phoneNumber))
                {
                    break;
                }
                Console.Write("Enter Contact Number (10 digits): ");
            }
            return phoneNumber;
        }

        internal string GetText()
        {
            string name;
            while (true)
            {
                name = Console.ReadLine();
                if (validation.IsValidName(name))
                {
                    break;
                }
                Console.Write("Enter Contact Name: ");
            }
            return name.Substring(0, 1).ToUpper() + name.Substring(1);  
        }

        internal string CategoryMenu(List<(string, int)> categoryCount)
        {
            List<string> optionsList = new ();
            string choice = "";
            foreach (var category in categoryCount)
            {
                optionsList.Add($"{category.Item1}: {category.Item2} contacts");
            }
            int pageSize = optionsList.Count < 3 ? 3 : optionsList.Count;
            choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .PageSize(pageSize)
                            .AddChoices(optionsList));
            

             return choice.Substring(0, choice.IndexOf(':'));
        }

        internal string GetMessage()
        {
            StringBuilder sb = new StringBuilder();
            string line;

            Console.WriteLine("\nEnter your text (type 'END' on a new line to finish):");
            while ((line = Console.ReadLine()) != "END")
            {
                sb.Append(line);
            }
            return sb.ToString();
        }
    }
}

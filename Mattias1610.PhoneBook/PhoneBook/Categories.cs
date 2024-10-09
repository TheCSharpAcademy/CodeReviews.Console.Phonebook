using Spectre.Console;

namespace PhoneBook
{
    public class Categories
    {

        public void Menu(){
            var option = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(MenuOptions.Family,
                    MenuOptions.Friends,
                    MenuOptions.Work,
                    MenuOptions.Back)
                );

            switch(option){
                case MenuOptions.Friends:
                    ShowFriends();
                    break;
                case MenuOptions.Family:
                    ShowFamily();
                    break;
                case MenuOptions.Work:
                    ShowWork();
                    break;
                case MenuOptions.Back:
                    return;
            }
        }

        private void ShowWork()
        {
            var work = NumberController.Show().Where(x => x.Category == "Work").ToList();
            UserInterface.ShowTable(work);
        }

        private void ShowFamily()
        {
            var family = NumberController.Show().Where(x => x.Category == "Family").ToList();
            UserInterface.ShowTable(family);
        }

        private void ShowFriends()
        {
            var friends = NumberController.Show().Where(x => x.Category == "Friend").ToList();
            UserInterface.ShowTable(friends);
        }

        enum MenuOptions{
            Family,
            Work,
            Friends,
            Back
        }
    }
}
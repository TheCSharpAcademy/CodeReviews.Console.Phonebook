namespace Phonebook.kjanos89;
class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        PhonebookManipulation manipulation = new PhonebookManipulation(menu);
        menu.SetPBManipulation(manipulation);
        menu.DisplayMenu();
    }
}
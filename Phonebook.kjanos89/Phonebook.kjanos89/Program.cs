namespace Phonebook.kjanos89;
class Program
{
    static void Main(string[] args)
    {
        PhonebookManipulation manipulation = new PhonebookManipulation();
        Menu menu = new Menu(manipulation);
        menu.DisplayMenu();
    }
}
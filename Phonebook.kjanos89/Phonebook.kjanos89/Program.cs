namespace Phonebook.kjanos89;
class Program
{
    static void Main(string[] args)
    {
        using (var context = new PhonebookContext())
        {
            context.Initialize();
        }
    }
}
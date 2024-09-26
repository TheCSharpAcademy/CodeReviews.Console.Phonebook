namespace Phonebook.Library.Helpers;
public abstract class Singleton<ClassType> where ClassType : new()
{
    static Singleton() { }

    public static readonly ClassType instance = new ClassType();

    public static ClassType Run
    {
        get => instance;
    }
}

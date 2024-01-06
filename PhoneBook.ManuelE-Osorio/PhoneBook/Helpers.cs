namespace PhoneBookProgram;

public class Helpers
{
    public static List<List<object>> ClearSelection(List<List<object>> selection)
    {
        foreach(List<object> select in selection)
        {
            select[0] = "[ ]";
        }
        return selection;
    }
}
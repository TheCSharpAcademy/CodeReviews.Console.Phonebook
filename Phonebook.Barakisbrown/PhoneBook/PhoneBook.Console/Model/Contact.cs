namespace PhoneBook.Console.Model;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public override string ToString()
    {
        string choices = string.Format("{0,20}    {1,30}    {2,15}", Name, Email, PhoneNumber);
        return choices;
    }
}

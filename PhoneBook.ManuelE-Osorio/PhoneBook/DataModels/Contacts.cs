namespace PhoneBookProgram;

public class Contact
{
    public int ContactId {get; set;}
    public string ContactName {get; set;} = "";
    public List<PhoneNumber> PhoneNumbers {get; set;} = [];
    public List<Email> Emails {get; set;} = [];
    public string? Category {get; set;}

    public static Contact FromCsv(string contactLine)
    {
        string[] data = contactLine.Split(',');
        string? errorMessage;
        errorMessage = InputValidation.ContactNameValidation(data[0]);
        errorMessage ??= InputValidation.CategoryNameValidation(data[1]);
        Contact contact = new();
        if(errorMessage == null)
        {
            contact.ContactName = data[0];
            contact.Category = data[1] ;
            return contact;
        }
        else
            throw new Exception ($@"Error at: {data[0]}, {errorMessage}");
    }
}
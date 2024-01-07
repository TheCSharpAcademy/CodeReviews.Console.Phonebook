namespace PhoneBookProgram;

public class EmailDTO(Email email)
{
    public string? Email {get; set;} = email.GetEmail();
}
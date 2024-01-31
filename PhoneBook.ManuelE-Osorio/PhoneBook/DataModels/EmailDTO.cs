namespace PhoneBookProgram;

public class EmailDto(Email email)
{
    public string? Selected { get; set; } = "[ ]";
    public string? Email {get; set;} = email.GetEmail();
}
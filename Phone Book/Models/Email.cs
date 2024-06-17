using System.ComponentModel.DataAnnotations;

public class Email
{
    public int Id { get; set; }
    [EmailAddress]
    public string EmailAddress { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
}


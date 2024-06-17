using System.ComponentModel.DataAnnotations;

public class PhoneNumber
{
    public int Id { get; set; }
    [Phone]
    public string Number { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
}

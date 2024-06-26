public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Email> EmailAddresses { get; set; } = new List<Email>();
    public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

    public override string ToString()
    {
        return Name;
    }
}


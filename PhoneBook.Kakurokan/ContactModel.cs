namespace PhobeBook.Kakurokan
{
    public class ContactModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }

        public ContactModel(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Phonebook.JaegerByte
{
    internal class DataModel
    {
        public DataModel(string firstName, string lastName, string phonenumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Phonenumber = phonenumber;
            Email = email;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public override string ToString()
        {
            return $"{LastName} {FirstName} - {Phonenumber} - {Email}";
        }
    }
}

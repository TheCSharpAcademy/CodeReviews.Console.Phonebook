using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DouglasFir.Models;

public class Contact
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? Notes { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(FirstName)
            || !string.IsNullOrEmpty(LastName)
            || !string.IsNullOrEmpty(PhoneNumber)
            || !string.IsNullOrEmpty(Email)
            || !string.IsNullOrEmpty(Address)
            || !string.IsNullOrEmpty(City)
            || !string.IsNullOrEmpty(ZipCode)
            || !string.IsNullOrEmpty(Country)
            || !string.IsNullOrEmpty(Notes);
    }
}

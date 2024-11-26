﻿namespace PhoneBook.Models
{
    internal class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Parameterless constructor for EF Core
        public Contact() { }

        // Custom constructor
        public Contact(int id, string? name, string? email, string? phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}

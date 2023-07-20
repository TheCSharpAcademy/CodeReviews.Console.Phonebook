﻿using System.ComponentModel.DataAnnotations;

namespace Phonebook.Model;

internal class Contact
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public string PhoneNumber { get; set; }
    [EmailAddress]
    public string EmailAddress { get; set; }
}

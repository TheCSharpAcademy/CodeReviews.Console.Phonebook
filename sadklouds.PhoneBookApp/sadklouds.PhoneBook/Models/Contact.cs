﻿using System.ComponentModel.DataAnnotations;

namespace sadklouds.PhoneBook.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; } 
        
    }
}

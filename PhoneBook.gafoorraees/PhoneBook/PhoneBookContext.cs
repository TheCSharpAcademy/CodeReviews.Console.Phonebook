﻿using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook;

internal class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PhoneBook;Trusted_Connection=True;");
    }

}

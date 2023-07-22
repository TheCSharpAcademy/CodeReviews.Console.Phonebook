using Microsoft.EntityFrameworkCore;
using PhoneBook.JsPeanut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.JsPeanut
{
    internal class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\LocalDBDemo;Initial Catalog=Contacts;Integrated Security=True;Connect Timeout=30;Encrypt=False");
    }
}
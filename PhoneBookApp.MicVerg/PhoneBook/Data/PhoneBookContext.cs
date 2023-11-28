using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PhoneBook.Data
{
    internal class PhoneBookContext : DbContext
    {
        public DbSet<ContactModel> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PhoneBookDatabase"].ConnectionString);
        }
    }
}

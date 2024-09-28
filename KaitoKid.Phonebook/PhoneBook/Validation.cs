using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class Validation
    {
        public bool IsValidNumber(string number)
        {
            if (number.IsNullOrEmpty())
            {
                AnsiConsole.Markup("[red]Number cannot be empty[/]\n\n");
                return false;
            }

            foreach (char c in number)
            {
                if (Char.IsLetter(c) )
                {
                    AnsiConsole.Markup("[red]Number cannot contain letter[/]\n\n");
                    return false;
                }
                if (Char.IsWhiteSpace(c))
                {
                    AnsiConsole.Markup("[red]Number cannot contain whitespace[/]\n\n");
                    return false;
                }
            }
            
            if(number.Length != 10)
            {
                AnsiConsole.Markup("[red]Number should be of 10 digits[/]\n\n");
                return false;
            }
            
            return true;
        }

        public bool IsValidName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                AnsiConsole.Markup("[red]Name cannot be empty[/]\n\n");
                return false;
            }

            if (Char.IsWhiteSpace(name[0]))
            {
                AnsiConsole.Markup("[red]Name cannot begin with whitespace[/]\n\n");
                return false;
            }
         
            return true;
        }

        public bool IsValidEmail(string? email)
        {
            string pattern = @"^[^@\s]+@(gmail\.com|yahoo\.com|outlook\.com)$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(email))
            {
                AnsiConsole.Markup("[red]Enter email with proper domain name (gmail.com/ yahoo.com/ outlook.com) and should not contain whitespace or extra @ character[/]\n\n");
                return false;
            }
            return true;
        }
    }
}

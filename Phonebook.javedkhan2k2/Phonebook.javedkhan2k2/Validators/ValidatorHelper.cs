using System.Text.RegularExpressions;
using Phonebook.Entities;

namespace Phonebook.Validators;

public static class ValidatorHelper
{
    public static bool IsValidName(string value) => Regex.IsMatch(value, @"^[a-zA-Z][a-zA-Z\s]{2,}$");

    public static bool IsValidEmail(string value) => Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    public static bool IsValidPhoneNumber(string value) => Regex.IsMatch(value, @"^0\d{11}$");

}
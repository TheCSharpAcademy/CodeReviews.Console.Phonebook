using System.Text.RegularExpressions;
using Microsoft.Identity.Client;

namespace Phonebook.Console.Helpers;

public static class AppValidator {
    public static bool IsValidPhoneNumber(string phoneNumber) {
        string pattern = @"^\d{3}-\d{3}-\d{4}$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(phoneNumber);
    }

    public static bool IsValidPhoneNumberOrEmpty(string phoneNumber) {
        string pattern = @"^\d{3}-\d{3}-\d{4}$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(phoneNumber) || phoneNumber == "";
    } 

    public static bool IsValidEmail(string email) {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(email);
    }

    public static bool IsValidEmailOrEmpty(string email) {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(email) || email == "";
    } 

    public static bool IsValidFullName(string name) {
        string pattern = @"^([A-Za-z]+(?: [A-Za-z]+)*)$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(name);
    } 

    public static bool IsValidFullNameOrEmpty(string name) {
        string pattern = @"^([A-Za-z]+(?: [A-Za-z]+)*)$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(name) || name == "";
    } 
}
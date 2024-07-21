using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.kjanos89
{
    public class PhonebookManipulation
    {
        public void AddContact()
        {
            Console.WriteLine("Please type in the name of the contact:");
            string name=Console.ReadLine();
            Console.WriteLine("Please type in the phone number of the contact:");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Please type in the e-mail address of the contact:");
            string email = Console.ReadLine();

            var contact= new Contact { Name=name, PhoneNumber=phoneNumber, Email=email };

            using (var context = new PhonebookContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }
        public void GetContacts()
        {
            using (var context = new PhonebookContext())
            {
                List<Contact> list = context.Contacts.ToList();
                if (list.Any())
                {
                    foreach (var contact in list)
                    {
                        Console.WriteLine($"The contact's id is {contact.Id}, name: {contact.Name}, phone number: {contact.PhoneNumber}, e-mail address: {contact.Email}.");
                    }
                }
                else
                {
                    Console.WriteLine("Your contact list is currently empty. Try adding some information first!");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                }
            }
        }
        public void UpdateContact()
        {
            Console.Clear();
            Console.WriteLine("Here is the list of all your contacts:");
            GetContacts();
            Console.WriteLine("\n");
            Console.WriteLine("Please type in the currently used name of the contact:");
            string name= Console.ReadLine();
            using (var context = new PhonebookContext())
            {
                var searchedContact = context.Contacts.FirstOrDefault(c => c.Name == name);
                if (searchedContact != null)
                {
                    Console.WriteLine("Please type in the name of the contact or leave empty if you don't want to modify it:");
                    string newName = Console.ReadLine();
                    Console.WriteLine("Please type in the phone number of the contact or leave empty if you don't want to modify it:");
                    string phoneNumber = Console.ReadLine();
                    Console.WriteLine("Please type in the e-mail address of the contact or leave empty if you don't want to modify it:");
                    string email = Console.ReadLine();
                    if(!String.IsNullOrEmpty(newName))
                    {
                        searchedContact.Name = newName;
                    }
                    if(!String.IsNullOrEmpty(phoneNumber))
                    {
                        searchedContact.PhoneNumber = phoneNumber;
                    }
                    if(!String.IsNullOrEmpty(email))
                    {
                        searchedContact.Email = email;
                    }
                    context.Contacts.Update(searchedContact);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Can't find contact with the given information.");
                }
                
            }
        }
        /*public void DeleteContact()
        {
            using(var context = new PhonebookContext())
            {
                var contact = context.Contacts.Find();
                if (contact != null)
                {
                    context.Contacts.Remove(contact);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("No contact found with the id. Try again:");
                    DeleteContact();
                }
            }
        }*/

    }
}

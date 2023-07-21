using Microsoft.EntityFrameworkCore;
using Phonebook.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Phonebook.Controllers;

internal class PhonebookController
{
	internal static void AddContact(Contact contact)
	{
		using var db = new PhonebookContext();
		db.Add(contact);
		db.SaveChanges();
	}
	internal static List<Contact> GetContacts()
	{
		using var db = new PhonebookContext();
		var contacts = db.Contacts
			.Include(x => x.Category)
			.ToList();
		return contacts;
	}
	internal static Contact GetContactById(int id)
	{
		using var db = new PhonebookContext();
		var contact = db.Contacts
			.Include(x => x.Category)
			.SingleOrDefault(x => x.ContactId == id);
		return contact;
	}
	internal static void DeleteContact(Contact contact)
	{
		using var db = new PhonebookContext();
		db.Remove(contact);
		db.SaveChanges();
	}
	internal static void UpdateContact(Contact contact)
	{
		using var db = new PhonebookContext();
		db.Update(contact);
		db.SaveChanges();
	}
	internal static void SendSMS(Contact contact)
	{
		Console.WriteLine("Enter the SMS text: ");
		string body = Console.ReadLine();

		var accountSid = "AC0761fbc9ed4c6da7129dc69aa95800c9";
		var authToken = Environment.GetEnvironmentVariable("Twilio_SMS_Token");
		TwilioClient.Init(accountSid, authToken);

		//added the +32 in front, which is the landcode for belgium
		//my test account at twilio only let me send to my own number for free
		var messageOptions = new CreateMessageOptions(
		  new PhoneNumber("+32" + contact.PhoneNumber));
		messageOptions.From = new PhoneNumber("+12727882478");
		messageOptions.Body = body;


		var message = MessageResource.Create(messageOptions);
	}
	internal static async Task SendEmail(Contact contact)
	{
		Console.WriteLine("Enter the subject for the email: ");
		string subject = Console.ReadLine();
		Console.WriteLine("Enter the text for the email: ");
		string body = Console.ReadLine();
		string toEmail = contact.EmailAddress;

		string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
		if (string.IsNullOrEmpty(apiKey))
		{
			throw new Exception("Sendgrid API key not found in environment variable.");
		}
		var client = new SendGridClient(apiKey);
		var from = new EmailAddress("carlmalfliet@proximus.be", "Carl's Phonebook app");
		var to = new EmailAddress(toEmail);
		var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent: body);
		var response = await client.SendEmailAsync(msg);
	}
}
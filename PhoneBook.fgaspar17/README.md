# PhoneBook

A console-based application to help you manage your contacts.
Developed using C#, Entity Framework, Twilio, Gmail, Spectre.Console, 
and SQL Server Express LocalDB.

## Given Requirements

- [x] This is an application where you should record contacts with their phone numbers.
- [x] Users should be able to Add, Delete, Update and Read 
from a database, using the console.
- [x] You need to use Entity Framework, raw SQL isn't allowed.
- [x] Your code should contain a base Contact class with AT LEAST 
{Id INT, Name STRING, Email STRING and Phone Number(STRING)}
- [x] You should validate e-mails and phone numbers and 
let the user know what formats are expected
- [x] You should use Code-First Approach, 
which means EF will create the database schema for you.
- [x] You should use SQL Server, not SQLite

## Features

- SQL Server database connection

  - The data is stored in a SQL Server database. I connect to it for the CRUD.
  - The database is managed by Entity Framework. 
  You should add an initial migration and update the database first.

- Console-based UI to navigate the menus

  - ![image](https://github.com/user-attachments/assets/26874d6f-d7fc-4d56-b62c-0c2c1a284513)
  - ![image](https://github.com/user-attachments/assets/b5b3dd61-790c-41de-bd2c-8f5bddf85b91)
  - ![image](https://github.com/user-attachments/assets/a8114bd9-02a7-4125-ab5b-a892e49c199e)

- CRUD operations

  - From the Contact Menu, you can create, show, or delete contacts.
  - From the Category Menu, you can create, update, show, or delete categories.
  - You can assign categories to contacts in the Group Contacts by Category Menu.
  To choose an option, you make use of arrow keys and enter.
  - Inputs are validated. For email and phone number you can check the given examples.
  - ![image](https://github.com/user-attachments/assets/9b7a3657-aa2e-49f4-895f-c3bb955b1f22)
  - You can cancel an operation by entering the string from the configuration file.

- Send Email
  
  - First, you need to create an application password for your google account, then
  you can enter your gmail and password in user secrets or the configuration file.
  - Then you can pick the Send Mail option, and you fill in the information needed.
  - ![image](https://github.com/user-attachments/assets/405e34f5-3a51-48b6-9048-6ba5e4efd11f)

- Send SMS

  - I'm using the Twilio API, so you need a Twilio account first.
  - In the user secrets or configuration file you enter the Twilio SID, auth token, and number.
  - Then you can pick the Send SMS option, and you write the message.
  - ![image](https://github.com/user-attachments/assets/1fa066a3-f06a-4c05-948b-7154c182056a)

## Challenges

- Getting used to Entity Framework.
- Validating phone numbers and emails.
- Sending mails to contacts.
- Working with Twilio API.
- Managing the contact and category relationship.

## Lessons Learned

- The command dotnet ef to add migrations and update the database schema.
- Constraints in EntityFramework: compound primary key and UNIQUE.
- Managing User Secrets to avoid leaking credentials in GitHub.
- Sending emails via System.Net.Mail.
- Sending SMS thanks to Twilio API.
- Using Regex to validate emails and phone numbers.
- Implementing the Factory pattern to abstract the Sender implementation.

## Areas to Improve

- EntityFramework is a bast library; I need to improve working with it.
- Class organization and meaningful names.
- The dotnet command options.

## Resources used

- StackOverflow posts
- ChatGPT
- [Twilio Documentation](https://www.twilio.com/docs/messaging/quickstart/csharp-dotnet-core)
- [DbContext Setup](https://learn.microsoft.com/es-es/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli)
- [SMTPClient Send Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient.send?view=net-8.0)
- [EntityFramework Documentation](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
- [Configuration in Console Apps Video](https://www.youtube.com/watch?v=z7w-aheVrC4)

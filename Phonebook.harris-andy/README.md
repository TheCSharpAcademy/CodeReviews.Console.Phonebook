# Phonebook_cSharpAcademy

This is my submission for the cSharpAcademy Phonebook project found here: [Phonebook Project](https://www.thecsharpacademy.com/project/16/phonebook)


## Project Description
- A small console app in which the user can create a phonebook of contacts (name, phone number, email), create contact categories and send emails through the console. Data is stored through SQL Server.
- Phone number formatting is verified using libphonenumbers-dotnet. Email formatting verified with MailKit.
- Sending email handled using MimeKit and MailKit
- Built with C#/.Net 8, Entity Framework, SQL Server, Spectre Console, Azure Data Studio, Docker 


## Usage
- Follow the instructions and away you go
- Select from the menu to perform operations such as: adding/deleting/updating/viewing contacts and categories or sending an email.


![main menu](/Images/mainMenu.png)


## Appsettings.json Setup
- create "appsettings.json" in root folder (where .csproj exists)
- add email object with your email and password as shown in image:
- note that for Gmail an app passkey is required, NOT your standard login password
- include your connection string with database login and password


![email setup](/Images/appsettings.png)


## Features
- Make a phonebook. Neat!
- Add names, phone numbers, emails and categories (what's up one-to-many)! WOW!


![contacts](/Images/contacts.png)


- Send an email through the console. What an age to be alive!


![email draft](/Images/email.png)


## More to do
- I'd like to improve the method calls between the controller and other classes.
- I need practice handling many-to-many relationships which weren't included in this project.
- I didn't add SMS capabilities because it required an Azure Data account and the page had technical problems.


## New Stuff & Things I Learned. Neat!
- Entity Framework is quite handy. Very cool abstraction of SQL queries.
- Sending emails through MailKit is surprisingly easy (and very cool).


## Questions & Comments
- I'm still working on organization. When it got to the final refactoring, I found myself kind of stuck figuring out how to call methods outside the controller without making everything static.
- I chose to use one Controller because it makes sense to me. I like having one location for all user interactions.
- CategoryDataManager uses static functions (it was easier) but it's the only one - is that bad practice?
- Should the database migrations folder be added to gitignore?

# ConsoleDrinks

## Given Requirements:
- [x] This is an application where you can record contacts with their phone numbers.
- [x] You should be able to Add, Delete, Update and Read from a database, using the console.
- [x] Using only Entity Framework, raw SQL isn't allowed.
- [x] Usage of SQL Server.
- [x] Validate e-mails and phone numbers and let the user know what formats are expected.

## Features

* SQL Server database connection with Entity Framework.
> [!IMPORTANT]
> After downloading the project, you should check appsetting.json and write your own path to connect the db.
> 
> ![image](https://github.com/TwilightSaw/CodeReviews.Console.Phonebook/blob/main/Phonebook.TwilightSaw/images/appsettings.png)

> [!IMPORTANT]
 > Also you should do starting migrations to create db with all necessary tables, simply write ```dotnet ef database update``` in CLI.
 > 
 > ![image](https://github.com/TwilightSaw/CodeReviews.Console.Phonebook/blob/main/Phonebook.TwilightSaw/images/migrations.png)

* A console based UI where you can navigate by user input.

   ![image](https://github.com/TwilightSaw/CodeReviews.Console.Phonebook/blob/main/Phonebook.TwilightSaw/images/ui.png)

* CRUD abilities of both contacts and categories of contacts.

   ![image](https://github.com/TwilightSaw/CodeReviews.Console.Phonebook/blob/main/Phonebook.TwilightSaw/images/crud.png)

   ![image](https://github.com/TwilightSaw/CodeReviews.Console.Phonebook/blob/main/Phonebook.TwilightSaw/images/sending.png)

* Possibilities to send SMS and emails to the contact.
> [!IMPORTANT]
> You need to adjust data in the appsettings.json with your data to have these features working.
> 
> ![image](https://github.com/TwilightSaw/CodeReviews.Console.Phonebook/blob/main/Phonebook.TwilightSaw/images/settings.png)

## Challenges and Learned Lessons
- Working with Many to Many tables is a little frustrating, but when you understand how it works it's a lot easier.
- Sending emails and SMS is not as hard as I thought and is very useful feature.
- appsettings.json is a handy space to locate necessary strings.

## Areas to Improve
- Better usage of delegates and generic type parameters.

## Resources Used
- C# Academy guidelines and roadmap.
- ChatGPT for new information as emails and SMS sending, EF usage, API usage etc..
- Spectre.Console documentation.
- EF documentation.
- Various StackOverflow articles.
﻿

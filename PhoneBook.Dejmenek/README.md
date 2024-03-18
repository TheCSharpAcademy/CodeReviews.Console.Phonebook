# PhoneBook

## Table of Contents
- [General Info](#general-info)
- [Technologies](#technologies)
- [Features](#features)
- [Examples](#examples)
- [Instalation and Setup](#instalation-and-setup)
- [Requirements](#requirements)
- [Challenges](#challenges)
- [Things Learned](#things-learned)
- [Used Resources](#used-resources)

## General Info
Project made for @TheCSharpAcademy.  
This project is a simple phonebook application that allows you to manage your contacts and categories, and even send emails to them directly.

## Technologies
- C#
- SQL Server
- Entity Framework Core
- [Spectre.Console](https://github.com/spectreconsole/spectre.console)

## Features
- Create and manage contacts and categories.
- Send emails to your contacts.
- Search for contacts by category.
- User-friendly interface: Provides clear menus and prompts for interaction.
- Input validation: Ensure data entered by the user is valid.

## Examples
- Main Menu  
- Manage Contacts    
- Manage Categories  
- View Contacts 
- View Contacts by Category  

## Instalation and Setup
1. Clone or download this project repository.
2. Open the solution file (PhoneBook.Dejmenek.sln) in Visual Studio.
3. Install the required NuGet packages:
	- libphonenumber-csharp
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.EntityFrameworkCore.Design
	- Spectre.Console
	- Spectre.Console.Cli
	- System.Configuration.ConfigurationManager
4. Update the App.config file with your SQL Server connection string details.
  
## Requirements
- [x] This is an application where you should record contacts with their phone numbers.
- [x] Users should be able to Add, Delete, Update and Read from a database, using the console.
- [x] You need to use Entity Framework, raw SQL isn't allowed.
- [x] Your code should contain a base Contact class with AT LEAST {Id INT, Name STRING, Email STRING and Phone Number(STRING)}
- [x] You should validate e-mails and phone numbers and let the user know what formats are expected.
- [x] You should use Code-First Approach, which means EF will create the database schema for you.
- [x] You should use SQL Server, not SQLite.

## Challenges
- [x] Create a functionality that allows users to add the contact's e-mail address and send an e-mail message from the app.
- [x] Expand the app by creating categories of contacts (i.e. Family, Friends, Work, etc).
- [] What if you want to send not only e-mails but SMS?

## Things Learned
While working on this project, I've spent some time to learn about Entity Framework Core and LINQ
(I learned some syntax and practised with exercises on filtering, grouping, joining and projecting data).
For validating phone numbers I've used [libphonenumber-csharp](https://github.com/twcclegg/libphonenumber-csharp) library, because there is so many things to consider when validating phone numbers. 
There is no need to reinvent the wheel. Whereas for sending emails I've used Gmail's SMTP server.

## Used Resources
- [Entity Framework Core Tutorial](https://www.youtube.com/watch?v=SryQxUeChMc&list=PLdo4fOcmZ0oX7uTkjYwvCJDG2qhcSzwZ6)
- [LINQ Tutorial](https://www.tutorialsteacher.com/linq) used to learn syntax and practise with exercises
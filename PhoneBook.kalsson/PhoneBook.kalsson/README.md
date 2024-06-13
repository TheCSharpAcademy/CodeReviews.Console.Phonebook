# Phone Book Application

This is a console application written in C# 12, which uses .NET 8.0 Framework. 
The application uses SQL Server with Entity Framework as an ORM.

## Description

Phone Book is a console application that lets you manage a list of contacts. 
It allows you to perform common contact management tasks, such as:

- Viewing all contacts in the phone book
- Adding new contacts
- Updating existing contacts
- Deleting contacts

Each contact consists of:

- First name
- Last name
- Email address
- Phone number

The application uses Spectre.Console for user interaction.

## Installation

To install this application:

1. Clone this repository

2. Install the necessary NuGet packages by running `dotnet restore` in the project directory

3. Set up your SQL Server connection string in `appsettings.json`

## Usage

To run the application, navigate to the directory containing the project file and use the `dotnet run` command.

The application will display a menu with options. Choose the desired option by typing its corresponding number and hitting "Enter".

### Adding a Contact

When you choose to add a contact, you must provide the contact's first name, last name, email, and phone number.

### Updating a Contact

When you choose to update an existing contact, you will be prompted to select a contact. Then, you can enter new values for the contact's fields.

### Deleting a Contact

To delete a contact, you will need to select a contact from the presented list.
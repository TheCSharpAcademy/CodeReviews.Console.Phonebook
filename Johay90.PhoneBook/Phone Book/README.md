# Phone Book Application

## Introduction

This project demonstrates a basic Phone Book application built using C# and Entity Framework. It allows users to manage contacts with their phone numbers and email addresses through a console interface. The application follows the Code-First approach, using SQL Server for the database, and includes features for adding, updating, deleting, and viewing contacts. Additionally, it provides functionality for sending emails, although this is currently mocked for development purposes.

## Features

- **Add, Update, Delete, and View Contacts:** Manage contacts with their phone numbers and email addresses.
- **Email Validation:** Ensures email addresses follow proper formats.
- **Phone Number Validation:** Ensures phone numbers follow proper formats.
- **Mock Email Sending:** Uses a strategy pattern to send emails. Actual email sending is mocked using tools like Papercut for development and testing.
- **Expandable Messaging Service:** Allows for future implementation of additional messaging services, such as SMS.

## Requirements

- .NET 6.0 or later
- SQL Server
- Papercut (or similar email mocking tool)

## Usage

### Setting up the Database and Application

1. **Install SQL Server:** Ensure you have SQL Server installed and running. Update the connection string in `PhoneBookContext.cs` to match your SQL Server configuration.
2. **Configure the Connection String**: Update the connection string to match your SQL Server configuration. The connection string can be found in `app.config`
3. **Configure the Database:** The application uses Entity Framework Code-First approach. Run the following command to set up the database:
```c#
Update-Database
```
4. **Run the Application:** Start the application

### Mock Email Sending

For email sending, the application uses a mocked service. To properly test email functionality:

1. **Install Papercut:** Download and install Papercut from [Papercut's official website](https://github.com/ChangemakerStudios/Papercut-SMTP).
2. **Configure Email Service:** The `EmailService` class is configured to use Papercut's default settings (localhost, port 25). Ensure Papercut is running when you test email functionality.
3. **Send Emails:** The application includes a `SendMessage` option in the menu to send emails. Emails will be captured by Papercut.

### Running the Application

1. Start the application and follow the console prompts to add, update, delete, and view contacts.
2. Use the `SendMessage` option to send emails to contacts. Emails will be handled by Papercut for development purposes.

## Potential Changes

### Abstracting `App.Run`

Currently, the `App.Run` method handles both menu navigation and business logic. This can be improved by abstracting it to hold only menus and enums, while moving logic to controller or service classes:

1. **Controller:** Handle user input and orchestrate calls to service methods.
2. **Service:** Implement business logic and interact with repositories.
3. **Repository:** Handle direct database interactions.

## Project Structure

- **Controllers:** Handle user input and manage the flow of the application.
- **Services:** Implement business logic.
- **Repositories:** Interact with the database using Entity Framework.
- **Models:** Define the data structures for contacts, emails, and phone numbers.
- **Utilities:** Provide validation and user input handling.

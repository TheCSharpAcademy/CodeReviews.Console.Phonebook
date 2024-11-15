# PhoneBook Application Documentation

## Overview
PhoneBook is a simple yet effective contact management application that allows users to store and manage their contacts. The application provides basic CRUD (Create, Read, Update, Delete) operations for contact management.

## Features

### 1. Contact Management
- Add new contacts with details:
    - First Name
    - Last Name
    - Phone Number
    - Email Address
- View list of all contacts
- Update existing contact information
- Delete contacts from the phonebook

### 2. Category Management
- Add new category with its name:
    - Name
- View list of all categories
- Update existing category information
- Delete categories from the phonebook

### 2. Data Persistence
- Contacts are stored in a database mainly mysql server using docker container
- Data persists between application sessions

### 3. User Interface
- Clear and intuitive console-based interface
- Easy-to-navigate menu system
- Input validation for phone numbers and email addresses

## How to Use

### Starting the Application

This section guides you through launching the PhoneBook application:

### Environment Setup

#### Visual Studio
Add the following environment variables in your project settings:
1. `CONNECTION_STRING`: Your SQL Server connection string
2. `SENDGRID_API_KEY`: Your SendGrid API key
3. `ACCOUNT_SID`: Your Twilio Account SID
4. `AUTH_TOKEN`: Your Twilio Auth Token
5. `PHONE_NUMBER`: Your Twilio Phone Number

#### Visual Studio Code
Create or modify `appsettings.Development.json`:
```json
{
    "ConnectionStrings": {
        "DefaultConnection": "YourSqlServerConnectionString"
    },
    "SendGrid": {
        "ApiKey": "YourSendGridApiKey"
    },
    "Twilio": {
        "AccountSid": "YourAccountSid",
        "AuthToken": "YourAuthToken",
        "PhoneNumber": "YourPhoneNumber"
    }
}
```

#### Database Setup
1. Install Entity Framework Core:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
2. Create initial migration:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
### Running the Application
1. Open your terminal or command prompt
2. Navigate to the application directory
3. Run the application using `dotnet run` command
4. Upon successful launch, you'll be presented with the main menu
5. The main menu displays various options for managing your contacts

Note: Ensure you have .NET runtime installed on your system before running the application.

### Managing Contacts

#### Adding a Contact
1. Select "Add Contact" from the main menu
2. Enter contact details when prompted:
     - First Name
     - Last Name
     - Phone Number
     - Email Address
3. Contact will be saved to the database

#### Viewing Contacts
1. Select "View All Contacts" from the main menu
2. List of all saved contacts will be displayed

#### Updating a Contact
1. Select "Update Contact" from the main menu
2. search for the contact
3. Enter new information for the contact
4. Changes will be saved automatically

#### Deleting a Contact
1. Select "Delete Contact" from the main menu
2. search for the contact
3. Confirm deletion when prompted


## Technical Requirements
- .NET Framework
- Sql server database
- Console application environment

## Error Handling
- Input validation for all user inputs
- Appropriate error messages for invalid operations
- Confirmation prompts for critical operations

## Data Validation
- Phone numbers must be in valid format
- Email addresses must be properly formatted
- Names cannot be empty

## Notes
- All operations are performed in real-time
- Data is immediately persisted to the database
- Application provides feedback for all operations
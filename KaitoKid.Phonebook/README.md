# Phonebook Console Application

## Overview
The Phonebook Console App allows users to store contacts with a name, phone number, and email address, as well as edit or delete these contacts. Users can categorize their contacts and send emails or SMS messages to selected contacts directly from the app.

## Features
- **CRUD Operations**: Create, read, update, and delete contacts.
- **Categorization**: Organize contacts into different categories.
- **Email & SMS**: Send emails and SMS messages to specified contacts.

## Setup Instructions

### 1. Configure Environment Variables
Set the following environment variables on your local machine or replace them with the required values:
- `DB_SERVER`: Your SQL Server instance name.
- `DB_NAME`: The name of your database.

### 2. Modify Configuration File :
   ```xml
   <configuration>
       <appSettings>
           <add key = "FlashcardsDBConnection" value = "Data Source=${DB_SERVER};Initial Catalog=${DB_NAME};Integrated Security=True;" />
       </appSettings >
   </configuration >
   ```

### 3. Set up user secrets for following value or replace them with required values:
   - `SENDER_EMAIL`: Required for sending emails
   - `PASSWORD`: Sender email password required for sending emails
   - `Twilio_SID`: Required for sending SMS via Twilio
   - `Twilio_Auth_Token`: Required for sending SMS via Twilio
   - `SENDER_NUMBER`: Required for sending SMS (Twilio Trial Number)
   - `COUNTRY_CODE`: Set up according to specific country's SMS receiver number

   Use the following command to set up user secrets:
   ```
   dotnet user-secrets "SENDER_EMAIL" "your_email_id"
   ```
   
### 4. Database Migration
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
     
## Dependencies
The project uses the following packages:
1. Microsoft.EntityFrameworkCore: It is used to map database with the c# app
2. Microsoft.Extensions.Configuration.UserSecrets: It is used to store sensitive data variable values like email id and password
3. Twilio: It used for sending SMS to specified contact
4. Spectre.Console: It is used for beautification of console App
5. System.Configuration: It is used to get connection string from App.config

## Project Structure
### 1. Models
   Contains Contact class which stores the contact name, phone number and email id

### 2. PhonebookContext:
   Defines the database context using EF Core, and is responsible for managing the Contact model and interacting with the database.

### 3. UserInput
   Handles user input and commands for performing CRUD operations or sending emails/SMS.

### 4. PhonebookService
   Processes user input and coordinates with the repository to perform the desired actions. It also manages email and SMS sending based on user choices.

### 5. PhonebookRepo
   Implements the CRUD operations, interacting with the database using the data provided by the user.

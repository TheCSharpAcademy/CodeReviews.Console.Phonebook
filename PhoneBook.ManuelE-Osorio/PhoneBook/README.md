# PhoneBook
The following application is a simple PhoneBook app where users can store contacts, phone numbers and emails. The app's
design is inspired by mobile application. The purpose of the app was to learn how use basic CRUD operations with Entity Framework connecting
to a SQL Server database. The application uses Google library libphonenumber and Microsoft's MailAddress to validate phone numbers and emails, respectively.

## Usage

### First Time Usage

Before starting the application, the user needs to configure the connection string in the appsettings.json file. Additionally the user can add 
their phone number and email to send SMS and Emails inside the app.

### Main Menu

The main menu shows the user the contacts stored in the database with their owned category. The following options are present in the main menu:

1) Enter to check the details of the selected contact: This option goes to the contact details view.
2) I to create a new contact
3) M to modify the selected contact
4) D to delete the selected contact
5) F to filter by first letter: The user is able to filter the contacts displayed in the screen by typing their starting letter
6) C to filter by category: The user is able to filter the contacts displayed in the screen by typing the desired category.
7) Q to clear the search filters
8) P to import data to the app: This option allows the user to import data to the app via the ContactsImport.csv, EmailsImport.csv and PhoneNumbersImport.csv files included. The import option can be used anytime in the app and will check for duplicates and valid values.
9) Backspace/Esc to exit the application

### Contact Detail

In the contact detail view, the user is able to see all the emails and phone numbers associated with the selected contact. A contact is able to have multiple emails and phone numbers. The view has the following options:

1) Enter to send an SMS/email to the selection: This option allows the user to send an SMS/Email. The user has to configure their own phone number or email in the appsettings.json before using this option.
2) P/E to create a new phone/email to the selected contact
3) M to modify your selection
4) D to delete your selection
5) Backspace/Esc to return: Returns to the Main Menu view.


## To be Done

1) Implement server to send Emails.

## References

1)  https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-8.0
2)  https://dba.stackexchange.com/questions/37014/in-what-data-type-should-i-store-an-email-address-in-database
3)  https://www.mssqltips.com/sqlservertip/2657/storing-email-addresses-more-efficiently-in-sql-server/
4)  https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
5)  https://stackoverflow.com/questions/5354900/entity-framework-code-first-advantages-and-disadvantages-of-fluent-api-vs-data
6)  https://learn.microsoft.com/en-us/ef/core/modeling/
7)  https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/types-and-properties
8)  https://stackoverflow.com/questions/2955459/what-is-an-index-in-sql
9)  https://stackoverflow.com/questions/5663754/entity-framework-4-lifespan-scope-of-context-in-a-winform-application
10) https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
11) https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller
12) https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.navigationbuilder-2.autoinclude?view=efcore-5.0
13) https://regex101.com/
14) https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes
15) https://stackoverflow.com/questions/21733756/best-way-to-split-string-by-last-occurrence-of-character
16) https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/accessing-attributes-by-using-reflection
17) https://learn.microsoft.com/en-us/dotnet/api/system.net.mail?view=net-8.0
18) https://github.com/google/libphonenumber
19) https://github.com/dotnet/runtime/issues/82689  isuue on MailAddress validation
20) https://learn.microsoft.com/en-us/dotnet/api/system.consolekeyinfo.modifiers?view=net-8.0&redirectedfrom=MSDN#System_ConsoleKeyInfo_Modifiers
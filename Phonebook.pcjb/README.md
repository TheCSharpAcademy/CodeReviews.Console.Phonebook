# Phone Book
A very simple Phone Book console app. Exercise by [The C# Academy](https://www.thecsharpacademy.com)

## Database Setup
The application needs an MS SQL Server database to store contacts.
Please configure the database connection string in appsettings.json:
```
{
    "DatabaseConnectionString": "Server=127.0.0.1;Initial Catalog=PhoneBook;User ID={DatabaseUserID};Password={DatabasePassword}"
}
```
### Development Database Secrets
The placeholders '{DatabaseUserID}' and '{DatabasePassword}' should not be modified or replaced with the real values. The app will read these values from the user-secrets provided by dotnet:
```
dotnet user-secrets set DatabaseUserID "YOUR-DATABASE-USERNAME-HERE"
dotnet user-secrets set DatabasePassword "YOUR-DATABASE-PASSWORD-HERE"
```
The app will create databases tables on startup as needed.
Thus the database user must to be allowed to create tables in the database.

## Email Setup
The application needs an SMTP-Server to send emails.
Please configure the SMTP-Server connection in appsettings.json:
```
{
    "Mail": {
        "From": "phonebook@example.com",
        "Host": "127.0.0.1",
        "Port": 1025,
        "Username": "{MailUsername}",
        "Password": "{MailPassword}"
    }
}
```

### Development Email Secrets
The placeholders '{MailUsername}' and '{MailPassword}' should not be modified or replaced with the real values. The app will read these values from the user-secrets provided by dotnet:
```
dotnet user-secrets set MailUsername "YOUR-SMTP-USERNAME-HERE"
dotnet user-secrets set MailPassword "YOUR-SMTP-PASSWORD-HERE"
```

### Development Email Server (MailHog)
For local development, you can quickly set up an email server using [MailHog](https://github.com/mailhog/MailHog) and Docker:
```
docker run -d -p 1025:1025 -p 8025:8025 mailhog/mailhog
```
The local email server will listen for SMPT requests on port 1025 and provide a user interface to check emails on port 8025: http://localhost:8025/

## Notes/References
* [The C# Academy Exercise "Phone Book"](https://thecsharpacademy.com/project/16)
* https://learn.microsoft.com/en-us/ef/core/
* https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
* https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#required-one-to-many



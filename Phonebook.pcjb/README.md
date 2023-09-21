# Phone Book
A very simple Phone Book console app. Exercise by [The C# Academy](https://www.thecsharpacademy.com)

## Database Setup
The application needs an MS SQL Server database to store flashcards, stacks and study sessions.
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

## Notes/References
* [The C# Academy Exercise "Phone Book"](https://thecsharpacademy.com/project/16)




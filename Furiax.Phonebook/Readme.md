<h1>Phonebook</h1>

This application is a part of the www.thecsharpacademy.com roadmap.
Goal of the website is to become a (better) C# and .Net developper.

<h2>About the app:</h2>
The app is a phonebook app where users can store contacts.
Each contact requires a name, a phonenumber and an emailaddress.
Contacts can be assigned to a category (i.e.: Family, Friends, Collegues,...)
There's also an added option for sending an email or a SMS to a contact.

<h2>Requirements:</h2>
- Store contacts with the following fields: id, name and phonenumber.
- The use of Entitiy Framework to perform the CRUD commands.
- Database needs to be created the 'Code First' approach (EF creates the db).
- Use SQL, no SQLite

Extra challenges:
- Also store an emailaddress for each contact
- Add Categories where contacts can be assigned to.
- Categories table should be linked to Contacts table
- Add an option to send Emails
- Add an option to send SMS

<h2>Lessons learned:</h2>
- The use of the basic CRUD commands with Entity Framework
- Creating a database and linking tables with each other via the Code First approach
- First time use of Spectre, a NuGet package that makes selecting and displaying options in a console app a whole lot easier and userfriendly.
- Had to search for a way to send email/SMS from inside a console application.
- First time use of Twilio (to send SMS) and SendGrid (to send emails), both API services.
- Learned about storing EnvironmentVariables so API keys are stored safely.

<h2>Resources used:</h2>
- The assignment page on www.thecsharpacademy.com 
- Microsoft docs on Entity Framework and environment variables
- Various google searches about options to send email/sms and how to store sensitive info secure.
- Documentation for setting up Twilio and SendGrid

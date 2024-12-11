## Phone book app

+ Console CRUD aplication for managing contacts. Created using Entity Framework, C#, SQL.

## Requiraments
+ This is an application where you should record contacts with their phone numbers.
+ Users should be able to Add, Delete, Update and Read from a database, using the console.
+ You need to use Entity Framework, raw SQL isn't allowed.
+ Your code should contain a base Contact class with AT LEAST {Id INT, Name STRING, Email STRING and Phone Number(STRING)}
+ You should validate e-mails and phone numbers and let the user know what formats are expected
+ You should use Code-First Approach, which means EF will create the database schema for you.
+ You should use SQL Server, not SQLite

## Features
+ You can manage your contacts and do CRUD commands
+ You can send emails from the app, if you want to do this: 

	Additionally go to the Google Account > Security page and look at the Signing in to Google > 2-Step Verification setting.
	
	If it is enabled, then you have to generate a password allowing .NET to bypass the 2-Step Verification. To do this, click on Signing in to Google > App passwords, select app = Mail, and device = Windows Computer, and finally generate the password. Use the generated password in the fromPassword constant instead of your standard Gmail password.
    If it is disabled, then you have to turn on Less secure app access, which is not recommended! So better enable the 2-Step verification.

## Challenges
+ Needed some time to understand how to work with EF
+ It was hard to understand how to work with emails

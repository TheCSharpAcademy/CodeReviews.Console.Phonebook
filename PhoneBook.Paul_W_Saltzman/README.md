# Console Phonebook
Console based CRUD application that can send emails or sms.
# Requirements
 - [x] This is an application where you should record contacts with their phone numbers.
 - [x] Users should be able to Add, Delete, Update and Read from a database, using the console.
 - [x] You need to use Entity Framework, raw SQL isn't allowed.
 - [x] Your code should contain a base Contact class with AT LEAST {Id INT, Name STRING, Email STRING and Phone Number(STRING)}
 - [x] You should validate e-mails and phone numbers and let the user know what formats are expected
 - [x] You should use Code-First Approach, which means EF will create the database schema for you.
 - [x] You should use SQL Server, not SQLite


# Optional Challenges
  - [x] Create a functionality that allows users to add the contact's e-mail address and send an e-mail message from the app.
  - [x] What if you want to send not only e-mails but SMS?

# Challanges
  - [x] Dealing with null values presented a challange.  I had to put several catches for null values or empty values.
  - [x] I struggled with indecision about whether to build to a email interface or use sftp database.  I ended up going with sftp.
  - [x] I refactored several times while creating this program.  I'll have to plan out my next program better before I start.  This is not the first time I've thought that.  Perhaps refactoring is just part of the process. 

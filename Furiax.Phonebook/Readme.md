<h1>Phonebook</h1>

This application is a part of the www.thecsharpacademy.com roadmap.<br>
Goal of the website is to become a (better) C# and .Net developper.<br>
<br>
<h2>About the app:</h2>
The app is a phonebook app where users can store contacts.<br>
Each contact requires a name, a phonenumber and an emailaddress.<br>
Contacts can be assigned to a category (i.e.: Family, Friends, Collegues,...)<br>
There's also an added option for sending an email or a SMS to a contact.<br>
<br>
<h2>Requirements:</h2>
* Store contacts with the following fields: id, name and phonenumber.<br>
* The use of Entitiy Framework to perform the CRUD commands.<br>
* Database needs to be created the 'Code First' approach (EF creates the db).<br>
* Use SQL, no SQLite<br>
<br>
Extra challenges:<br>
* Also store an emailaddress for each contact<br>
* Add Categories where contacts can be assigned to.<br>
* Categories table should be linked to Contacts table<br>
* Add an option to send Emails<br>
* Add an option to send SMS<br>
<br>
<h2>Lessons learned:</h2>
* The use of the basic CRUD commands with Entity Framework<br>
* Creating a database and linking tables with each other via the Code First approach<br>
* First time use of Spectre, a NuGet package that makes selecting and displaying options in a console app a whole lot easier and userfriendly.<br>
* Had to search for a way to send email/SMS from inside a console application.<br>
* First time use of Twilio (to send SMS) and SendGrid (to send emails), both API services.<br>
* Learned about storing EnvironmentVariables so API keys are stored safely.<br>
<br>
<h2>Resources used:</h2>
* The assignment page on www.thecsharpacademy.com <br>
* Microsoft docs on Entity Framework and environment variables<br>
* Various google searches about options to send email/sms and how to store sensitive info secure.<br>
* Documentation for setting up Twilio and SendGrid<br>

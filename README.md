****MNP Contact Management Application****

Web based application to store employees and clients contacts

Project was build using ASP.NET MVC and Razor pages, .Net 6.0 and Visual Studio 2022
Contacts are stored in SQLite database, Dapper is used for database operations

Database will be created on the first run with initial contact 
and it is stored in: ...\ContactsApplication-master\ContactsApp\Database\Contacts.db

When running tests, separate testing database is created in: 
...\ContactsApplication-master\ContactsApp.Test\ContactsDBTest.db


Solution has two projects
- ContactsApp - main application
- ContactsApp.Test - controller unit and database access tests


Instructios how to run Contacts Management Application

- From https://github.com/galexyu70/ContactsApplication, click on the dropdown "Code"
- Select "Download ZIP"
- In your Downloads folder you fill find file: ContactsApplication-master.zip
- Uzip project to the location of your choice
- Navigate to Solution file (...\ContactsApplication-master\ContactsApp.sln
- Open Solution in Visual Studio 2022
- in Solution Explorer, right click on Solution, select "Restore Nuget Packages"
- Run application from Visual Studio
- Add New Contact, Click on the table of contacts to Edit Contact
- Run Tests from Test Explorer


# Backend

This application provides the backend services for the Mobile-Rounds mobile application. Here, 
you will find the requirements for running the application under IIS. 

## Technologies Used

- ASP.NET Web API 2
- Entity Framework 6.1.3 (Database Table Declerations)
- Code First Database Migrations
- NuGet Package Manager
- MSTest Test Framework
- Visual Studio 2015 with Update 3
- Windows Authentication (NTLM)

## Project Descriptions

Below you will find descriptions of each project and what their purpose is in the overall solution.

| Project Name | Purpose |
|:------------:|:--------|
| *Backend* | Serves as the REST API service for the mobile application. It provides all the data storage as necessary. |
| *Backend.Schemas* | Defines the model objects that represent a record in a database table. |
| *Backend.DataAccess* | Defines a set of base classes, repositories and interfaces for accessing database objects. All operations are done through `ViewModels` |
| *Backend.Diagrams* | A set of UML diagrams as well as layer diagrams outlining the connection between services. |
| *Mobile-Rounds.ViewModels*| A set of view model objects that are shared between the *Backend* and *Tablet* applications. |
| *Backend.Tests* | The test library for all backend based binaries. This will encompase the DataAccess library as well as the Schemas library if necessary. |
| *Mobile-Rounds.ViewModels.Tests* | A suite of tests for the *Mobile-Rounds.ViewModels* library. |

## Development

To begin development, you must first create the developer database. To do this, open up 
`Tools -> NuGet Package Manager -> Package Manager Console`. Then, from the drop down titled `Default project`
select the project named `Backend.Schemas`. Then in the console run the command: `Update-Database`. This will
create all database tables and any seed data.

At this point you should then be able to run the server like any application using F5.

## Caveats and configuration

### Windows Authentication

Windows Authentication is the process in which a API service can authenticate based on the currently logged in users
credentials. Since our users will all be on the same domain, we can use this mechanism to secure our *Backend* API. This 
allows us to authenticate all API requests against Active Directory Groups. This is key when it comes to the Admin
based functionality of the application.

This is extremely tricky to test outside an actual network, however Tyler has virtual machines and his tablet configured
for such a situation.

Since using Windows Authentication, the *Backend* application must be installed and running on a computer
that is on the same domain as the client devices. This also requires that the application be running under
an account that is on the domain. If it isn't on the domain, the application will not pass the currently
logged in users credentials to the Web API from the mobile client. This is important as the API
will send back a `401: Unauthorized` response.

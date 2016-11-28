
### Mobile Rounds

#### Status

[![Build status](https://ci.appveyor.com/api/projects/status/w27f664khvcfblga/branch/master?svg=true&passingText=master%20-%20OK&pendingText=master%20build%20in%20process&failingText=master%20failing)](https://ci.appveyor.com/project/tvand7093/mobile-rounds/branch/master)
[![Build status](https://ci.appveyor.com/api/projects/status/w27f664khvcfblga/branch/develop?svg=true&passingText=develop%20-%20OK&pendingText=develop%20build%20in%20process&failingText=develop%20failing)](https://ci.appveyor.com/project/tvand7093/mobile-rounds/branch/develop)

#### Description
A Windows 10 applicaiton and ASP.NET Web API 2 backend for the CS420 Capstone Project.

### System Requirements

#### Development Environment

The following list outlines the requirements for developing both the mobile and API applications: 

 - Visual Studio 2015 Update 3
 - Windows 10 SDK (minimum version: 10.0.10586.0)
 - ASP.NET Web API development tools (should be installed with Visual Studio)

#### Device Environment

The following outlines the minimum requirements to run the Windows Mobile application on a physical device:

 - Windows 10 Build 10586 or greater
 - Tablet must be registered on the Active Directory domain
 - User must be signed in with an Active Directory account
 
The following outlines the requirments for running the API Backend server on premise:

 - Server must be logged into the same domain as the tablet application 
 - All app settings must be configured (TBD)
 - Application must run on an IIS instance that matches the Mobile applications configuration
 - SQL Server connection must be specified in IIS
 - API must run under an Active Directory account.

### Contributers
 - Tyler Vanderhoef
 - Matt Burris
 - Wyatt McGehee
 - Chris Willette



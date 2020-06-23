# Person App
A basic people management application, web api and db, made using Razor .net core and SQL Server 

<h1>Setup</h1>

<h2>Database</h2>
The database "personDb" in the root of this file directory is a SQL Server Database backup, created on SQLSERVEREXPRESS.

This DB can be restored from the backup in Sql Server Management Studio.

<h2>Web API</h2>

Open the PersonApplicatoin solution in Visual Studio.

Make sure to update the Web.config file's "connectionStrings" to set the DefaultConnection to point to where the personDb backup is hosted as currently it is set to 
<i>"Server=localhost\SQLEXPRESS;Database=person_db;User Id=Test;Password=Test;"</i>

The PersonWebApi should then be built, published, and hosted on IIS as a website.

<h2>Web Application</h2>

Open the PersonApplicatoin solution in Visual Studio.

In Startup.cs make sure to set the <i>BaseAddress</i> to the address of the web api, with the correct port number as currently it is set to <i>http://localhost:45/</i>

The PersonWebApp can be published and hosted on IIS.


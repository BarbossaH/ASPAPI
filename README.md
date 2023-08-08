# ASPAPI

# dependency injection

1. we will inject services which are the logic dealing with the response, map data between the DTOs and the original data model, save data into the database, etc.
2. notice the life cycle of dependency injection. I am not so sure how to use that in all kinds of scenarios.

# response

including DTOs and other information, such as "success, error" and other necessary and custom data

# MAP-auto mapper

to separate the original from transferred data, using MAP can build the relationship between them, and also it can add some new data into DTOs,
which won't influence the original data.

# save data into database

Notice the update and create are different. For update, I just need to map the DTOs to the original data model, and then save it into the database.

# Serilog.AspNetCore and Serilog.Sinks.File

CLI: dotnet add package Serilog.AspNetCore --version 7.0.0
CLI: dotnet add package Serilog.Sinks.File --version 5.0.1-dev-00947
Serilog.AspNetCore is for the showing the log in the console. And the Serilog.Sinks.File is for saving the log into the file.

setting in appsettings.json: "Logpath": "/Users/huangbo/Projects/ASPAPI/Logs/API_Log.txt",
;

# CORs

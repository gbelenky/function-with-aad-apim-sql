# Create an identity-protected API implementation with Azure Functions, Azure SQL and API Management 

![](docs/media/2022-05-02-17-13-20.png)

This step-by-step tutorial will implement architecture above and contain the follwing steps:

- Create Azure Functions in VS Code
- Create SQL Database and add SQL Bindings to the Functions. This repo enhances [this sample](https://docs.microsoft.com/en-us/samples/azure-samples/azure-sql-binding-func-dotnet-todo/todo-backend-dotnet-azure-sql-bindings-azure-functions/)
- Deploy Azure Function into Azure
- Protect access to Azure SQL through AAD while using Functions. [Based on this ](https://docs.microsoft.com/en-us/azure/azure-functions/functions-identity-access-azure-sql-with-managed-identity) and [this tutorial](https://docs.microsoft.com/en-us/azure/azure-functions/functions-identity-access-azure-sql-with-managed-identity)
- Expose API through the API mangement
- Protect Back-End API through AAD so that only this APIM instance can access the back-end API
- Protect exposed API through APIM API Keys for the external developers
- Test access to the exposed API through the developer portal 

Audience: teams exploring new generation cloud services implementation concepts

Reason for this tutorial: focus more on the complete business solution rather than on separate services

## Scenario description

Contoso ToDo Product team has already migrated a couple of their applications into Azure App Service. These applications are using custom implemented Authentication and Authorization. They have heard about Microsoft Zero Trust concept and would like to protect their externally exposed APIs, API back-end and the Azure SQL data.

The team would like to increase developer velocity while using serverless technologies such as Azure Functions

## - Create Azure Functions in VS Code

Please refer to this [existing tutorial](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp) and create the following Functions:

* AddToDo
* DeleteToDo
* GetToDo
* GetToDos
* UpdateToDo

All these Functions will be using HTTP Trigger and also different HTTP Methods

The test samples for them are here:
[sample](test.http)
![](docs/media/2022-05-11-16-34-46.png)

The result is in the [branch](https://github.com/gbelenky/function-with-aad-apim-sql/tree/Step-1--Create-Functions) of this repo


Install [HTTP REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client), start the Function and run the tests on your local machine by using the [sample requests](test.http)

## Create the SQL Database and tables

Use this [quickstart](https://docs.microsoft.com/en-us/azure/azure-sql/database/single-database-create-quickstart) to create your database - do not add sample data to your database as it is descibed in the tutorial

Select the Query Editor in your newly created database and execute [this script](/sql/create.sql) there:

![](docs/media/2022-05-11-18-19-38.png)

Add .NET packages for Azure Functions SQL binding as [referred here](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.Sql)

Open VSCode terminal and execute there 

```
dotnet add package Microsoft.Azure.WebJobs.Extensions.Sql --version 0.1.311-preview

```

Start adding bindings to the code - start with [AddToDo.cs](AddToDo.cs)

![](docs/media/2022-05-11-18-35-33.png)

You also need to update your local.settings.json file configuring the SQLConnectionString
![](docs/media/2022-05-11-18-38-46.png)

Replace your code by the content here [AddToDo.cs](), run the Function project and call the function from the [test.http](test.http) and observe the results
![](docs/media/2022-05-11-18-49-08.png)

Continue with the [GetToDo.cs](GetToDo.cs), [GetToDos.cs](GetToDos.cs), [DeleteToDo.cs](DeleteToDo.cs) and [UpdateToDo.cs](UpdateToDo.cs)

Test the rest of the functions with  [test.http](test.http)

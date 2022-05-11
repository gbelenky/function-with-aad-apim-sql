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

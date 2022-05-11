# Create an identity-protected API implementation with Azure Functions, Azure SQL and API Management 

![](docs/media/2022-05-02-17-13-20.png)

This step-by-step tutorial will implement architecture above and contain the follwing steps:

- Create and protect Azure SQL through AAD. [Based on this tutorial](https://docs.microsoft.com/en-us/azure/azure-functions/functions-identity-access-azure-sql-with-managed-identity)
- Create Azure Function in VS Code accessing Azure SQL Database. [This repo enhances this sample](https://docs.microsoft.com/en-us/samples/azure-samples/azure-sql-binding-func-dotnet-todo/todo-backend-dotnet-azure-sql-bindings-azure-functions/)
- Deploy Azure Function into Azure
- Create and protect Azure SQL through AAD. [Based on this tutorial](https://docs.microsoft.com/en-us/azure/azure-functions/functions-identity-access-azure-sql-with-managed-identity)
- Expose API through the APIM Developer portal
- Protect API through APIM API Keys
- Protect Back-End API through AAD
- Limit access to the backend API only to the APIM instance through AAD

Audience: teams exploring new generation cloud services implementation concepts 
Reason for the tutorial: focus rather on the business solution than on separate services

## Scenario description

Contoso Product Rating team has already migrated a couple of their applications into Azure App Service. These applications are using custom implemented Authentication and Authorization. They have heard about Microsoft Zero Trust concept and would like to protect their API
However, the team would like to increase deve using   

# Create an identity-protected API implementation with Azure Functions, Azure SQL and API Management 

![](docs/media/2022-05-02-17-13-20.png)

This step-by-step tutorial will implement architecture above and contain the follwing steps:

- Create Azure Functions in VS Code
- Create SQL Database and add SQL Bindings to the Functions. This repo enhances [this sample](https://docs.microsoft.com/en-us/samples/azure-samples/azure-sql-binding-func-dotnet-todo/todo-backend-dotnet-azure-sql-bindings-azure-functions/)
- Deploy Azure Function into Azure
- Expose the API through the API management
- Protect Back-End API through AAD so that only this APIM instance can access the back-end API
- Protect exposed API through APIM API Keys for the external developers
- Test access to the exposed API through the developer portal
- Optional. Protect access to Azure SQL through AAD while using Functions. [Based on this ](https://docs.microsoft.com/en-us/azure/azure-functions/functions-identity-access-azure-sql-with-managed-identity) and [this tutorial](https://docs.microsoft.com/en-us/azure/azure-functions/functions-identity-access-azure-sql-with-managed-identity) 

Audience: teams exploring new generation cloud services implementation concepts

Reason for this tutorial: focus more on the complete business solution rather than on separate services

## Scenario description

Contoso ToDo Product team has already migrated a couple of their applications into Azure App Service. These applications are using custom implemented Authentication and Authorization. They have heard about Microsoft Zero Trust concept and would like to protect their externally exposed APIs, API back-end and the Azure SQL data.

The team would like to increase developer velocity while using serverless technologies such as Azure Functions

## Create Azure Functions in VS Code

Please refer to this [existing tutorial](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp) and create the following Functions:

* AddToDo
* DeleteToDo
* GetToDo
* GetToDos
* UpdateToDo

All these Functions will be using HTTP Trigger and also different HTTP Methods

Replace the generated Functions code with the content in the [branch](https://github.com/gbelenky/function-with-aad-apim-sql/tree/Step-1--Create-Functions) of this repo

Install [HTTP REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client), start the Function and run the tests on your local machine by using the [sample requests](test.http)

The test samples for them are here:
[sample](test.http)
![](docs/media/2022-05-11-16-34-46.png)

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

Replace your code by the content here [AddToDo.cs](AddToDo.cs), run the Function project and call the function from the [test.http](test.http) and observe the results
![](docs/media/2022-05-11-18-49-08.png)

Continue with the [GetToDo.cs](GetToDo.cs), [GetToDos.cs](GetToDos.cs), [DeleteToDo.cs](DeleteToDo.cs) and [UpdateToDo.cs](UpdateToDo.cs)

Test the rest of the functions with  [test.http](test.http)

## Deploy Azure Functions
Please refer [to this document](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp#enable-publishing-with-advanced-create-options) for the detailed deployment information 

![](docs/media/2022-05-12-13-18-50.png)

![](docs/media/2022-05-12-13-19-24.png)

![](docs/media/2022-05-12-13-20-12.png)

![](docs/media/2022-05-12-13-20-35.png)

![](docs/media/2022-05-12-13-21-05.png)

![](docs/media/2022-05-12-13-21-43.png)

![](docs/media/2022-05-12-13-22-17.png)

![](docs/media/2022-05-12-13-22-36.png)

![](docs/media/2022-05-12-13-22-56.png)

![](docs/media/2022-05-12-13-23-27.png)

![](docs/media/2022-05-12-13-23-50.png)

![](docs/media/2022-05-12-13-24-12.png)



![](docs/media/2022-05-12-13-24-46.png)

Upload local settings:

![](docs/media/2022-05-12-13-29-25.png)

Verify the deployment in the Azure Portal

![](docs/media/2022-05-12-13-31-02.png)

Verify your local settings were published. In my case I had to add the SQLConnectionString manually 

![](docs/media/2022-05-12-13-34-22.png)

Test your deployed Function

![](docs/media/2022-05-12-13-37-02.png)

![](docs/media/2022-05-12-13-37-45.png)

![](docs/media/2022-05-12-13-39-03.png)

![](docs/media/2022-05-12-13-38-44.png)

You can test all your functions from the Azure Portal or add corresponding requests to the test.http file. You can find the Function URL here:

![](docs/media/2022-05-12-13-42-27.png)

In my case it is https://function-with-aad-apim-sql.azurewebsites.net/api/GetToDos

## Expose the API through the API management

Create a new API management instance:

![](docs/media/2022-05-12-13-55-51.png)

![](docs/media/2022-05-12-13-56-25.png)

![](docs/media/2022-05-12-13-57-55.png)

Create Managed Identity for your API Management
![](docs/media/2022-05-12-13-59-03.png)

Optionally add Application Insights to your API Management
![](docs/media/2022-05-12-14-01-10.png)

Review and Create your API Management. This will take some minutes to be completed.

Select the APIM resource you jut creted, go to the APIs blade and select "Create from Azure Resource" - "Function App"

![](docs/media/2022-05-12-15-17-37.png)

Search and select your Function App

![](docs/media/2022-05-12-15-20-06.png)

Select and import all Functions from your Function App

![](docs/media/2022-05-12-15-20-53.png)

Click "Create"

![](docs/media/2022-05-12-15-21-32.png)

After the import you will land in the Design Blade of the APIM

![](docs/media/2022-05-12-15-22-52.png)

Test access to your backend API from the APIM

![](docs/media/2022-05-12-15-32-32.png)

![](docs/media/2022-05-12-15-33-09.png)

As you can see here, your API is already protected by the automatically generated key:

![](docs/media/2022-05-12-15-36-33.png)

![](docs/media/2022-05-12-15-37-20.png)

Now, we need to protect the backend API so that only APIM is able to access it.

### Step 1. Configure authentication for your Functions App

Go to your Function App and select the Authentication blade:

![](docs/media/2022-05-12-15-56-03.png)

Select Microsoft

![](docs/media/2022-05-12-15-56-34.png)

Click Add

![](docs/media/2022-05-12-15-58-26.png)

Note App (client) Id

Try to access your back-end now:

![](docs/media/2022-05-12-18-36-55.png)

and this is the result

![](docs/media/2022-05-12-18-37-34.png)

when testing the Function from APIM you will get the same result:

![](docs/media/2022-05-12-18-39-07.png)

there is a different picture when testing in the browser:

![](docs/media/2022-05-12-18-42-16.png)

![](docs/media/2022-05-12-18-43-03.png)

This means that we limited access to the back-end API only to the indentities inside of the Azure AD tenant where we created identity of the Azure Function (also called Managed Identity describing the Service Principal which was also instantiated in the same AAD Tenant). But still, all identities can access our Function - we need to change it


### Step 2. Allow APIM to access the Function

Before we limit access to the backend, we need to allow APIM to access our Function. [The following policy will help us](https://docs.microsoft.com/en-us/azure/api-management/api-management-authentication-policies#use-managed-identity-to-authenticate-with-a-backend-service) to the APIM

```
<authentication-managed-identity resource="Client_id_of_Backend"/>

```

If you did not note the Client Id of your Function, you need to revisit the Authentication blade of the Function:

![](docs/media/2022-05-12-18-54-46.png)

Jump back to APIM and select your API:

Click Inbound Processing -> Policies

![](docs/media/2022-05-12-18-57-00.png)

Add the following line and replace the resource with your Azure Function Client Id:

![](docs/media/2022-05-12-19-02-10.png)

Save it and test your API again:

Our request was successful:

![](docs/media/2022-05-12-19-04-52.png)

### Step 3. Allow ONLY APIM to access the Function

If you're unfamiliar with managed identities for Azure resources, check out the [overview section](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview). 

Be sure to review the [difference between a system-assigned and user-assigned managed identity](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types). 

Here you can find more information on [Service Principals and Applications](https://docs.microsoft.com/en-us/azure/active-directory/develop/app-objects-and-service-principals)

To limit access to APIM only you need to 
- Create an App Role for your Function in its AAD registration (this will be correlated with the Service Principal of your AAD App Registration). App Role defines a group of users/applications anabled to access this application and receive receive this App Role as an authorization claim (outside of scope here. More [here](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-add-app-roles-in-azure-ad-apps#usage-scenario-of-app-roles)) 
- Add the APIM Managed Identity Object ID to the App Role
- [Limit access to the Function only to the users/roles in the App Role](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-restrict-your-app-to-a-set-of-users#update-the-app-to-require-user-assignment) 

A similar approach can be achieved by using [Security Groups](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-add-app-roles-in-azure-ad-apps#app-roles-vs-groups), but might evolve IT personnel managing AAD Security Groups. Here, we will work through the App Roles.

Go to the application registrations:

![](docs/media/2022-05-12-19-27-45.png)

select your function:

![](docs/media/2022-05-12-19-28-35.png)

Create a new App Role:

![](docs/media/2022-05-12-19-31-16.png)

Currently you cannot assign your APIM Application identity to the app role through the Azure Portal. You will need to go through the [Azure CLI](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/how-to-assign-app-role-managed-identity-cli) or [Powershell](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/how-to-assign-app-role-managed-identity-powershell).


Use the Bash environment in [Azure Cloud Shell](https://docs.microsoft.com/en-us/azure/cloud-shell/quickstart). For more information, see [Azure Cloud Shell Quickstart - Bash](https://docs.microsoft.com/en-us/azure/cloud-shell/quickstart).

[Launch Cloud Shell in a new window](https://shell.azure.com/)

This is the commandlet you will need to execute:
```
# Assign the managed identity access to the app role.
New-AzureADServiceAppRoleAssignment -ObjectId $APIMmanagedIdentityObjectId -Id $appRoleId -PrincipalId $APIMmanagedIdentityObjectId -ResourceId $functionServicePrincipalObjectId

```
functionServicePrincipalObjectId : 

![](docs/media/2022-05-13-18-33-07.png)


Go to the AAD Enterprise Applications

![](docs/media/2022-05-13-18-34-36.png)

Copy the Object Id
![](docs/media/2022-05-13-18-35-46.png)

appRoleId : 

Go to AAD App registrations, select your registration and the App Role. Copy the App Role Id

![](docs/media/2022-05-13-18-38-26.png)

APIMmanagedIdentityObjectId:

Go to APIM - search for managed identity and copy the Object Principal id :

![](docs/media/2022-05-13-18-40-10.png)

Set these values in Powershell and execute the commandlet:

```
# Enterprise applications 
$functionServicePrincipalObjectId = '15b**********'
# App Role Id
$appRoleId = '78b**********'
# APIM Managed Identity
$APIMmanagedIdentityObjectId = '891******'

# Assign the managed identity access to the app role.
New-AzureADServiceAppRoleAssignment -ObjectId $APIMmanagedIdentityObjectId -Id $appRoleId -PrincipalId $APIMmanagedIdentityObjectId -ResourceId $functionServicePrincipalObjectId

```

And this is [the last step](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-restrict-your-app-to-a-set-of-users#update-the-app-to-require-user-assignment)

![](docs/media/2022-05-13-18-45-03.png)

Verify that you have no access to the function directly 

![](docs/media/2022-05-13-18-47-53.png)

And that you have access through the APIM:

![](docs/media/2022-05-13-18-49-11.png)







---
services: government
platforms: aspnetcore2.1
author: yujhong
---

# Azure Government Functions Sample
### Description 
This sample consists of an Azure Functions application as well as a Asp.net Core Web application.
The Web application is an Image Moderator application that allows one to moderate images that are being uploaded to a Cosmos database by users. 
When a user uploads an image it is stored in a Blob container and a document referencing the image with the status "pending" is written to the Cosmos database.
A message with the Image Id is written to a Queue, which triggers the Function app which watches for the image to be uploaded.
The function sends the image to the Computer vision API to see if the image is of a Car, and returns back a status of "Approved" or "Rejected". 

# How To Run This Sample
Getting started is simple!  To run this sample in Azure Government you will need:

- An Azure Active Directory (Azure AD) tenant in Azure Government. You must have an [Azure Government subscription](https://azure.microsoft.com/overview/clouds/government/request/) in order to have an AAD tenant in Azure Government. For more information on how to get an Azure AD tenant, please see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/) 
- A user account in your Azure AD tenant. This sample will not work with a Microsoft account, so if you signed in to the Azure Government portal with a Microsoft account and have never created a user account in your directory before, you need to do that now.


To run locally you will additionally need:
- Install [.NET Core](https://www.microsoft.com/net/core) 2.1.0 or later.
- Install [Visual Studio](https://www.visualstudio.com/vs/) 2017 version 15.3 or later with the following workloads:
    - **ASP.NET and web development**
    - **.NET Core cross-platform development**

### Setup
#### Step 1: Register the sample with your Azure Active Directory tenant

1. Sign in to the [Azure Government portal](https://portal.azure.us).
2. On the top bar, click on your account and under the **Directory** list, choose the Active Directory tenant where you wish to register your application.
3. Click on **More Services** in the left hand nav, and choose **Azure Active Directory**.
4. Click on **App registrations** and choose **Add**.
5. Enter a friendly name for the application, for example 'Inventory App' and select 'Web Application and/or Web API' as the Application Type. For the sign-on URL, enter a temporary placeholder - for example, `https://mywebapp/signin-oidc`. 

    >[!Note] 
    > We will change this URL later after creating the web application and deploying to Azure Government.
    >
    >

    Click on **Create** to create the application.
6. While still in the Azure portal, choose your application, click on **Settings** and choose **Properties**.
7. Find the Application ID value and copy it to the clipboard.
8. Find and save your Azure AD Domain name found at the top of the Overview Page under **Azure Active Directory**.

#### Step 2: Deploy Resources to Azure Government

After clicking on the "Deploy to Azure Gov" button below, you will be prompted with a ARM deployment template in the portal. Fill in the values for your AAD client id and domain name with the values saved in step 7 and 8 in the previous section. Enter the name of your choice for the App plan name parameter, and click create. 

<a href="https://portal.azure.us/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fyujhongmicrosoft%2Fgov-function-sample%2Fmaster%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/AzureGov.png" />
</a> 

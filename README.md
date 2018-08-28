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

To run locally you will additionally need:
- Install [.NET Core](https://www.microsoft.com/net/core) 2.1.0 or later.
- Install [Visual Studio](https://www.visualstudio.com/vs/) 2017 version 15.3 or later with the following workloads:
    - **ASP.NET and web development**
    - **.NET Core cross-platform development**

### Setup

#### Step 1: Deploy Resources to Azure Government

After clicking on the "Deploy to Azure Gov" button below, you will be prompted with a ARM deployment template in the portal.  Enter the name of your choice for the App plan name parameter, and click create. 

1. Web app 
2. Storage Queue
3. Cosmos database
4. Blob storage 
5. Cognitive services account
<a href="https://portal.azure.us/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fyujhongmicrosoft%2Fgov-function-sample%2Fmaster%2Fazuredeploy.json" target="_blank">
    <img src="http://azuredeploy.net/AzureGov.png" />
</a> 

#### Step 2: Set connection strings in Function App


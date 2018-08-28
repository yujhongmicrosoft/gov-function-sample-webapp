# Azure Government Functions Sample
### Description 
This sample consists of an Azure Functions application as well as a Asp.net Core Web application.
The Web application is an Image Moderator application that allows one to moderate images that are being uploaded to a Cosmos database by users. 
When a user uploads an image it is stored in a Blob container and a document referencing the image with the status "pending" is written to the Cosmos database.
A message with the Image Id is written to a Queue, which triggers the Function app which watches for the image to be uploaded.
The function sends the image to the Computer vision API to see if the image is of a Car, and returns back a status of "Approved" or "Rejected". 

### Setup
1. Click on the Deploy to Azure Gov button below to deploy the needed resources.
2. Cognitive services account?

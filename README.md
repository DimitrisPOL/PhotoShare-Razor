# PhotoShare

PhotoShare is a project that allows the public and private sharing of photographs from photographers to users. It stores pictures using Azure Blob Storage. The pictures are uploaded from profesional photographers that are registered on the website using an admin control panel and they are publically made available via the front page. The front page lazy loads a number of pictures upon visiting. By scrolling the front page new pictures are being appended for the user to see. The pictures are linked to a location and the users may search for pictures attributed to the location using a search box that is enhanced with auto-completion and auto-correction features. There is also a photographers page for the user to see the photographers that are registered on the site and are enabled for view by the administrator. 

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or later.
- [Azure Storage](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal) account with [Access key](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-keys-manage?tabs=azure-portal)

## Features

- Store pictures using Azure Blob Storage in an admin control panel.
- Creating location entities with which the pictures are grouped by. Each location holds each own container in blob storeage. This enables users to search pictures by location.
- Progressively view published pictures in the front page via an Ajax appending mechanism (new images are fethched and added as user scorll).
- Auto-completion and auto-correction with suggested searches dynamically as the user types.
- Register new professional photographers that add new pictures.
- Display registered professional for the users in 'Our Photographers'. Only the photographers that admin has enabled visibility are shown.
- JQuery Slideshow that slides pictures every three seconds. Also the user can slide on their own using navigation arrow buttons.

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/DimitrisPOL/PhotoShare.git
cd PhotoShare

```

## Setting Up the Project
- Install Required Packages
- Configure Database
  -  Set you DBs connection string in appsettings.json
```bash
  "ConnectionStrings": {
    "DefaultConnection": "Server=*********L2;Database=PhotoShare;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }

```
  -  From Nuget Package Manager Consele choose Photoshare.Infrastructure project and enable migrations and update database

```bash
PM> Enable-Migrations

PM> Update-Database

```

![image](https://github.com/user-attachments/assets/a56cd814-989f-4b85-8747-314578fb3186)

  - Create an [Azure Storage](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal) and enbale an  [Access key](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-keys-manage?tabs=azure-portal)
  - In Azure Portal Panel navigate to Storage Accounts > {Storage you created} > Access Keys
  -  Fill the appropriate data on appsettings.json . Fill the name of the storage account in AzureSotrageAccountUrl ending in .blob.core.windows.net and in ConnectionString. Fill in the AccountKey. 
> [!CAUTION]
> Never share this key or have it exposed in a repository. Someone abusing this key could result in Microsoft charging your account.

 ```bash
      "BlobStorageSettings": {
        "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=*************;AccountKey=***********************;EndpointSuffix=core.windows.net",
        "AzureSotrageAccountUrl": "*********.blob.core.windows.net",
        "AzureHostUrl": "127.0.0.1",
        "Scheme": "https"
      }
  
  ```

## Sample photos

![Screenshot 2024-08-18 at 14-37-41 Home page - Photo](https://github.com/user-attachments/assets/0da43dcb-16f4-4a6f-b0d6-4ce4d037bb32)



![Screenshot 2024-08-18 at 14-48-44 - Photo](https://github.com/user-attachments/assets/1fe6ad5f-3405-4428-9665-a99817d51459)



![Screenshot 2024-08-18 at 14-49-12 Locations - Photo](https://github.com/user-attachments/assets/557786f6-72e5-4f0f-b190-554a47c14f78)


![Screenshot 2024-08-18 at 14-50-25 - Photo](https://github.com/user-attachments/assets/13807e0e-85ff-4dac-939e-c44f8a590174)


![Screenshot 2024-08-18 at 14-51-00 Register - Photo](https://github.com/user-attachments/assets/fdbe6421-27d5-4f88-b769-4a55db98914c)

![image](https://github.com/user-attachments/assets/5e8a71a4-a80f-4725-aa60-35866c3cd403)



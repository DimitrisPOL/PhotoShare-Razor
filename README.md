# PhotoShare

PhotoShare is a project that facilitates both public and private sharing of photographs from photographers to users. It stores images using Azure Blob Storage. Professional photographers, registered on the website, upload pictures through an admin control panel, making them publicly available on the front page. The front page implements lazy loading, initially displaying a limited number of pictures when visited. As the user scrolls, more images are progressively appended for viewing. Each picture is linked to a location, and users can search for images associated with a specific location through a search box enhanced with auto-completion and auto-correction features. Additionally, there is a photographers page where users can view photographers registered on the site and approved for visibility by the administrator.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or later.
- [Azure Storage](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal) account with [Access key](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-keys-manage?tabs=azure-portal)

## Features

- Store pictures using Azure Blob Storage in an admin control panel.
- Create location entities that group the pictures. Each location has its own container in blob storage, allowing users to search for pictures by location.
- Progressively view published pictures on the front page through an AJAX appending mechanism (new images are fetched and added as the user scrolls).
- Auto-completion and auto-correction with suggested searches that dynamically appear as the user types.
- Register new professional photographers who can add new pictures.
- Display registered professionals for users on the "Our Photographers" page. Only photographers whose visibility has been enabled by the admin are shown.
- JQuery slideshow that automatically transitions between pictures every three seconds. Users can also manually navigate using arrow buttons.

## Future Features

-  Private sharing of pictures from photographer to simple user

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/DimitrisPOL/PhotoShare.git
cd PhotoShare

```

## Setting Up the Project
1.  Install Required Packages
2.  Configure Database
  -  Set you database  connection string in 'appsettings.json'
```bash
  "ConnectionStrings": {
    "DefaultConnection": "Server=*********L2;Database=PhotoShare;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }

```
3. Enable Migrations and Update the Database
  -  From the NuGet Package Manager Console, choose the PhotoShare.Infrastructure project and run the following commands:
 ```bash
PM> Enable-Migrations

PM> Update-Database

```

![image](https://github.com/user-attachments/assets/a56cd814-989f-4b85-8747-314578fb3186)

4.  Create an [Azure Storage](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal) Account and enable an [Access key](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-keys-manage?tabs=azure-portal)
  -  In the Azure Portal, navigate to: Storage Accounts > [Your Storage Account] > Access Keys
  -  Enter the appropriate details in appsettings.json. Provide the storage account name in AzureStorageAccountUrl, ending in .blob.core.windows.net, and add the ConnectionString and AccountKey.
```bash
      "BlobStorageSettings": {
        "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=*************;AccountKey=***********************;EndpointSuffix=core.windows.net",
        "AzureSotrageAccountUrl": "*********.blob.core.windows.net",
        "AzureHostUrl": "127.0.0.1",
        "Scheme": "https"
      }
  
```
> [!CAUTION]
> Never share this key or have it exposed in a repository. Someone abusing this key could result in Microsoft charging your account.



## Sample photos

![Screenshot 2024-08-18 at 14-37-41 Home page - Photo](https://github.com/user-attachments/assets/0da43dcb-16f4-4a6f-b0d6-4ce4d037bb32)



![Screenshot 2024-08-18 at 14-48-44 - Photo](https://github.com/user-attachments/assets/1fe6ad5f-3405-4428-9665-a99817d51459)



![Screenshot 2024-08-18 at 14-49-12 Locations - Photo](https://github.com/user-attachments/assets/557786f6-72e5-4f0f-b190-554a47c14f78)


![Screenshot 2024-08-18 at 14-50-25 - Photo](https://github.com/user-attachments/assets/13807e0e-85ff-4dac-939e-c44f8a590174)


![Screenshot 2024-08-18 at 14-51-00 Register - Photo](https://github.com/user-attachments/assets/fdbe6421-27d5-4f88-b769-4a55db98914c)

![image](https://github.com/user-attachments/assets/5e8a71a4-a80f-4725-aa60-35866c3cd403)



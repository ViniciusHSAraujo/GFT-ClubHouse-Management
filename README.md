# GFT- Club House Management

Management system for event houses developed during GFT internship training.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

.Net Core SDK 2.2

Entity Framework Core Tools 2.2

MySQL 8
### Installing

 - Clone the project.

 - Restore packages:

```
dotnet restore
```


 - Change the "AppSettings.json" to your database reference:

```
"ConnectionStrings": {
    "DefaultConnection": "Server=SERVER_IP_OR_HOST;Database=DATABASE_NAME;Uid=USERNAME;Pwd=PASSWORD;"
  }
```

 - Update the database:

```
dotnet ef database update
```

 - After installation, when accessing the project for the first time, access the administrative area with the following credentials:
    - Username: admin@admin.com;
    - Password: DefaultPassword;

## Themes used

[DirectoryAds](https://colorlib.com/wp/template/directoryads/)

[Lumino: Free Bootstrap Admin Template](https://medialoot.com/item/lumino-admin-bootstrap-template/)

## Authors

* **Vinícius Araújo** - *Initial work* - [Linkedin](https://linkedin.com/in/ViniciusHSAraujo)

See also the list of [contributors](https://github.com/ViniciusHSAraujo/GFT-ClubHouse-Management/contributors) who participated in this project.

## License

This project is licensed under the MIT License.

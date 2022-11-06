# Parks Lookup API

#### By _Kelly Bruce_

#### _An API to access information about national and state parks._

## Technologies Used

* _C#/.NET_
* _SQL Workbench_
* _MVC_
* _Entity Framework_
* _Identity_
* _JwtBearer_

## Description

This API is used to access information about national and state parks.

## Setup/Installation Requirements

* Run the command

    ``git clone https://github.com/kelly-camille/Parks.git``

* Run the command

    ``cd ParksLookupSolution``

* The file structure should look like this:
    <pre>ParksLookup.Solution
    ├── .gitignore 
    ├── ... 
    └── ParksLookup
        ├── Controllers
        ├── Models
        ├── ...
        ├── README.md
        └── Startup.cs</pre>

* Add a file named appsettings.json

    <pre>ParksLookup.Solution
    ├── .gitignore 
    ├── ... 
    └── ParksLookup
        ├── Controllers
        ├── Models
        ├── ...
        ├── Startup.cs
        └──appsettings.json
      
* The code in appsettings.json. should look like this:

```json
{
    },
  "JwtConfig": {
    "Secret" : "[YOUR-SECRET-HERE]"
  }
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=parks_lookup;uid=root;pwd=[YOUR-PASSWORD-HERE];"
}

```

* Replace [YOUR-PASSWORD-HERE] with your MySQL password.

* Replace [YOUR-SECRET-HERE] with your random length 32 string.

* Run the command

    ```dotnet ef database update```

Navigate to the following directory in the console
    <pre>ParksLookup.Solution
    └── <strong>ParksLookup</strong></pre>

Run the following command in the console

  ``dotnet build``

Then run the following command in the console

  ``dotnet run``

## API Documentation
Using postman:

#### HTTP Request Structure
```
GET /api/parks
POST /api/Parks
GET /api/Parks/{id}
PUT /api/Parks/{id}
DELETE /api/Parks/{id}
```
* To utilize the POST request and create a new instance of a park:
```
Query - http://localhost:5000/api/parks 

Body--
{
  "parkId": 0,
  "name": "string",
  "state": "string",
  "type": "string",
}
```

#### Example Query
```
https://localhost:5000/api/Parks/1
```


## Known Bugs

* _currently a bug with register route in authorization_

## License

[MIT](/LICENSE)

Copyright (c) 2022 Kelly Bruce
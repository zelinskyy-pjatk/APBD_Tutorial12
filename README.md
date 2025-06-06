# APBD_Tutorial12

Test Data to be inserted into database:

  INSERT INTO Trip (Name, Description, DateFrom, DateTo, MaxPeople)
              VALUES ('Test Trip', 'Seeded from SSMS', '2025-07-01', '2025-07-10', 20);
  
  INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel) VALUES ('Anton', 'Ivashchenko', 'antonik@gmail.com', '+48675928391', '15463789121');
  
  INSERT INTO Country (Name) VALUES ('Ukraine');
  INSERT INTO Country (Name) VALUES ('Poland');
  INSERT INTO Country (Name) VALUES ('Germany');
  INSERT INTO Country (Name) VALUES ('Netherlands');
  
  INSERT INTO Country_Trip (IdCountry, IdTrip) VALUES (1, 2);


Test Data to be received as a result of GET request:
    {
    "pageNum": 1,
    "pageSize": 10,
    "allPages": 1,
    "trips": [
      {
        "name": "Test Trip",
        "description": "Seeded from SSMS",
        "dateFrom": "2025-07-01T00:00:00",
        "dateTo": "2025-07-10T00:00:00",
        "maxPeople": 20,
        "countries": [
          {
            "name": "Ukraine"
          }
        ],
        "clients": [
          {
            "firstName": "John",
            "lastName": "Doe"
          },
          {
            "firstName": "Mark",
            "lastName": "Mohn"
          }
        ]
      }
    ]
  }


  

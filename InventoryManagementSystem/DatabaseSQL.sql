CREATE TABLE Inventory (
  itemID int not null PRIMARY KEY,
  itemName varchar(100) not null,
  tag varchar(50),
  serialNumber varchar(100),
  manufacturer varchar(100),
  modelID int,
  modelNumber varchar(50),
  category varchar(50),
  location varchar(50),
  status varchar(50),
  assignedTo int,
  dateAssigned datetime,
  documentationID int,
  datePurchased datetime,
  assignedLocation varchar(50),
  FOREIGN KEY (assignedTo) REFERENCES Employees(EmployeeID),
  FOREIGN KEY (modelID) REFERENCES ComputerSpecsList(modelID),
  FOREIGN KEY (documentationID) REFERENCES Documentation(DocID)
)

CREATE TABLE OfficeList (
  officeID int not null PRIMARY KEY,
  officeName varchar not null,
  officeFloor int
)

CREATE TABLE Employees (
  EmployeeID int not null PRIMARY KEY,
  Name varchar(100),
  Title varchar(50),
  Username varchar(50),
  EmailAddress varchar(100),
  Manager varchar(100),
  Division varchar(100),
  Location varchar(100),
  Status varchar(100),
  PhoneNumber varchar(50)
)

CREATE TABLE Documentation (
  DocID int not null PRIMARY KEY,
  DocLink varchar(255),
  DateAdded datetime
)

CREATE TABLE ComputerSpecsList (
  modelID int not null PRIMARY KEY,
  modelNumber varchar(100),
  manufacturer varchar(50),
  HD_Type varchar(50),
  HD_Capacity varchar(10),
  Ram varchar(20),
  CPU varchar(55),
  DVIPorts varchar(10),
  VGAPorts varchar(10),
  DisplayPorts varchar(10),
  HDMIPorts varchar(10),
  USBPorts varchar(10)
)


CREATE TABLE users (
  userID int NOT NULL IDENTITY(1,1),
  firstName varchar(25) NOT NULL,
  lastName varchar(25) NOT NULL,
  phone varchar(15) DEFAULT NULL,
  role int NOT NULL,
  password varchar(45) NOT NULL,
  title varchar(15) NOT NULL,
  PRIMARY KEY (userID)
);

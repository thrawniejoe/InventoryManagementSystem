

CREATE TABLE OfficeList (
  officeID int not null IDENTITY(1,1) PRIMARY KEY,
  officeName varchar(55) not null,
  officeFloor int
);

CREATE TABLE Employees (
  EmployeeID int not null IDENTITY(1,1) PRIMARY KEY,
  Name varchar(100),
  Title varchar(50),
  Username varchar(50),
  EmailAddress varchar(100),
  Manager varchar(100),
  Division varchar(100),
  Location varchar(100),
  Status varchar(100),
  PhoneNumber varchar(50)
);

CREATE TABLE Documentation (
  DocID int not null IDENTITY(1,1) PRIMARY KEY,
  DocLink varchar(255),
  DateAdded datetime
);

CREATE TABLE ComputerSpecsList (
  modelID int not null IDENTITY(1,1) PRIMARY KEY,
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
);

CREATE TABLE Roles(
	roleID int NOT NULL IDENTITY(1,1),
	Title varchar(25) NOT NULL,
	permissionLevel varchar(15),
	PRIMARY KEY(roleID)
);

CREATE TABLE Users (
  userID int NOT NULL IDENTITY(1,1),
  firstName varchar(25) NOT NULL,
  lastName varchar(25) NOT NULL,
  phone varchar(15) DEFAULT NULL,
  roleID int NOT NULL,
  password varchar(45) NOT NULL,
  title varchar(15) NOT NULL,
  emailAddress varchar(55) NOT NULL,
  PRIMARY KEY (userID),
  FOREIGN KEY (roleID) REFERENCES Roles(roleID)
);

CREATE TABLE Inventory (
  itemID int not null IDENTITY(1,1) PRIMARY KEY,
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
  dateRecordModified datetime,
  recordModifiedBy_userID int,
  documentationID int,
  datePurchased datetime,
  officeID int,
  FOREIGN KEY (recordModifiedBy_userID) REFERENCES Users(userID),
  FOREIGN KEY (officeID) REFERENCES OfficeList(officeID),
  FOREIGN KEY (assignedTo) REFERENCES Employees(EmployeeID),
  FOREIGN KEY (modelID) REFERENCES ComputerSpecsList(modelID),
  FOREIGN KEY (documentationID) REFERENCES Documentation(DocID)
);

ALTER TABLE Users
ADD FOREIGN KEY (role) REFERENCES roles(RoldID);


--CREATE VIEW [dbo].[vInventoryList]
--	AS select itemName, i.tag, i.serialNumber, c.modelNumber, i.category, i.location, i.status, e.Name, i.dateAssigned, i.dateRecordModified, u.emailAddress, i.datePurchased, i.itemID, i.officeID from Inventory as i
--INNER JOIN ComputerSpecsList as c on i.modelID = c.modelID
--INNER JOIN Employees as e on i.assignedTo = e.EmployeeID
--INNER JOIN Users as u on i.recordModifiedBy_userID = u.userID;

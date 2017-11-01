

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
  password varbinary(256) NOT NULL,
  title varchar(15),
  emailAddress varchar(55) NOT NULL,
  hashedPassword varbinary(256),
  PRIMARY KEY (userID),
  FOREIGN KEY (roleID) REFERENCES Roles(roleID)
);

CREATE TABLE Categories (
	CategoryID int not null IDENTITY(1,1) PRIMARY KEY,
	CategoryName varchar(50)
);

CREATE TABLE Locations (
	LocationID int not null IDENTITY(1,1) PRIMARY KEY,
	Location varchar(50),
	State varchar(50)
);

CREATE TABLE StatusList (
	StatusID int not null IDENTITY(1,1) PRIMARY KEY,
	Status varchar(50)
);

CREATE TABLE Inventory (
  itemID int not null IDENTITY(1,1) PRIMARY KEY,
  itemName varchar(100) not null,
  tag varchar(50),
  serialNumber varchar(100),
  manufacturer varchar(100),
  modelID int,
  modelNumber varchar(50),
  CategoryID int,
  LocationID int,
  StatusID int,
  assignedTo int,
  dateAssigned datetime,
  dateRecordModified datetime,
  recordModifiedBy_userID int,
  datePurchased datetime,
  officeID int,
  CONSTRAINT FK_UserID FOREIGN KEY (recordModifiedBy_userID) REFERENCES Users(userID),
  CONSTRAINT FK_OfficeID FOREIGN KEY (officeID) REFERENCES OfficeList(officeID),
  CONSTRAINT FK_AssignedTo FOREIGN KEY (assignedTo) REFERENCES Employees(EmployeeID),
  CONSTRAINT FK_ModelID FOREIGN KEY (modelID) REFERENCES ComputerSpecsList(modelID),
  CONSTRAINT FK_CategoryID FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
  CONSTRAINT FK_LocationID FOREIGN KEY (LocationID) REFERENCES Locations(LocationID),
  CONSTRAINT FK_StatusID FOREIGN KEY (StatusID) REFERENCES StatusList(StatusID)
);

CREATE TABLE Documentation (
  DocID int not null IDENTITY(1,1) PRIMARY KEY,
  ItemID int not null,
  DocLink varchar(255),
  DateAdded datetime,
  FOREIGN KEY (ItemID) REFERENCES Inventory(ItemID)
);

ALTER TABLE Users
ADD FOREIGN KEY (role) REFERENCES roles(RoldID);


CREATE VIEW [dbo].[vInventoryList]
	AS select itemName, i.tag, i.serialNumber, cs.modelNumber, c.CategoryName, l.Location, s.Status, e.Name, i.dateAssigned, i.dateRecordModified, u.emailAddress, i.datePurchased, i.itemID, i.officeID, i.assignedTo, e.Name from Inventory as i
INNER JOIN ComputerSpecsList as cs on i.modelID = cs.modelID
INNER JOIN Employees as e on i.assignedTo = e.EmployeeID
INNER JOIN Categories as c on i.CategoryID = c.CategoryID
INNER JOIN Locations as l on i.LocationID = l.LocationID
INNER JOIN StatusList as s on i.StatusID = s.StatusID
INNER JOIN Users as u on i.recordModifiedBy_userID = u.userID;

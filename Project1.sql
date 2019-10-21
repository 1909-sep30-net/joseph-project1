CREATE TABLE dbo.Locations
(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(25) NOT NULL,
);
GO

CREATE TABLE dbo.Customers
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(25) NOT NULL,
);
GO

CREATE TABLE dbo.Products
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name NVARCHAR(25) NOT NULL,
	Price MONEY NOT NULL
);
GO

CREATE TABLE dbo.Orders
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(ID),
	LocationId INT NOT NULL FOREIGN KEY REFERENCES Locations(ID),
	Date DATETIME2 NOT NULL,
	Total MONEY NOT NULL,
);
GO

CREATE TABLE dbo.ProductOrdered
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(ID),
	ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ID),
	Quantity INT NOT NULL,
);
GO

CREATE TABLE dbo.ProductInventory
(
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	LocationId INT NOT NULL FOREIGN KEY REFERENCES Locations(ID),
	ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ID),
	Quantity INT NOT NULL,
);

-- select * from Products
-- select * from Orders
-- select * from ProductOrdered
-- select * from ProductInventory
-- select * From dbo.Locations
-- select * from dbo.Customers
INSERT INTO dbo.Locations VALUES ('Location1');
INSERT INTO dbo.Locations VALUES ('Location2');
INSERT INTO dbo.Locations VALUES ('Location3');

-- select * from dbo.Customers
INSERT INTO dbo.Customers VALUES ('FirstName1', 'LastName1');
INSERT INTO dbo.Customers VALUES ('FirstName2', 'LastName2');
INSERT INTO dbo.Customers VALUES ('FirstName3', 'LastName3');

-- select * from dbo.Products
INSERT INTO dbo.Products VALUES ('Product1', 1.00);
INSERT INTO dbo.Products VALUES ('Product2', 2.00);
INSERT INTO dbo.Products VALUES ('Product3', 3.00);

-- select * from ProductEntry
INSERT INTO dbo.ProductInventory VALUES (1, 1, 50);
INSERT INTO dbo.ProductInventory VALUES (1, 2, 50);
INSERT INTO dbo.ProductInventory VALUES (1, 3, 50);
				
INSERT INTO dbo.ProductInventory VALUES (2, 1, 50);
INSERT INTO dbo.ProductInventory VALUES (2, 2, 50);
INSERT INTO dbo.ProductInventory VALUES (2, 3, 50);
				
INSERT INTO dbo.ProductInventory VALUES (3, 1, 50);
INSERT INTO dbo.ProductInventory VALUES (3, 2, 50);
INSERT INTO dbo.ProductInventory VALUES (3, 3, 50);

DROP TABLE dbo.ProductOrdered;
DROP TABLE dbo.ProductInventory;
DROP TABLE dbo.Orders;
DROP TABLE dbo.Products;
DROP TABLE dbo.Locations;
DROP TABLE dbo.Customers;
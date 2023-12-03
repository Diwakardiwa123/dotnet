CREATE TABLE LoginTable (
    Username varchar(50) Primary Key Not Null, 
    Passwords varchar(10) Not Null, 
    Email varchar(100) 
)

CREATE TABLE UserProfile (
    Username varchar(50) Primary Key,
    UserID VARCHAR(10) NOT NULL UNIQUE,  -- eg: UID1001
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    MobileNo VARCHAR(15),
    Email varchar(100),
    CurrentAddress VARCHAR(255),
    DOB DATE,
    Passwords VARCHAR(50)
)

ALTER TABLE UserProfile
ADD UNIQUE (UserName)

CREATE TABLE ProductListing (
    ProductID INT PRIMARY KEY IDENTITY(1001, 1) NOT NULL,
    ProductName VARCHAR(255) NOT NULL,
    ProductDescription TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT NOT NULL,
    Category VARCHAR(50),
    Manufacturer VARCHAR(50),
    ImageURL VARCHAR(255)
)

CREATE TABLE Wishlist (
    WishlistID INT PRIMARY KEY IDENTITY(101, 1),
    UserID VARCHAR(10),
    ProductID INT,
    FOREIGN KEY (UserID) REFERENCES UserProfile(UserID),
    FOREIGN KEY (ProductID) REFERENCES ProductListing(ProductID)
)

CREATE TABLE OrderDetails (
    OrderID VARCHAR(100) PRIMARY KEY,
    UserID VARCHAR(10),
    ProductID INT,
    Quantity INT,
    TotalAmount DECIMAL(10, 2),
    OrderDate DATE,
    FOREIGN KEY (UserID) REFERENCES UserProfile(UserID),
    FOREIGN KEY (ProductID) REFERENCES ProductListing(ProductID)
)

CREATE PROCEDURE InsertUserProfileProcedure (
    @Username VARCHAR(50),
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @MobileNo VARCHAR(15),
    @Email VARCHAR(100),
    @CurrentAddress VARCHAR(255),
    @DOB DATE,
    @Passwords VARCHAR(50)
)
AS
    DECLARE @UserID VARCHAR(10);

    SET @UserID = 'UID' + CAST((select COALESCE(MAX(CAST(SUBSTRING(UserID,4, 4) AS INT))+1, 1000) from UserProfile) AS VARCHAR(4))

    INSERT INTO UserProfile (
        Username, UserID, FirstName, LastName, MobileNo, Email, CurrentAddress, DOB, Passwords
    ) VALUES (
        @Username, @UserID, @FirstName, @LastName, @MobileNo, @Email, @CurrentAddress, @DOB, @Passwords
    );
GO 


CREATE PROCEDURE UpdateUserProfileProcedure (
    @Username VARCHAR(50),
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @MobileNo VARCHAR(15),
    @Email VARCHAR(100),
    @CurrentAddress VARCHAR(255),
    @DOB DATE,
    @Passwords VARCHAR(50),
    @UserID VARCHAR(10)
)
AS 
    UPDATE UserProfile
    SET
        Username = @Username,
        FirstName = @FirstName,
        LastName = @LastName,
        MobileNo = @MobileNo,
        Email = @Email,
        CurrentAddress = @CurrentAddress,
        DOB = @DOB,
        Passwords = @Passwords
    WHERE
        UserID = @UserID;
GO

-- Procedure for Products

CREATE PROCEDURE InsertProductProcedure (
    @ProductName VARCHAR(255),
    @ProductDescription TEXT,
    @Price DECIMAL(10, 2),
    @StockQuantity INT,
    @Category VARCHAR(50),
    @Manufacturer VARCHAR(50),
    @ImageURL VARCHAR(255)
)
AS
    INSERT INTO ProductListing (ProductName, ProductDescription, Price, StockQuantity, Category, Manufacturer, ImageURL)
    VALUES (@ProductName, @ProductDescription, @Price, @StockQuantity, @Category, @Manufacturer, @ImageURL);
GO

CREATE PROCEDURE UpdateProductProcedure (
    @ProductID INT,
    @ProductName VARCHAR(255),
    @ProductDescription TEXT,
    @Price DECIMAL(10, 2),
    @StockQuantity INT,
    @Category VARCHAR(50),
    @Manufacturer VARCHAR(50),
    @ImageURL VARCHAR(255)
)
AS
    UPDATE ProductListing
    SET
        ProductName = @ProductName,
        ProductDescription = @ProductDescription,
        Price = @Price,
        StockQuantity = @StockQuantity,
        Category = @Category,
        Manufacturer = @Manufacturer,
        ImageURL = @ImageURL
    WHERE
        ProductID = @ProductID;
GO


-- Procedure for Wishlist

CREATE PROCEDURE InsertWishlistProcedure (
    @UserID VARCHAR(10),
    @ProductID INT
)
AS
    INSERT INTO Wishlist (UserID, ProductID)
    VALUES (@UserID, @ProductID);
GO

-- Procedures for Orderdetails 

CREATE PROCEDURE InsertOrderDetailsProcedure (
    @UserID VARCHAR(10),
    @ProductID INT,
    @Quantity INT,
    @TotalAmount DECIMAL(10, 2),
    @OrderDate DATE
)
AS
    DECLARE @OrderID VARCHAR(10);

    SET @OrderID = 'ORDID' + CAST((select COALESCE(MAX(CAST(SUBSTRING(OrderID,6, 7) AS INT))+1, 1012001) from OrderDetails) AS VARCHAR(4))

    INSERT INTO OrderDetails (OrderID, UserID, ProductID, Quantity, TotalAmount, OrderDate)
    VALUES (@OrderID, @UserID, @ProductID, @Quantity, @TotalAmount, @OrderDate);
GO


CREATE PROCEDURE UpdateOrderDetailsProcedure (
    @OrderID VARCHAR(100),
    @UserID VARCHAR(10),
    @ProductID INT,
    @Quantity INT,
    @TotalAmount DECIMAL(10, 2),
    @OrderDate DATE
)
AS
    UPDATE OrderDetails
    SET
        UserID = @UserID,
        ProductID = @ProductID,
        Quantity = @Quantity,
        TotalAmount = @TotalAmount,
        OrderDate = @OrderDate
    WHERE
        OrderID = @OrderID;
GO

-----------------------------------------
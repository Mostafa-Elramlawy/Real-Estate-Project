-- Create Database Name: Grad
CREATE DATABASE Grad;
GO

-- Use Database
USE Grad;
GO

-- Create Tables

CREATE TABLE TB_user (
	UserID int identity(1,1) primary key,
	PhoneNumber int,
	UserName NVARCHAR(255),
	Password NVARCHAR(255),
	Confirm_Password NVARCHAR(255),
	image NVARCHAR(Max),
	Email  NVARCHAR(255),
	Gender char,
	Name NVARCHAR(255),
	Address NVARCHAR(255),
	Role_id int
);


select *  from TB_user
ALTER TABLE TB_user
DROP CONSTRAINT FK_TB_user;


ALTER TABLE TB_user ADD CONSTRAINT FK_TB_user FOREIGN KEY (Role_id) REFERENCES  Role(Role_id);

CREATE TABLE Role (
	Role_id int  primary key,
	role_name varchar(50),
);

ALTER TABLE TB_user
ADD CONSTRAINT DF_TB_user_Role_id DEFAULT 1 FOR Role_id;

CREATE TABLE CraftsMen2 (
	CM_ID int identity(1,1) primary key,
	CM_Name varchar(255),
	CM_Address varchar(255),
	CM_Job varchar(255),
	CM_Phone int
);


CREATE TABLE Auction (
	AuctionID int identity(1,1) primary key,
	Bid_End DATETIME ,
	Bid_start DATETIME,
	Bid_Name varchar(255),
	Start_price int,
	lowest_bidding_price int,
	Pro_id int,
	AStatus_ID int,
);

ALTER TABLE Auction  ADD CONSTRAINT FK_Auction FOREIGN KEY (Pro_id) REFERENCES  Property(Pro_id);

ALTER TABLE Auction  ADD CONSTRAINT FK_Auction1 FOREIGN KEY (AStatus_ID) REFERENCES  AStatus(AStatus_ID);

CREATE TABLE Transactions (
	Transaction_ID int identity(1,1) primary key,
	Pro_Price int,
	Pro_id int,
	Transaction_date datetime,
	AuctionID int,
	UserID int,
);

ALTER TABLE Transactions ADD CONSTRAINT FK_Transactions1 FOREIGN KEY (AuctionID) REFERENCES  Auction(AuctionID);

ALTER TABLE Transactions ADD CONSTRAINT FK_Transactions2 FOREIGN KEY (UserID) REFERENCES  TB_user(UserID);


CREATE TABLE Favorite (
	fav_id int  identity(1,1) primary key ,
	Pro_id int ,
	UserID int,
);

ALTER TABLE Favorite ADD CONSTRAINT FK_Favorite FOREIGN KEY (Pro_id) REFERENCES  Property(Pro_id);

ALTER TABLE Favorite ADD CONSTRAINT FK_Favorite1 FOREIGN KEY (UserID) REFERENCES  TB_user(UserID);

CREATE TABLE Property (
	Pro_id int identity(1,1) primary key ,
	Price int,
	Title varchar(255) ,
	Description varchar(255),
	NumberOfBedRooms varchar(225),
	NumberOfBathRooms varchar(225),
	NumOfLivingRoom varchar(225),
	size float,
	garage_num int,
	Floor_Num int,
	Building_Num int,
	--Date DateTime,
	Status_ID int,
	P_Type_ID int,
	City_id int,
	Government_id int,
	District_ID int,
	UserID int,
	ImagePath VARCHAR(255)
);

ALTER TABLE Property
ADD P3D_path VARCHAR(255);

CREATE TABLE City (
	City_id int IDENTITY(1,1) PRIMARY KEY,
	City_name varchar(50),
);


ALTER TABLE Property ADD CONSTRAINT FK_P1 FOREIGN KEY (Status_ID) REFERENCES  Status(Status_ID);

ALTER TABLE Property ADD CONSTRAINT FK_P2 FOREIGN KEY (P_Type_ID) REFERENCES  P_Type(P_Type_ID);

ALTER TABLE Property ADD CONSTRAINT FK_P3 FOREIGN KEY (City_id) REFERENCES  City(City_id);

ALTER TABLE Property ADD CONSTRAINT FK_P4 FOREIGN KEY (Government_id) REFERENCES  Government(Government_id);

ALTER TABLE Property ADD CONSTRAINT FK_P5 FOREIGN KEY (District_ID) REFERENCES  District(District_ID);

ALTER TABLE Property ADD CONSTRAINT FK_P6 FOREIGN KEY (UserID) REFERENCES  TB_user(UserID);

CREATE TABLE Government (
    Government_id int IDENTITY(1,1) PRIMARY KEY,
    Government_name varchar(50)
);

CREATE TABLE P_Type (
	P_Type_ID int IDENTITY(1,1) PRIMARY KEY,
	P_Type_name varchar(50),
	
);

CREATE TABLE Status (
	Status_ID int IDENTITY(1,1) PRIMARY KEY,
	Status_name varchar(50),
);

CREATE TABLE AStatus (
	AStatus_ID int identity(1,1) primary key ,
	AStatus_name varchar(50),
);

CREATE TABLE Property_img (
    img_id INT IDENTITY(1, 1) PRIMARY KEY,
    img_name VARCHAR(255),
    img_path VARCHAR(255),
    Pro_id INT FOREIGN KEY REFERENCES Property(Pro_id)
);


CREATE TABLE District (
	District_ID int IDENTITY(1,1) PRIMARY KEY,
	District_name varchar(255),
);


--------------------------------------

-----------------------------------------


Create procedure spGetAuction
as
Begin
select AuctionID, Bid_End, Bid_start,Bid_Name, Start_price, lowest_bidding_price,Pro_id,AStatus_ID

from Auction
End


Create procedure spAddAuction
--@AuctionID nvarchar(15),
@Bid_End nvarchar(50),
@Bid_start nvarchar(50),
@Bid_Name nvarchar(50),
@Start_price nvarchar(50),
@lowest_bidding_price nvarchar(50),
@AStatus_ID nvarchar(50),
@Pro_id nvarchar(50)
as
   Begin
       DECLARE 
--@AuctionID_cast nvarchar(15),
@Bid_End_cast nvarchar(15),
@Bid_start_cast nvarchar(255),
@Bid_Name_cast nvarchar(50),
@Start_price_cast nvarchar(255),
@lowest_bidding_price_cast nvarchar(255),
@AStatus_IDcast nvarchar(255),
@Pro_idcast nvarchar(255)

	--SET @AuctionID_cast = CAST(@AuctionID AS nvarchar)
	SET @Bid_End_cast = CAST(@Bid_End AS nvarchar)
	SET @Bid_start_cast = CAST(@Bid_start AS nvarchar)
	SET @Bid_Name_cast = CAST(@Bid_Name AS nvarchar)
	SET @Start_price_cast = CAST(@Start_price AS nvarchar)
	SET @lowest_bidding_price_cast = CAST(@lowest_bidding_price AS nvarchar)
	SET @AStatus_IDcast = CAST(@AStatus_ID AS nvarchar)
	SET @Pro_idcast = CAST(@Pro_id AS nvarchar)
	
	Insert into Auction (Bid_End, Bid_start,Bid_Name, Start_price, lowest_bidding_price,AStatus_ID,Pro_id)

	Values ( @Bid_End, @Bid_start, @Bid_Name, @Start_price, @lowest_bidding_price,@AStatus_ID,@Pro_id)
End


Create procedure spEidtAuction
@AuctionID nvarchar(15),
@Bid_End nvarchar(50),
@Bid_start nvarchar(50),
@Bid_Name nvarchar(50),
@Start_price nvarchar(50),
@lowest_bidding_price nvarchar(50),
@Pro_id nvarchar(15)

as
   Begin
       DECLARE 
@AuctionID_cast nvarchar(15),
@Bid_End_cast nvarchar(15),
@Bid_start_cast nvarchar(255),
@Bid_Name_cast nvarchar(50),
@Start_price_cast nvarchar(255),
@lowest_bidding_price_cast nvarchar(255),
@Pro_id_cast nvarchar(255)

	SET @AuctionID_cast = CAST(@AuctionID AS nvarchar)
	SET @Bid_End_cast = CAST(@Bid_End AS nvarchar)
	SET @Bid_start_cast = CAST(@Bid_start AS nvarchar)
	SET @Bid_Name_cast = CAST(@Bid_Name AS nvarchar)
	SET @Start_price_cast = CAST(@Start_price AS nvarchar)
	SET @lowest_bidding_price_cast = CAST(@lowest_bidding_price AS nvarchar)
	SET @Pro_id_cast = CAST(@Pro_id AS nvarchar)

	 UPDATE Auction
    SET Bid_End = @Bid_End,
        Bid_start = @Bid_start,
        Bid_Name = @Bid_Name,
        Start_price = @Start_price,
        lowest_bidding_price = @lowest_bidding_price,
        Pro_id = @Pro_id,
        AStatus_ID = 2 -- Set the AStatus_ID to 2 (Approved)
    WHERE AuctionID = @AuctionID;
End

-----------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------


Create procedure spGetTransactions
as
Begin
select Transaction_ID, Pro_Price, Transaction_date,AuctionID, UserID

from Transactions
End


Create procedure spAddTransactions

--@Transaction_ID nvarchar(50),
@Pro_Price nvarchar(50),
@Transaction_date nvarchar(50),
@AuctionID nvarchar(50),
@UserID nvarchar(15)
as
Begin
       DECLARE 
--@Transaction_ID nvarchar(50),
@Pro_Price_cast nvarchar(50),
@Transaction_date_cast nvarchar(50),
@AuctionID_cast nvarchar(50),
@UserID_cast nvarchar(15)
	---SET @Transaction_ID_cast = CAST(@Transaction_ID AS nvarchar)
	SET @Pro_Price_cast = CAST(@Pro_Price AS nvarchar)
	SET @Transaction_date_cast = CAST(@Transaction_date AS nvarchar)
	SET @AuctionID_cast = CAST(@AuctionID AS nvarchar)
	SET @UserID_cast = CAST(@UserID AS nvarchar)
	
	Insert Transactions (Pro_Price, Transaction_date,AuctionID, UserID)

	Values (@Pro_Price, @Transaction_date,@AuctionID, @UserID)
End


--CREATE PROCEDURE FindMaxNumber
--AS
--BEGIN
--    SELECT MAX(Pro_Price) as Pro_Price  FROM Transactions;
--END

----MaxNumber


CREATE PROCEDURE [dbo].[FindMaxNumber]
    @AuctionID INT
AS
BEGIN
    SELECT MAX(Pro_Price) AS MaxPrice
    FROM Transactions
    WHERE AuctionID = @AuctionID
END


--EXEC FindMaxNumber;
-----------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------

--regester start

Create procedure spGetAllusers
as
Begin
select UserID, UserName, Password, Confirm_Password, Gender, Name, PhoneNumber, image, Email,
		Address, Role_id

from TB_user
End

Create procedure spAddUser
@PhoneNumber nvarchar(15),
@UserName nvarchar(255),
@Password nvarchar(255),
@Confirm_Password nvarchar(255),
@image nvarchar(Max),
@Email  nvarchar(255),
@Gender nvarchar(255),
@Name nvarchar(255),
@Address nvarchar(255),
@Role_id nvarchar(255)

as
Begin
	DECLARE @PhoneNumber_cast nvarchar(15),@UserName_cast nvarchar(255),@Password_cast nvarchar(255),
			@Confirm_Password_cast nvarchar(255),@image_cast nvarchar(Max),@Email_cast nvarchar(255),@Gender_cast nvarchar(255),
			@Name_cast nvarchar(255),@Address_cast nvarchar(255),@Role_id_cast nvarchar(255)

	SET @PhoneNumber_cast = CAST(@PhoneNumber AS nvarchar)
	SET @UserName_cast = CAST(@UserName AS nvarchar)
	SET @Password_cast = CAST(@Password AS nvarchar)
	SET @Confirm_Password_cast = CAST(@Password AS nvarchar)
	SET @image_cast = CAST(@image AS nvarchar)
	SET @Email_cast = CAST(@Email AS nvarchar)
	SET @Gender_cast = CAST(@Gender AS nvarchar)
	SET @Name_cast = CAST(@Name AS nvarchar)
	SET @Address_cast = CAST(@Address AS nvarchar)
	SET @Role_id_cast = CAST(@Role_id AS nvarchar)

	Insert into TB_user (UserName, Password, Confirm_Password, Gender, Name, PhoneNumber, image, Email,
		Address,Role_id)

	Values (@UserName, @Password, @Confirm_Password, @Gender, @Name, @PhoneNumber, @image, @Email,
		@Address, @Role_id)
End

--regester end

-----------------------------------------------------------------------------------

--Property start

CREATE PROCEDURE spGetAllProperty
AS
BEGIN
    SELECT Pro_id, Price, Title, Description, NumberOfBedRooms, NumberOfBathRooms, NumOfLivingRoom, Floor_Num,
        Building_Num, size, garage_num, ImagePath, P3D_path,
        Status_ID, City_id, P_Type_ID, Government_id, District_ID, UserID
    FROM Property
END


CREATE PROCEDURE spAddProperty
@Price nvarchar(500),
@Title  nvarchar(255),
@Description nvarchar(255),
@NumberOfBedRooms nvarchar(255),
@NumberOfBathRooms nvarchar(255),
@NumOfLivingRoom nvarchar(255),
@size nvarchar(255),
@garage_num nvarchar(255),
@Floor_Num nvarchar(255),
@Building_Num nvarchar(255),
@Status_ID nvarchar(255),
@P_Type_ID nvarchar(255),
@City_id nvarchar(255),
@Government_id nvarchar(255),
@District_ID nvarchar(255),
@UserID nvarchar(255),
@ImagePath VARCHAR(255),
@P3D_path VARCHAR(255)
AS
BEGIN
    DECLARE @Price_cast nvarchar(500), @Title_cast nvarchar(255),@Description_cast nvarchar(255),@size_cast nvarchar(255),@garage_num_cast nvarchar(255),
    @NumberOfBedRooms_cast nvarchar(255),@NumberOfBathRooms_cast nvarchar(255),@NumOfLivingRoom_cast nvarchar(255),@Floor_Num_cast nvarchar(255),
    @Building_Num_cast nvarchar(255),
    --@Date_cast DateTime,
    @Status_ID_cast nvarchar(255),@P_Type_ID_cast nvarchar(255), @P3D_path_cast nvarchar(255),
    @City_id_cast nvarchar(255),@Government_id_cast nvarchar(255),@District_ID_cast nvarchar(255),@UserID_cast nvarchar(255)

    SET @Price_cast = CAST(@Price AS nvarchar)
    SET @Title_cast = CAST(@Title AS nvarchar)
    SET @Description_cast = CAST(@Description AS nvarchar)
    SET @NumberOfBedRooms_cast = CAST(@NumberOfBedRooms AS nvarchar)
    SET @NumberOfBathRooms_cast = CAST(@NumberOfBathRooms AS nvarchar)
    SET @NumOfLivingRoom_cast = CAST(@NumOfLivingRoom AS nvarchar)
    SET @size_cast = CAST(@size AS nvarchar)
    SET @garage_num_cast = CAST(@garage_num AS nvarchar)
	SET @P3D_path_cast = CAST(@P3D_path AS nvarchar)
    SET @Floor_Num_cast = CAST(@Floor_Num AS nvarchar)
    SET @Building_Num_cast = CAST(@Building_Num AS nvarchar)
    --SET @Date_cast = CAST(@Date AS DateTime)
    SET @Status_ID_cast = CAST(@Status_ID AS nvarchar)
    SET @P_Type_ID_cast = CAST(@P_Type_ID AS nvarchar)
    SET @City_id_cast = CAST(@City_id AS nvarchar)
    SET @Government_id_cast = CAST(@Government_id AS nvarchar)
    SET @District_ID_cast = CAST(@District_ID AS nvarchar)
    SET @UserID_cast = CAST(@UserID AS nvarchar)

    INSERT INTO Property (Price, Title, Description, NumberOfBedRooms, NumberOfBathRooms,
        NumOfLivingRoom, Floor_Num, Building_Num, garage_num, size,Status_ID, P_Type_ID, ImagePath, P3D_path,
        City_id, Government_id, District_ID, UserID)

    Values (@Price, @Title, @Description, @NumberOfBedRooms, @NumberOfBathRooms,
			@NumOfLivingRoom, @Floor_Num, @Building_Num, @garage_num, @size,
			--@Date, 
			@Status_ID, @P_Type_ID, @ImagePath,@P3D_path,
			@City_id, @Government_id, @District_ID, @UserID)

    DECLARE @Pro_id INT
    SET @Pro_id = SCOPE_IDENTITY()

    INSERT INTO Property_img (img_name, img_path, Pro_id)
    VALUES (@ImagePath, @ImagePath, @Pro_id)

    SELECT @Pro_id AS Pro_id
END

CREATE PROCEDURE spUpdateProperty
(
    @Pro_id INT,
    @Price INT,
    @Title NVARCHAR(MAX),
    @Description NVARCHAR(MAX),
    @NumberOfBedRooms NVARCHAR(MAX),
    @NumberOfBathRooms NVARCHAR(MAX),
    @NumOfLivingRoom NVARCHAR(MAX),
    @size INT,
    @garage_num INT,
    @Floor_Num INT,
    @Building_Num INT,
    @Status_ID INT,
    @P_Type_ID INT,
    @City_id INT,
    @Government_id INT,
    @District_ID INT,
    @UserID INT,
    @ImagePath NVARCHAR(MAX) = NULL,
    @P3D_path NVARCHAR(MAX) = NULL
)
AS
BEGIN
    UPDATE Property
    SET Price = @Price,
        Title = @Title,
        Description = @Description,
        NumberOfBedRooms = @NumberOfBedRooms,
        NumberOfBathRooms = @NumberOfBathRooms,
        NumOfLivingRoom = @NumOfLivingRoom,
        size = @size,
        garage_num = @garage_num,
        Floor_Num = @Floor_Num,
        Building_Num = @Building_Num,
        Status_ID = @Status_ID,
        P_Type_ID = @P_Type_ID,
        City_id = @City_id,
        Government_id = @Government_id,
        District_ID = @District_ID,
        UserID = @UserID,
        ImagePath = @ImagePath,
        P3D_path = @P3D_path
    WHERE Pro_id = @Pro_id
END

-----------end property-------------------
------------------------------------------
------------------------------------------

CREATE PROCEDURE spAddImage
    @img_name VARCHAR(255),
    @img_path VARCHAR(255),
    @Pro_id INT
AS
BEGIN
    INSERT INTO Property_img (img_name, img_path, Pro_id)
    VALUES (@img_name, @img_path, @Pro_id)
END

-- Stored procedure to retrieve all images
CREATE PROCEDURE GetImages
    @Pro_id INT
AS
BEGIN
    SELECT img_id, img_name, img_path, Pro_id
    FROM Property_img
    WHERE Pro_id = @Pro_id
END

------------------------------------------
------------------------------------------

CREATE PROCEDURE spGetPropertyById
    @AuctionID INT
AS
BEGIN
    SELECT
    
        Title,
        ImagePath
    FROM
        Property,Auction
    WHERE
        Auction.AuctionID=@AuctionID and Auction.Pro_id=Property.Pro_id;
END


EXEC spGetPropertyById @AuctionID = 13;

select * from Auction

select * from Property where Pro_id=5;

CREATE PROCEDURE GetMaxProPriceByAuctionID
    @AuctionID INT
AS
BEGIN
    SELECT t.AuctionID, t.Pro_Price AS MaxProPrice, u.UserName,
           p.Title, p.NumberOfBedRooms, p.NumberOfBathRooms, p.NumOfLivingRoom,ImagePath
    FROM Transactions t
    JOIN TB_user u ON t.UserID = u.UserID
    JOIN Auction a ON t.AuctionID = a.AuctionID
    JOIN Property p ON a.Pro_id = p.Pro_id
    WHERE t.AuctionID = @AuctionID and 
  t.Pro_Price=(select max (x.Pro_Price) from Transactions x where x.AuctionID = @AuctionID)
END

CREATE PROCEDURE spGetLowestBiddingPriceById
    @AuctionID INT
AS
BEGIN
    SELECT lowest_bidding_price
    FROM Auction
    WHERE AuctionID = @AuctionID;
END

CREATE PROCEDURE spGetStartPriceById
    @AuctionID INT
AS
BEGIN
    SELECT Start_price
    FROM Auction
    WHERE AuctionID = @AuctionID;
END

Create PROCEDURE [dbo].[GetAuctionBidEnd]
    @AuctionID INT
AS
BEGIN
    SELECT Bid_End
    FROM Auction
    WHERE AuctionID = @AuctionID
END

CREATE PROCEDURE getimagebyid
    @AuctionID INT
AS
BEGIN
    SELECT
    
        ImagePath
    FROM
        Property,Auction
    WHERE
        Auction.AuctionID=@AuctionID and Auction.Pro_id=Property.Pro_id;
END

EXEC GetMaxProPriceByAuctionID @AuctionID = 13


CREATE PROCEDURE GetMaxProPriceByAuctionID
    @AuctionID INT
AS
BEGIN
    SELECT t.AuctionID, MAX(t.Pro_Price) AS MaxProPrice, u.UserName,
           p.Title, p.NumberOfBedRooms, p.NumberOfBathRooms, p.NumOfLivingRoom,ImagePath
    FROM Transactions t
    JOIN TB_user u ON t.UserID = u.UserID
    JOIN Auction a ON t.AuctionID = a.AuctionID
    JOIN Property p ON a.Pro_id = p.Pro_id
    WHERE t.AuctionID = @AuctionID
    GROUP BY t.AuctionID, u.UserName, p.Title, p.NumberOfBedRooms, p.NumberOfBathRooms, p.NumOfLivingRoom,ImagePath
END


CREATE PROCEDURE GetMaxProPriceByAuctionIDDa
    @AuctionID INT
AS
BEGIN
    DECLARE @MaxProPrice INT

    -- Set initial value of @MaxProPrice to zero
    SET @MaxProPrice = 0

    -- Check if an auction has an available Pro_Price value
    IF EXISTS (
        SELECT 1
        FROM Transactions
        WHERE AuctionID = @AuctionID
    )
    BEGIN
        -- Get the maximum Pro_Price for the given AuctionID
        SELECT @MaxProPrice = MAX(Pro_Price)
        FROM Transactions
        WHERE AuctionID = @AuctionID
    END

    -- Return the AuctionID and MaxProPrice
    SELECT @AuctionID AS AuctionID, @MaxProPrice AS MaxProPrice
END

EXEC GetMaxProPriceByAuctionIDDa @AuctionID = 1023


CREATE PROCEDURE GetRoleByUserID
    @UserID INT
AS
BEGIN
    SELECT Role_id
    FROM TB_user
    WHERE UserID = @UserID
END
EXEC GetRoleByUserID @UserID=1

------------------------------------------------------------------------------------------


CREATE PROCEDURE spInsertCity
    @CityName VARCHAR(50)
AS
BEGIN
    INSERT INTO City (City_name)
    VALUES (@CityName)
END

-------------------------------------

CREATE PROCEDURE spInsertDistrict
    @DistrictName VARCHAR(255)
AS
BEGIN
    INSERT INTO District (District_name)
    VALUES (@DistrictName)
END

--------------------------------------

CREATE PROCEDURE spInsertP_Type
    @P_TypeName VARCHAR(50)
AS
BEGIN
    INSERT INTO P_Type (P_Type_name)
    VALUES (@P_TypeName)
END

----------------------------------------

CREATE PROCEDURE spInsertGovernment
    @GovernmentName VARCHAR(50)
AS
BEGIN
    INSERT INTO Government (Government_name)
    VALUES (@GovernmentName)
END

-----------------------------------------

CREATE PROCEDURE spInsertStatus
    @StatusName VARCHAR(50)
AS
BEGIN
    INSERT INTO Status (Status_name)
    VALUES (@StatusName)
END

------------------------------------------
------------------------------------------
------------------------------------------

CREATE PROCEDURE spGetCraftsMen
AS
BEGIN
    SELECT CM_ID, CM_Name, CM_Address, CM_Job, CM_Phone
    FROM CraftsMen2
END

------------------------------------------

-- spAddCraftsMen
CREATE PROCEDURE spAddCraftsMen
    @CM_Name NVARCHAR(50),
    @CM_Address NVARCHAR(100),
    @CM_Job NVARCHAR(50),
    @CM_Phone INT
AS
BEGIN
    INSERT INTO CraftsMen2 (CM_Name, CM_Address, CM_Job, CM_Phone)
    VALUES (@CM_Name, @CM_Address, @CM_Job, @CM_Phone)
END

------------------------------------------

-- spUpdateCraftsMen
CREATE PROCEDURE spUpdateCraftsMen
    @CM_ID INT,
    @CM_Name NVARCHAR(50),
    @CM_Address NVARCHAR(100),
    @CM_Job NVARCHAR(50),
    @CM_Phone INT
AS
BEGIN
    UPDATE CraftsMen2
    SET CM_Name = @CM_Name,
        CM_Address = @CM_Address,
        CM_Job = @CM_Job,
        CM_Phone = @CM_Phone
    WHERE CM_ID = @CM_ID
END

------------------------------------------

CREATE PROCEDURE spDeleteCraftsMen
    @CM_ID INT
AS
BEGIN
    DELETE FROM CraftsMen2
    WHERE CM_ID = @CM_ID
END

------------------------------------------

CREATE PROCEDURE spGetCraftsMenById
    @CM_ID INT
AS
BEGIN
    SELECT CM_ID, CM_Name, CM_Address, CM_Job, CM_Phone
    FROM CraftsMen2
    WHERE CM_ID = @CM_ID
END




---------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------- Filling Tables with Data --------------------------------------------


-- Inserting into TB_user table
INSERT INTO TB_user (PhoneNumber, UserName, Password, Confirm_Password, image, Email, Gender, Name, Address, Role_id)
VALUES 
(1234567890, 'adminUser', 'password123', 'password123', 'image1.jpg', 'admin@example.com', 'M', 'Admin User', '123 Admin St', 0),
(1234567891, 'johnDoe', 'password123', 'password123', 'image2.jpg', 'john@example.com', 'M', 'John Doe', '456 User St', 1),
(1234567892, 'janeDoe', 'password123', 'password123', 'image3.jpg', 'jane@example.com', 'F', 'Jane Doe', '789 Manager St', 0),
(1234567893, 'craftsMan1', 'password123', 'password123', 'image4.jpg', 'craftsman@example.com', 'M', 'CraftsMan One', '101 CraftsMan St', 1),
(1234567894, 'customer1', 'password123', 'password123', 'image5.jpg', 'customer@example.com', 'F', 'Customer One', '202 Customer St', 1);

-- Inserting into Role table
INSERT INTO Role (Role_id, role_name)
VALUES 
(1, 'Admin'),
(0, 'User'),
(1, 'Manager'),
(0, 'Customer'),
(0, 'CraftsMan');

-- Inserting into CraftsMen2 table
INSERT INTO CraftsMen2 (CM_Name, CM_Address, CM_Job, CM_Phone)
VALUES 
('CraftsMan A', 'Location A', 'Carpenter', 1111111111),
('CraftsMan B', 'Location B', 'Plumber', 1111111112),
('CraftsMan C', 'Location C', 'Electrician', 1111111113),
('CraftsMan D', 'Location D', 'Painter', 1111111114),
('CraftsMan E', 'Location E', 'Mason', 1111111115);

-- Inserting into Auction table
INSERT INTO Auction (Bid_End, Bid_start, Bid_Name, Start_price, lowest_bidding_price, AStatus_ID, Pro_id)
VALUES 
('2025-02-01 10:00:00', '2025-01-30 12:00:00', 'Auction 1', 500000, 20000, 1, 1),
('2025-02-02 10:00:00', '2025-01-31 12:00:00', 'Auction 2', 600000, 70000, 2, 2),
('2025-02-03 10:00:00', '2025-02-01 12:00:00', 'Auction 3', 700000, 30000, 1, 3),
('2025-02-04 10:00:00', '2025-02-02 12:00:00', 'Auction 4', 800000, 60000, 2, 4),
('2025-02-05 10:00:00', '2025-02-03 12:00:00', 'Auction 5', 1000000, 100000, 1, 5);

-- Inserting into Transactions table
INSERT INTO Transactions (Pro_Price, Pro_id, Transaction_date, AuctionID, UserID)
VALUES 
(500000, 1, '2025-01-30 14:00:00', 1, 1),
(600000, 2, '2025-01-31 14:00:00', 2, 2),
(700000, 3, '2025-02-01 14:00:00', 3, 3),
(750000, 4, '2025-02-02 14:00:00', 4, 4),
(950000, 5, '2025-02-03 14:00:00', 5, 5);

-- Inserting into Favorite table
INSERT INTO Favorite (UserID, Pro_id) 
VALUES 
(9, 1), 
(10, 3), 
(11, 4), 
(12, 2), 
(13, 5);

-- Inserting into Property table
INSERT INTO Property (Price, Title, Description, NumberOfBedRooms, NumberOfBathRooms, NumOfLivingRoom, size, garage_num, Floor_Num, Building_Num, Status_ID, P_Type_ID, City_id, Government_id, District_ID, UserID, ImagePath, P3D_path)
VALUES 
(500000, 'Property 1', '3 Bed, 2 Bath, 1 Living Room', '3', '2', '1', 120.5, 1, 5, 101, 1, 1, 1, 1, 1, 1, 'property1.jpg', '3Dpath1'),
(650000, 'Property 2', '4 Bed, 3 Bath, 1 Living Room', '4', '3', '1', 150.5, 2, 10, 102, 1, 2, 2, 2, 2, 2, 'property2.jpg', '3Dpath2'),
(700000, 'Property 3', '5 Bed, 4 Bath, 2 Living Rooms', '5', '4', '2', 180.5, 3, 15, 103, 2, 1, 3, 3, 3, 3, 'property3.jpg', '3Dpath3'),
(800000, 'Property 4', '6 Bed, 5 Bath, 3 Living Rooms', '6', '5', '3', 220.5, 4, 20, 104, 2, 2, 4, 4, 4, 4, 'property4.jpg', '3Dpath4'),
(1200000, 'Property 5', '7 Bed, 6 Bath, 4 Living Rooms', '7', '6', '4', 300.5, 5, 25, 105, 3, 1, 5, 5, 5, 5, 'property5.jpg', '3Dpath5');

-- Inserting into City table
INSERT INTO City (City_name)
VALUES 
('Cairo'),
('Alexandria'),
('Giza'),
('Tanta'),
('Sharm El Sheikh');

-- Inserting into Government table
INSERT INTO Government (Government_name)
VALUES 
('Cairo Gov'),
('Alexandria Gov'),
('Giza Gov'),
('Qalyubia Gov'),
('Matrouh Gov');

-- Inserting into P_Type table
INSERT INTO P_Type (P_Type_name)
VALUES
('Residential'),
('Commercial'),
('Industrial'),
('Agricultural'),
('Mixed-use');

-- Inserting into Status table
INSERT INTO Status (Status_name)
VALUES
('For Sale'),
('Sold'),
('Under Auction'),
('Pending'),
('Under Maintenance');

-- Inserting into AStatus table
INSERT INTO AStatus (AStatus_name)
VALUES
('Active'),
('Completed'),
('Canceled'),
('Upcoming'),
('Closed');

-- Inserting into Property_img table
INSERT INTO Property_img (img_name, img_path, Pro_id)
VALUES
('front_view.jpg', 'images/property_1_1.jpg', 1),
('living_room.jpg', 'images/property_1_2.jpg', 2),
('office_building.jpg', 'images/property_2_1.jpg', 3),
('land_plot.jpg', 'images/property_3_1.jpg', 4),
('terrace_view.jpg', 'images/property_4_1.jpg', 5),

-- Inserting into District table
INSERT INTO District (District_name)
VALUES
('Downtown'),
('Maadi'),
('Smouha'),
('6th of October'),
('Al-Salam');



SELECT * FROM TB_user;





CREATE TABLE [dbo].[Order]
(
	[Order_Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [User_Id] NVARCHAR(128) NOT NULL, 
    [Pay_Method] NVARCHAR(16) NOT NULL, 
    [Total_Price] FLOAT NOT NULL, 
    CONSTRAINT [FK_Order_User] FOREIGN KEY ([User_Id]) REFERENCES [AspNetUsers]([Id])
)

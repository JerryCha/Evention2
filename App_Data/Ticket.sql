CREATE TABLE [dbo].[Tickets]
(
	[Sku_Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [Event_Id] INT NOT NULL, 
    [Sku_Name] NVARCHAR(32) NOT NULL, 
    [Price] FLOAT NOT NULL, 
    CONSTRAINT [FK_Events_EventId] FOREIGN KEY ([Event_Id]) REFERENCES [Events]([EventId]) 
)

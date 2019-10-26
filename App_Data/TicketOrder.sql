CREATE TABLE [dbo].[TicketOrder]
(
	[Order_Id] INT NOT NULL PRIMARY KEY, 
    [Sku_Id] INT NOT NULL, 
    [Qty] INT NOT NULL, 
    CONSTRAINT [FK_TicketOrder_Ticket] FOREIGN KEY ([Sku_Id]) REFERENCES [Tickets]([Sku_Id]),
    CONSTRAINT [FK_TicketOrder_Order] FOREIGN KEY ([Order_Id]) REFERENCES [Order]([Order_Id]),
)

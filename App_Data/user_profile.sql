CREATE TABLE UserProfiles
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(64) NULL, 
    [LastName] NVARCHAR(64) NULL, 
    [MiddleName] NVARCHAR(64) NULL, 
    [Street] NVARCHAR(128) NULL, 
    [Surburb] NVARCHAR(64) NULL, 
    [State] NVARCHAR(16) NULL, 
    [PostCode] NVARCHAR(16) NULL, 
    [Birthday] DATETIME NULL, 
    [Gender] INT NULL
)

ALTER TABLE UserProfiles
ADD CONSTRAINT id_reference FOREIGN KEY (Id) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;
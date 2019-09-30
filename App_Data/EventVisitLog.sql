﻿CREATE TABLE EventVisitLog
(
	[Record_Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [Viewer_Id] NVARCHAR(128) NULL, 
    [Event_Id] INT NOT NULL, 
    [IP_Address] NVARCHAR(15) NOT NULL, 
    [View_Time] DATETIME NOT NULL
)

ALTER TABLE EventVisitLog
ADD CONSTRAINT user_log FOREIGN KEY (Viewer_Id) REFERENCES AspNetUsers (Id) ON DELETE NO ACTION;

ALTER TABLE EventVisitLog
ADD CONSTRAINT event_log FOREIGN KEY (Event_Id) REFERENCES Events (EventId) ON DELETE NO ACTION;

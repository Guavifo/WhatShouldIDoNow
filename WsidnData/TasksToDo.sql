﻿CREATE TABLE [dbo].[TasksToDo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [GoalDescription] NCHAR(200) NOT NULL, 
    [Category] INT NULL, 
    [DateDue] DATETIME NULL, 
    [LastViewed] DATETIME NOT NULL DEFAULT GETDATE(), 
	[Timesviewed] NCHAR(10) NULL,
    [Frequency] INT NOT NULL DEFAULT 1
)

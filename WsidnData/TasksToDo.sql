CREATE TABLE [dbo].[TasksToDo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [GoalDescription] NCHAR(200) NOT NULL, 
    [Category] INT NULL FOREIGN KEY (Category) REFERENCES Categories(Id), 
    [DateDue] DATETIME NULL, 
	[LastViewed] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateStart] DATETIME NOT NULL DEFAULT GETDATE(), 
	[Timesviewed] NCHAR(10) NULL,
    [IntervalByHour] INT NOT NULL DEFAULT 0
)

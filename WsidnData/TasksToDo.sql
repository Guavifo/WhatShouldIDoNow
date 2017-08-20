CREATE TABLE [dbo].[TasksToDo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [UserID] INT NOT NULL FOREIGN KEY (UserID) REFERENCES Users(Id) Default 1, --Remove Default Later
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [Description] NVARCHAR(200) NOT NULL, 
    [Category] INT NULL FOREIGN KEY (Category) REFERENCES Categories(Id), 
    [DateDue] DATETIME NULL, 
	[LastViewed] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateStart] DATETIME NOT NULL DEFAULT GETDATE(), 
	[TimesViewed] INT NOT NULL DEFAULT 1,
    [IntervalByHour] INT NOT NULL DEFAULT 0
  
)

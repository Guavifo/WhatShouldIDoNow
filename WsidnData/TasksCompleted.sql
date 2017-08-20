CREATE TABLE [dbo].[TasksCompleted]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
	[UserID] INT NOT NULL FOREIGN KEY (UserID) REFERENCES Users(Id) Default 1, --Remove Default Later
    [DateCreated] DATETIME NOT NULL , 
    [Description] NVARCHAR(200) NOT NULL, 
    [Category] INT NULL FOREIGN KEY (Category) REFERENCES Categories(Id), 
    [DateCompleted] DATETIME NOT NULL DEFAULT GETDATE(), 
)

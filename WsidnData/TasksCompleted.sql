CREATE TABLE [dbo].[TasksCompleted]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [DateCreated] DATETIME NOT NULL , 
    [GoalDescription] NCHAR(200) NOT NULL, 
    [Category] INT NULL FOREIGN KEY (Category) REFERENCES Categories(Id), 
    [DateCompleted] DATETIME NOT NULL, 
)

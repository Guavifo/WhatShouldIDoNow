CREATE TABLE [dbo].[Categories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Description] NCHAR(20) NOT NULL, 
    [IsArchived] BIT NOT NULL DEFAULT 0
)

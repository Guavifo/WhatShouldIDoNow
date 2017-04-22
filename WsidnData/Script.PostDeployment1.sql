/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT 1 FROM Categories WHERE Description = 'Technology')
BEGIN
	INSERT INTO Categories (Description)
	VALUES ('Technology')
END
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Description = 'Finances')
BEGIN
	INSERT INTO Categories (Description)
	VALUES ('Finances')
END
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Description = 'Education')
BEGIN
	INSERT INTO Categories (Description)
	VALUES ('Education')
END
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Description = 'Social')
BEGIN
	INSERT INTO Categories (Description)
	VALUES ('Social')
END

CREATE TABLE [dbo].[Bots](
	[Id] 		[uniqueidentifier] NOT NULL,
	[UserId] 	[uniqueidentifier] NOT NULL,
	[Name] 		[nvarchar](50) NOT NULL,
	[IsRunning] [bit] NOT NULL,
	PRIMARY KEY (Id),
	CONSTRAINT FK_UserBot FOREIGN KEY (UserId)
	REFERENCES dbo.Users (Id)
)
GO
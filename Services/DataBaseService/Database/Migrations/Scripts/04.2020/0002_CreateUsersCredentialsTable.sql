CREATE TABLE dbo.UsersCredentials (
    [Id]		    UNIQUEIDENTIFIER NOT NULL   UNIQUE,
	[UserId]        UNIQUEIDENTIFIER NOT NULL,
    [Email]		    NVARCHAR (50)    NOT NULL   UNIQUE CLUSTERED,
    [PasswordHash]  NVARCHAR (MAX)    NOT NULL,
    CONSTRAINT [PK_UsersCredentials] PRIMARY KEY ([Id] ASC)
)
GO
ALTER TABLE dbo.UsersCredentials ADD CONSTRAINT
	FK_UsersCredentials_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.Users
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION
GO
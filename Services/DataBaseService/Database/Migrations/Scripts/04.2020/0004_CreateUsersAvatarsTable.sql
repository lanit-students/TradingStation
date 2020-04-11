CREATE TABLE dbo.UsersAvatars (
	[Id] UNIQUEIDENTIFIER UNIQUE NOT NULL,
	Avatar VARBINARY(MAX) NOT NULL,
	TypeAvatar NVARCHAR(20) NOT NULL,
	CONSTRAINT [PK_UserAvatars] PRIMARY KEY ([Id] ASC)
)
GO
ALTER TABLE dbo.Users ADD UserAvatarId UNIQUEIDENTIFIER
GO
ALTER TABLE dbo.Users ADD CONSTRAINT
	FK_Users_UsersAvatars FOREIGN KEY 
	(
	UserAvatarId 
	) REFERENCES dbo.UsersAvatars 
	(
	Id
	) ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

INSERT INTO [dbo].[UsersAvatars]
           ([Id]
           ,[Avatar]
           ,[TypeAvatar])
     VALUES
           ('6FF619FF-8B86-D011-B42D-00C04FC964FF'
           ,0
           ,'')
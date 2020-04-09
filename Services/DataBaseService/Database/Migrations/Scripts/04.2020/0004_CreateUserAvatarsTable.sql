﻿CREATE TABLE dbo.UserAvatars (
	[Id] UNIQUEIDENTIFIER NOT NULL UNIQUE,
	Avatar VARBINARY(MAX) NOT NULL,
	TypeAvatar NVARCHAR(20) NOT NULL,
	CONSTRAINT [PK_UserAvatars] PRIMARY KEY ([Id] ASC)
)
GO
ALTER TABLE dbo.Users ADD UserAvatarId UNIQUEIDENTIFIER
GO
ALTER TABLE dbo.Users ADD CONSTRAINT
	FK_Users_UserAvatars FOREIGN KEY
	(
	UserAvatarId
	) REFERENCES dbo.UserAvatars
	(
	Id
	) ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO
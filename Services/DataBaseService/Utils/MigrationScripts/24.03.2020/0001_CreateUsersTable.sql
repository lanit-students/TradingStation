CREATE TABLE [UsersCredentials] (
    [Id]		    UNIQUEIDENTIFIER NOT NULL   UNIQUE,
    [Email]		    NVARCHAR (50)    NOT NULL   UNIQUE CLUSTERED,
    [PasswordHash]  NVARCHAR (40)    NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id] ASC)
);
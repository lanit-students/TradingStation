CREATE TABLE [UsersCredentials] (
    [Id]		    UNIQUEIDENTIFIER NOT NULL,
    [Email]		    NVARCHAR (30)    NOT NULL,
    [PasswordHash]  NVARCHAR (40)    NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);
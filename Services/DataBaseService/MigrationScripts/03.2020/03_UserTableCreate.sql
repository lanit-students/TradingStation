USE [TradingStation];

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users]

CREATE TABLE [dbo].[Users]
    (
        Id              uniqueidentifier    NOT NULL,
        Email           NVARCHAR(40)        NOT NULL,
        PasswordHash    NVARCHAR(MAX)       NOT NULL,
        FirstName       NVARCHAR(15)        NOT NULL,
        LastName        NVARCHAR(20)        NULL,
		IsActive        BIT                 NOT NULL,
        CONSTRAINT PKusers 
            PRIMARY KEY NONCLUSTERED (Id),
        CONSTRAINT UNQusersEmail 
            UNIQUE CLUSTERED (Email),
    );

ALTER TABLE [dbo].[Users]
	ADD CONSTRAINT DFTusersIsActive
	DEFAULT(1) FOR IsActive;
USE [TradingStation];

IF OBJECT_ID('dbo.ExecutedScripts', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExecutedScripts]

CREATE TABLE [dbo].[ExecutedScripts]
    (
        Id	    INT IDENTITY     NOT NULL,
		FileName    NVARCHAR(100)     NOT NULL,
        Code    NVARCHAR(MAX)    NOT NULL,
        CONSTRAINT PKscripts
            PRIMARY KEY CLUSTERED (Id),
    );	
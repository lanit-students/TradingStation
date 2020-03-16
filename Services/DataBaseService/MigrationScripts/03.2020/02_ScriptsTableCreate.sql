USE [TradingStation];

IF OBJECT_ID('dbo.Scripts', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scripts]

CREATE TABLE [dbo].[Scripts]
    (
        Id	    INT               NOT NULL,
        Code    NVARCHAR(1000)    NOT NULL,
        CONSTRAINT PKscripts
            PRIMARY KEY CLUSTERED (Id),
    );

ALTER TABLE [dbo].[Scripts]
	ADD CONSTRAINT CHKpositiveId
	CHECK(Id >= 0);
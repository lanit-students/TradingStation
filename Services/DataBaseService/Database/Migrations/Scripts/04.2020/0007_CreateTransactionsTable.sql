﻿CREATE TABLE dbo.Transactions (
	[Id] UNIQUEIDENTIFIER UNIQUE NOT NULL,
	[UserId]        UNIQUEIDENTIFIER NOT NULL,
	[Figi] VARCHAR(12) NOT NULL,
	[Broker] VARCHAR(50) NOT NULL,
	[TransactionDate] DATE	NOT NULL,
	[TransactionTime] TIME NOT NULL,
	[Operation] VARCHAR(4)  NOT NULL CHECK (Operation IN('Sell', 'Buy')),
	[Count] INT NOT NULL,
	[Price] DECIMAL NOT NULL,
	CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id] ASC),
	CONSTRAINT FK_Transactions_Users FOREIGN KEY (UserId) 
	REFERENCES dbo.Users (Id) 
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
)
GO
CREATE TABLE dbo.PortfolioInstruments (
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Figi] VARCHAR(12) NOT NULL,
	[Lots] INT NOT NULL,
	[InstrumentType] VARCHAR(8) NOT NULL CHECK (InstrumentType IN('Currency', 'Bond', 'Stock')),

	CONSTRAINT [PK_PortfolioInstruments] PRIMARY KEY ([UserId], [Figi] ASC),

	CONSTRAINT FK_PortfoliosInstruments_Users FOREIGN KEY (UserId) 
	REFERENCES dbo.Users (Id) 
	ON UPDATE NO ACTION
	ON DELETE NO ACTION,
)
GO
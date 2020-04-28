ALTER TABLE dbo.Transactions 
ADD IsSuccess BIT DEFAULT 1
GO

ALTER TABLE dbo.Transactions 
ADD Currency  VARCHAR(4)  NOT NULL CHECK (Currency IN('RUB', 'USD', 'EUR'))
GO
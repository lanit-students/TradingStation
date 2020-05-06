ALTER TABLE dbo.Transactions 
ADD IsSuccess BIT DEFAULT 1
GO

ALTER TABLE dbo.Transactions 
ADD Currency  VARCHAR(3)  NOT NULL CHECK (Currency IN('Rub', 'Usd', 'Eur'))
GO
USE [TradingStation];
GO

CREATE PROCEDURE [dbo].[GetMaxScriptId]   
AS 
BEGIN
	SET NOCOUNT ON;  
	DECLARE @Id INT;
	SET @Id = (SELECT max(Id) 
		FROM [dbo].[Scripts]);
	IF (@Id IS NULL)
		SET @Id = 0;
	SET NOCOUNT OFF;
	RETURN @Id;
END;
USE [TradingStation];
GO

CREATE PROCEDURE [dbo].[InsertNewScriptRow]
	@Code NVARCHAR(1000)
AS
BEGIN
	DECLARE @Id INT;
	EXEC @Id = [dbo].[GetMaxScriptId];
	SET @Id = @Id + 1;
	INSERT INTO [dbo].[Scripts]
		VALUES (@Id, @Code);
END;
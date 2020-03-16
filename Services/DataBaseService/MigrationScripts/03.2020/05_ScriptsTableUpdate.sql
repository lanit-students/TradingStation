USE [TradingStation];

BULK INSERT #TEXT FROM 'D:\Programming\C#\LanitTercom\TradingStation\Services\DataBaseService\MigrationScripts\03.2020\01_TradingStationDBCreate.sql';
/*You now have your bulk data*/

EXEC [dbo].[InsertNewScriptRow] @Code = #TEXT;

/*drop table #TEXT;*/
CREATE TABLE dbo.BotRules (
    [Id]				UNIQUEIDENTIFIER	NOT NULL   UNIQUE CLUSTERED,
	[OperationType]     integer				NOT NULL,
    [TimeMarker]		integer				NOT NULL,
    [TriggerValue]		integer				NOT NULL,
    CONSTRAINT [PK_BotRules] PRIMARY KEY ([Id] ASC)
)
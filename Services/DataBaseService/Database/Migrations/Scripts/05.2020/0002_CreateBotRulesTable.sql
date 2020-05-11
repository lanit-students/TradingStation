CREATE TABLE dbo.BotRules (
    [Id]				UNIQUEIDENTIFIER	NOT NULL   UNIQUE CLUSTERED,
	[OperationType]     integer				NOT NULL,
    [TriggerType]		integer				NOT NULL,
    [TriggerValue]		integer				NOT NULL,
    CONSTRAINT [PK_BotRules] PRIMARY KEY ([Id] ASC)
)
USE [LogsDataBase];
GO

CREATE TABLE [dbo].[_Logs](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[ServiceName] [nvarchar](50) NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
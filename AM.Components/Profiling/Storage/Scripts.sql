USE [CorpNetProfiler]
GO
/****** Object:  Table [CW].[Profiling]    Script Date: 10/07/2014 06:57:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[Profiling]') AND type in (N'U'))
DROP TABLE [CW].[Profiling]
GO
/****** Object:  Table [CW].[ProfilingConfig]    Script Date: 10/07/2014 06:57:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[ProfilingConfig]') AND type in (N'U'))
DROP TABLE [CW].[ProfilingConfig]
GO
/****** Object:  Table [CW].[ProfilingFilter]    Script Date: 10/07/2014 06:57:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[ProfilingFilter]') AND type in (N'U'))
DROP TABLE [CW].[ProfilingFilter]
GO
/****** Object:  Table [CW].[ProfilingTiming]    Script Date: 10/07/2014 06:57:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[ProfilingTiming]') AND type in (N'U'))
DROP TABLE [CW].[ProfilingTiming]
GO
/****** Object:  Table [CW].[ProfilingTiming]    Script Date: 10/07/2014 06:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[ProfilingTiming]') AND type in (N'U'))
BEGIN
CREATE TABLE [CW].[ProfilingTiming](
	[TimingId] [int] IDENTITY(1,1) NOT NULL,
	[ProfilingId] [int] NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[SQL] [varchar](max) NULL,
	[StartOffset] [decimal](7, 1) NOT NULL,
	[Duration] [decimal](7, 1) NOT NULL,
	[DataSize] [bigint] NULL,
	[DataRowCount] [int] NULL,
 CONSTRAINT [PK_ProfilingTimings] PRIMARY KEY CLUSTERED 
(
	[TimingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CW].[ProfilingFilter]    Script Date: 10/07/2014 06:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[ProfilingFilter]') AND type in (N'U'))
BEGIN
CREATE TABLE [CW].[ProfilingFilter](
	[ProfilingFilterId] [int] IDENTITY(1,1) NOT NULL,
	[UrlString] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ProfilingFilter] PRIMARY KEY CLUSTERED 
(
	[ProfilingFilterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CW].[ProfilingConfig]    Script Date: 10/07/2014 06:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[ProfilingConfig]') AND type in (N'U'))
BEGIN
CREATE TABLE [CW].[ProfilingConfig](
	[Enabled] [bit] NULL,
	[RunOnStartup] [bit] NULL,
	[RunScheduleCron] [varchar](128) NULL,
	[RunDurationMins] [int] NULL,
	[StorageQueueInterval] [int] NULL,
	[StorageResumeMins] [int] NULL,
	[ActiveUserLocator] [varchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [CW].[Profiling]    Script Date: 10/07/2014 06:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CW].[Profiling]') AND type in (N'U'))
BEGIN
CREATE TABLE [Profiling](
	[ProfilingId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[Url] [varchar](256) NOT NULL,
	[Referer] [varchar](256) NULL,
	[Action] [varchar](10) NULL,
	[ClientIP] [varchar](15) NULL,
	[ClientAgent] [varchar](128) NULL,
	[ActiveUser] [varchar](30) NULL,
	[MachineName] [nvarchar](100) NULL,
	[Started] [datetime] NOT NULL,
	[Duration] [decimal](7, 1) NOT NULL,
	[RequestID] [uniqueidentifier] NULL,
	[Event] [varchar](128) NULL,
	[ResponseSize] [int] NULL,
	[RequestSize] [int] NULL,
	[Exception] [varchar](512) NULL,
	[SessionID] [varchar](24) NULL,
 CONSTRAINT [PK_Profiling] PRIMARY KEY CLUSTERED 
(
	[ProfilingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO


-- Add default config (enabled, but don't run on startup)
DELETE [CW].ProfilingConfig
GO

INSERT INTO [CW].ProfilingConfig (
	Enabled,
	RunOnStartup,
	RunScheduleCron,
	RunDurationMins,
	StorageQueueInterval,
	StorageResumeMins,
	ActiveUserLocator,
	LogFileName)
SELECT
	Enabled = 1,
	RunOnStartup = 0,
	RunScheduleCron = '',	
	RunDurationMins = 0,
	StorageQueueInterval = 60000,
	StorageResumeMins = 1,
	ActiveUserLocator = 'ActiveUser.Login',
	LogFileName = ''
GO

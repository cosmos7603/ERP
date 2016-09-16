DROP TYPE [GuestCommList]

CREATE TYPE [dbo].[GuestCommList] AS TABLE(
	[CustomerId] [int] NULL,
	[GuestCommTypeId] [int] NULL,
	[OptInTypeCode] [char](30) NULL
)
GO

DROP TYPE [dbo].[GuestCommListTable]

CREATE TYPE [dbo].[GuestCommListTable] AS TABLE(
	[GuestCommId] [int] NULL
)
GO

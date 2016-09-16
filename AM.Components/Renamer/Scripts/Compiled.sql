SET NOCOUNT ON

-- Drop computed columns
EXEC z_ShowMsg 'Drop computed columns'

DROP INDEX Customer.IX_Customer_household_index
GO

ALTER TABLE Promotion DROP COLUMN promotion_code
ALTER TABLE Transactions_Hst DROP COLUMN trans_conv_amt
ALTER TABLE Acct_Res DROP COLUMN store_price_amt
ALTER TABLE Acct_Pass DROP COLUMN store_price_amt
ALTER TABLE Grp_Line_Item_Adj DROP COLUMN comm_adj_amt
ALTER TABLE Grp_Line_Item_Adj DROP COLUMN profit_adj_amt
ALTER TABLE Acct_Pass_Vend DROP COLUMN vendor_price_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN ps_markup_dsct_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN ps_comm_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN ps_vendor_cost_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN pd1_markup_dsct_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN pd1_comm_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN pd1_vendor_cost_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN pd2_markup_dsct_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN pd2_comm_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN pd2_vendor_cost_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN p34_markup_dsct_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN p34_comm_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN p34_vendor_cost_amt
ALTER TABLE Grp_Line_Pricing DROP COLUMN free_qty
ALTER TABLE Transactions DROP COLUMN pay_trans_amt
ALTER TABLE Transactions DROP COLUMN trans_conv_amt
ALTER TABLE Customer DROP COLUMN avg_trip_amt
ALTER TABLE Customer DROP COLUMN household_index
ALTER TABLE Customer DROP COLUMN household_key
ALTER TABLE Users DROP COLUMN full_name
ALTER TABLE Acct_Res_Vend DROP COLUMN vendor_price_amt
ALTER TABLE Line_Item_Adj DROP COLUMN comm_adj_amt
ALTER TABLE Line_Item_Adj DROP COLUMN profit_adj_amt
ALTER TABLE Line_Pass DROP COLUMN markup_dsct_amt
ALTER TABLE Line_Pass DROP COLUMN comm_amt
ALTER TABLE Line_Pass DROP COLUMN vendor_cost_amt
ALTER TABLE Line_Pass DROP COLUMN profit_amt
GO

-- Drop check constraints
EXEC z_ShowMsg 'Drop check constraints'

ALTER TABLE Interest DROP CONSTRAINT CK_Interest_Level
ALTER TABLE Report_Email_Execution DROP CONSTRAINT CK_Report_Email_Execution
ALTER TABLE Reservation DROP CONSTRAINT CK_Reservation
ALTER TABLE Email DROP CONSTRAINT CK_Email_StatusCode
ALTER TABLE CE_Activity DROP CONSTRAINT CK_CE_Activity_Type
ALTER TABLE CE_Activity DROP CONSTRAINT CK_CE_Activity_Level
ALTER TABLE CE_Activity_Event DROP CONSTRAINT CK_CE_Activity_Event
ALTER TABLE Appointment DROP CONSTRAINT CK_CeCustomer
ALTER TABLE Mktg_Src DROP CONSTRAINT CK_Mktg_Src_Level
GO

-- Schema renamings
EXEC z_ShowMsg 'Schema renamings'

DECLARE @Error varchar(max)
DECLARE @TableName varchar(256)
DECLARE @ColumnName varchar(256)
DECLARE @OldName varchar(256)
DECLARE @NewName varchar(256)
DECLARE @SQL varchar(max)


-- Update Query Columns
EXEC z_ShowMsg 'Update query columns...'

DECLARE RC CURSOR FOR
SELECT DISTINCT query_column_name FROM Query_Column

OPEN RC
FETCH NEXT FROM RC INTO @ColumnName

WHILE @@FETCH_STATUS = 0   
BEGIN
	SET @NewName = NULL

	SELECT @NewName = [NewName]
	FROM CruiseWeb_Rename..Renaming R
	WHERE R.OldName = @ColumnName

	IF (@NewName IS NULL)
		SET @NewName = CruiseWeb_Rename.dbo.CapitalizeString(@ColumnName)

	SELECT @SQL = 'UPDATE Query_Column SET query_column_name = ''' + @NewName + ''' WHERE query_column_name = ''' + @ColumnName + ''''
	EXECUTE(@SQL)

	FETCH NEXT FROM RC INTO @ColumnName
END   

CLOSE RC
DEALLOCATE RC


-- Update Query Filter Fields
EXEC z_ShowMsg 'Update Query Filter Fields...'

DECLARE RC CURSOR FOR
SELECT DISTINCT  field_name FROM Query_Filter_Field

OPEN RC
FETCH NEXT FROM RC INTO @ColumnName

WHILE @@FETCH_STATUS = 0   
BEGIN   
	SET @NewName = NULL

	SELECT @NewName = [NewName]
	FROM CruiseWeb_Rename..Renaming R
	WHERE R.OldName = @ColumnName

	IF (@NewName IS NULL)
		SET @NewName = CruiseWeb_Rename.dbo.CapitalizeString(@ColumnName)

	SELECT @SQL = 'UPDATE Query_Filter_Field SET field_name = ''' + @NewName + ''' WHERE field_name = ''' + @ColumnName + ''''
	EXECUTE(@SQL)

	FETCH NEXT FROM RC INTO @ColumnName
END   

CLOSE RC
DEALLOCATE RC

-- Rename fields
DECLARE @Error varchar(max)
DECLARE @TableName varchar(256)
DECLARE @ColumnName varchar(256)
DECLARE @OldName varchar(256)
DECLARE @NewName varchar(256)
DECLARE @SQL varchar(max)


DECLARE RC CURSOR FOR 
SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE
	TABLE_SCHEMA = 'dbo'
	
	-- Exclusions
	AND LEFT(TABLE_NAME, 3) != 'CB_'
	AND TABLE_NAME NOT LIKE 'ELMAH_%'
	AND TABLE_NAME NOT LIKE 'dtproperties'

OPEN RC
FETCH NEXT FROM RC INTO @TableName, @ColumnName

WHILE @@FETCH_STATUS = 0   
BEGIN   
	SET @NewName = NULL

	SELECT @NewName = [NewName]
	FROM CruiseWeb_Rename..Renaming R
	WHERE R.OldName = @ColumnName

	IF (@NewName IS NULL)
		SET @NewName = CruiseWeb_Rename.dbo.CapitalizeString(@ColumnName)

	IF (@NewName IS NOT NULL AND @NewName != @ColumnName)
	BEGIN
		SELECT @SQL = 'EXEC sp_rename ''[' + @TableName + '].[' + @ColumnName + ']'', ''' + @NewName + ''', ''COLUMN'''
		PRINT @SQL
		EXECUTE(@SQL)
	END

	FETCH NEXT FROM RC INTO @TableName, @ColumnName
END   

CLOSE RC
DEALLOCATE RC

-- Renamge tables
DECLARE RC CURSOR FOR
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE
	TABLE_SCHEMA = 'dbo'
	
	-- Has underscore
	AND CHARINDEX('_', [TABLE_NAME]) > 0

	-- Exclusions
	AND TABLE_NAME NOT LIKE 'ELMAH_%'
	AND TABLE_NAME NOT LIKE 'dtproperties'

OPEN RC
FETCH NEXT FROM RC INTO @TableName

WHILE @@FETCH_STATUS = 0   
BEGIN   
	SET @NewName = NULL

	SELECT @NewName = [NewName]
	FROM CruiseWeb_Rename..Renaming R
	WHERE R.OldName = @TableName

	IF (@NewName IS NOT NULL AND @NewName != @TableName)
	BEGIN
		SELECT @SQL = 'EXEC sp_rename ''[' + @TableName + ']'', ''' + @NewName + ''''
		PRINT @SQL
		EXECUTE(@SQL)
	END

	FETCH NEXT FROM RC INTO @TableName
END   

CLOSE RC
DEALLOCATE RC
GO

-- Recreate computed columns
EXEC z_ShowMsg 'Recreate computed columns...'

ALTER TABLE Promotion ADD PromotionCode AS (([prefix]+CONVERT([varchar](10),[PromotionId],0)))
ALTER TABLE TransactionsHst ADD TransConvAmt AS ((convert(money,case when ([CurrencyConvCode] <> [CurrencyCode]) then ([TransAmt] * [ConvRate]) else [TransAmt] end)))
ALTER TABLE AcctRes ADD StorePriceAmt AS ((convert(money,([StoreAmt] + [FeesTaxesAmt]))))
ALTER TABLE AcctPass ADD StorePriceAmt AS ((CONVERT([money],[StoreAmt]+[FeesTaxesAmt],0)))
ALTER TABLE GrpLineItemAdj ADD CommAdjAmt AS ((convert(money,([StoreAdjAmt] - [VendorAdjAmt] - [MarkupAdjAmt]))))
ALTER TABLE GrpLineItemAdj ADD ProfitAdjAmt AS ((convert(money,([StoreAdjAmt] - [VendorAdjAmt] + [OtherCommAdjAmt]))))
ALTER TABLE AcctPassVend ADD VendorPriceAmt AS ((CONVERT([money],([VendorAmt]+[FeesTaxesAmt])-[CommAmt],0)))
ALTER TABLE GrpLinePricing ADD PsMarkupDsctAmt AS ((CONVERT([money],[PsStoreAmt]-[PsVendorAmt],(0))))
ALTER TABLE GrpLinePricing ADD PsCommAmt AS (([dbo].[TruncateAmount](CONVERT([money],[PsVendorAmt]*([PsCommPrct]/(100)),(0)))))
ALTER TABLE GrpLinePricing ADD PsVendorCostAmt AS (([dbo].[TruncateAmount](CONVERT([money],[PsVendorAmt]-[dbo].[TruncateAmount](CONVERT([money],[PsVendorAmt]*([PsCommPrct]/(100)),(0))),(0)))))
ALTER TABLE GrpLinePricing ADD Pd1MarkupDsctAmt AS ((CONVERT([money],[Pd1StoreAmt]-[Pd1VendorAmt],(0))))
ALTER TABLE GrpLinePricing ADD Pd1CommAmt AS (([dbo].[TruncateAmount](CONVERT([money],[Pd1VendorAmt]*([Pd1CommPrct]/(100)),(0)))))
ALTER TABLE GrpLinePricing ADD Pd1VendorCostAmt AS (([dbo].[TruncateAmount](CONVERT([money],[Pd1VendorAmt]-[dbo].[TruncateAmount](CONVERT([money],[Pd1VendorAmt]*([Pd1CommPrct]/(100)),(0))),(0)))))
ALTER TABLE GrpLinePricing ADD Pd2MarkupDsctAmt AS ((CONVERT([money],[Pd2StoreAmt]-[Pd2VendorAmt],(0))))
ALTER TABLE GrpLinePricing ADD Pd2CommAmt AS (([dbo].[TruncateAmount](CONVERT([money],[Pd2VendorAmt]*([Pd2CommPrct]/(100)),(0)))))
ALTER TABLE GrpLinePricing ADD Pd2VendorCostAmt AS (([dbo].[TruncateAmount](CONVERT([money],[Pd2VendorAmt]-[dbo].[TruncateAmount](CONVERT([money],[Pd2VendorAmt]*([Pd2CommPrct]/(100)),(0))),(0)))))
ALTER TABLE GrpLinePricing ADD P34MarkupDsctAmt AS ((CONVERT([money],[P34StoreAmt]-[P34VendorAmt],(0))))
ALTER TABLE GrpLinePricing ADD P34CommAmt AS (([dbo].[TruncateAmount](CONVERT([money],[P34VendorAmt]*([P34CommPrct]/(100)),(0)))))
ALTER TABLE GrpLinePricing ADD P34VendorCostAmt AS (([dbo].[TruncateAmount](CONVERT([money],[P34VendorAmt]-[dbo].[TruncateAmount](CONVERT([money],[P34VendorAmt]*([P34CommPrct]/(100)),(0))),(0)))))
ALTER TABLE GrpLinePricing ADD FreeQty AS (([TotalQty]-[AllocQty]))
ALTER TABLE Transactions ADD PayTransAmt AS ((convert(money,convert(decimal(15,2),case when (isnull([GstConvAmt],0) > 0 and ([TransTypeCode] = 'DISB_VEND')) then ([TransAmt] - [GstConvAmt] / [ConvRate]) when (isnull([GstConvAmt],0) > 0 and ([TransTypeCode] = 'RECV_VEND')) then ([TransAmt] + [GstConvAmt] / [ConvRate]) else [TransAmt] end))))
ALTER TABLE Transactions ADD TransConvAmt AS ((convert(money,convert(decimal(15,2),([TransAmt] * [ConvRate])))))
ALTER TABLE Customer ADD HouseholdIndex AS ((checksum([StoreId],[PriAddress1],[PriAddress2],[PriAddress3],[PriCity],[PriStateName],[PriZip],[PriCountryCode])))
ALTER TABLE Customer ADD HouseholdKey AS ((case when [PriAddress1]='' OR ([StatusCode]='SYSTEM' OR [StatusCode]='DELETED') then '' else replace(upper(((((((CONVERT([varchar],[StoreId],(0))+[PriAddress1])+[PriAddress2])+[PriAddress3])+[PriCity])+[PriStateName])+[PriZip])+[PriCountryCode]),'''','') end))
ALTER TABLE Customer ADD AvgTripAmt AS (convert(money,case when (isnull([NumSailedCruises],0) > 0) then ([TotalSailedAmt] / [NumSailedCruises]) else 0 end))
ALTER TABLE Users ADD FullName AS (([FirstName] + ' ' + [LastName]))
ALTER TABLE AcctResVend ADD VendorPriceAmt AS ((convert(money,([VendorAmt] + [FeesTaxesAmt] - [CommAmt]))))
ALTER TABLE LineItemAdj ADD CommAdjAmt AS ((convert(money,([StoreAdjAmt] - [VendorAdjAmt] - [MarkupAdjAmt]))))
ALTER TABLE LineItemAdj ADD ProfitAdjAmt AS ((convert(money,([StoreAdjAmt] - [VendorAdjAmt] + [OtherCommAdjAmt]))))
ALTER TABLE LinePass ADD MarkupDsctAmt AS ((convert(money,([StoreAmt] - [VendorAmt]))))
ALTER TABLE LinePass ADD CommAmt AS (([dbo].[TruncateAmount](CONVERT([money],[VendorAmt]*([CommPrct]/(100)),0))))
ALTER TABLE LinePass ADD VendorCostAmt AS (([dbo].[TruncateAmount](CONVERT([money],[VendorAmt]-[dbo].[TruncateAmount](CONVERT([money],[VendorAmt]*([CommPrct]/(100)),0)),0))))
ALTER TABLE LinePass ADD ProfitAmt AS (([dbo].[TruncateAmount](CONVERT([money],[StoreAmt]-([VendorAmt]-([dbo].[TruncateAmount](CONVERT([money],[VendorAmt]*([CommPrct]/(100)),0))+isnull([OtherCommAmt],(0)))),0))))
GO

CREATE NONCLUSTERED INDEX [IX_Customer_HouseholdIndex] ON [dbo].[Customer]
(
	[HouseholdIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO

-- Recreate check constraints
EXEC z_ShowMsg 'Adding check constraints back...'

ALTER TABLE Interest ADD CONSTRAINT CK_InterestLevel CHECK ([InterestLevelCode]='USER' OR [InterestLevelCode]='STORE' OR [InterestLevelCode]='HO' OR [InterestLevelCode]='CORP')
ALTER TABLE ReportEmailExecution ADD CONSTRAINT CK_ReportEmailExecution CHECK ([ReportStatusCode]='ERROR' OR [ReportStatusCode]='OK')
ALTER TABLE Reservation ADD CONSTRAINT CK_Reservation CHECK ([CustDpstAmt] = 0 or [CustDpstCurr] is not null)
ALTER TABLE Email ADD CONSTRAINT CK_EmailStatusCode CHECK ([StatusCode]='QUEUED' OR [StatusCode]='ERROR' OR [StatusCode]='SENT')
ALTER TABLE CEActivity ADD CONSTRAINT CK_CEActivityType CHECK ([CeActivityTypeCode]='PRINT' OR [CeActivityTypeCode]='EMAIL' OR [CeActivityTypeCode]='SHORE')
ALTER TABLE CEActivity ADD CONSTRAINT CK_CEActivityLevel CHECK ([CeActivityLevelCode]='CLI' OR [CeActivityLevelCode]='RES')
ALTER TABLE CEActivityEvent ADD CONSTRAINT CK_CEActivityEvent CHECK ([CeActivityLevelCode]='CLI' OR [CeActivityLevelCode]='RES')
ALTER TABLE Appointment ADD CONSTRAINT CK_CECustomer CHECK (NOT ([AppointmentTypeCode]='CEX' AND [CustomerId] IS NULL))
ALTER TABLE MktgSrc ADD CONSTRAINT CK_MktgSrcLevel CHECK ([MktgSrcLevelCode]='USER' OR [MktgSrcLevelCode]='STORE' OR [MktgSrcLevelCode]='HO' OR [MktgSrcLevelCode]='CORP')
GO


-- Drop procedures
EXEC z_ShowMsg 'Drop procedures...'
DECLARE @SQL varchar(max)

DECLARE RC CURSOR FOR
SELECT DropSQL = 'DROP PROCEDURE [' + name + ']'
FROM sys.procedures
WHERE [type] = 'P' AND is_ms_shipped = 0 AND name NOT LIKE 'z%' AND name NOT LIKE 'sp%' AND name NOT LIKE 'ELMAH%'
ORDER BY name

OPEN RC
FETCH NEXT FROM RC INTO @SQL

WHILE @@FETCH_STATUS = 0   
BEGIN   
	EXECUTE(@SQL)
	FETCH NEXT FROM RC INTO @SQL
END   

CLOSE RC
DEALLOCATE RC
GO


-- USER TYPES
EXEC z_ShowMsg 'User Types...'
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




-- STANDARIZATION
EXEC z_ShowMsg 'Standarization'

DECLARE @SQL varchar(max)

DECLARE RC CURSOR FOR

SELECT
	ChangeSQL = 'sp_rename ''' + s.name + '.' + dc.name +'''' + ', ''' + 'DF_' + t.name + '_' + c.name + ''';'
FROM
	sys.default_constraints dc
	INNER JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
	INNER JOIN sys.tables t ON t.object_id = c.object_id
	INNER JOIN sys.schemas s ON s.schema_id = t.schema_id
WHERE
	dc.name <> 'DF_' + t.name + '_' + c.name
ORDER BY
	t.name,c.name

OPEN RC
FETCH NEXT FROM RC INTO @SQL

WHILE @@FETCH_STATUS = 0   
BEGIN
	PRINT @SQL
	EXECUTE(@SQL)
	FETCH NEXT FROM RC INTO @SQL
END   

CLOSE RC
DEALLOCATE RC

/*
EXECUTE THIS, BUT YOU KNOW IT WILL THROW SOME ERRORS

DECLARE RC CURSOR FOR
SELECT
	--ChangeSQL =  'sp_rename ''' + s.name + '.' + o.name + '''' + ',''' + 'FK_' + t1.name + '_' + c1.name + '_' + t2.name + '_' + c2.name + ''';'
	ChangeSQL =  'sp_rename ''' + s.name + '.' + o.name + '''' + ',''' + 'FK_' + t1.name + '_' + t2.name + ''';'
FROM
	sys.foreign_key_columns fkc
	INNER JOIN sys.objects o ON o.object_id = fkc.constraint_object_id
	INNER JOIN sys.tables t1 ON t1.object_id = fkc.parent_object_id
	INNER JOIN sys.schemas s ON t1.schema_id = s.schema_id
	INNER JOIN sys.columns c1 ON c1.column_id = fkc.parent_column_id AND c1.object_id = t1.object_id
	INNER JOIN sys.tables t2 ON t2.object_id = fkc.referenced_object_id
	INNER JOIN sys.columns c2 ON c2.column_id = fkc.referenced_column_id AND c2.object_id = t2.object_id
WHERE
	o.name <> 'FK_' + t1.name + '_' + t2.name
	AND o.name != 'sysdiagrams'

OPEN RC
FETCH NEXT FROM RC INTO @SQL

WHILE @@FETCH_STATUS = 0   
BEGIN
	PRINT @SQL
	EXECUTE(@SQL)
	FETCH NEXT FROM RC INTO @SQL
END   

CLOSE RC
DEALLOCATE RC*/

/*
DECLARE RC CURSOR FOR
SELECT
	ChangeSQL = 'sp_rename ''' + s.name + '.' + kc.name + '''' + ',''' + 'PK_' + t.name + ''';'
FROM
	sys.key_constraints kc
	INNER JOIN sys.objects o ON o.object_id = kc.object_id
	INNER JOIN sys.tables t ON t.object_id = kc.parent_object_id
	INNER JOIN sys.schemas s ON s.schema_id = kc.schema_id
WHERE
	kc.name <> 'PK_' + t.name


OPEN RC
FETCH NEXT FROM RC INTO @SQL

WHILE @@FETCH_STATUS = 0   
BEGIN
	PRINT @SQL
	EXECUTE(@SQL)
	FETCH NEXT FROM RC INTO @SQL
END   

CLOSE RC
DEALLOCATE RC

*/
GO

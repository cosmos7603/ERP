-- Get Computed Columns List
SELECT
	TableName = T.name,
	ColumnName = C.name,
	Formula = C.[definition]
INTO
	#ComputedColumns
FROM
	sys.computed_columns C INNER JOIN sys.tables T ON T.object_id = C.object_id

-- Drop
--SELECT 'ALTER TABLE ' + TableName + ' DROP COLUMN ' + ColumnName FROM #ComputedColumns
DROP INDEX Customer.IX_Customer_household_index

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


-- Recreate
--SELECT 'ALTER TABLE ' + TableName + ' ADD ' + ColumnName + ' AS (' + Formula + ')' FROM #ComputedColumns

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
ALTER TABLE Users ADD FullName AS (([FirstName] + ' ' + [LastName]))
ALTER TABLE AcctResVend ADD VendorPriceAmt AS ((convert(money,([VendorAmt] + [FeesTaxesAmt] - [CommAmt]))))
ALTER TABLE LineItemAdj ADD CommAdjAmt AS ((convert(money,([StoreAdjAmt] - [VendorAdjAmt] - [MarkupAdjAmt]))))
ALTER TABLE LineItemAdj ADD ProfitAdjAmt AS ((convert(money,([StoreAdjAmt] - [VendorAdjAmt] + [OtherCommAdjAmt]))))
ALTER TABLE LinePass ADD MarkupDsctAmt AS ((convert(money,([StoreAmt] - [VendorAmt]))))
ALTER TABLE LinePass ADD CommAmt AS (([dbo].[TruncateAmount](CONVERT([money],[VendorAmt]*([CommPrct]/(100)),0))))
ALTER TABLE LinePass ADD VendorCostAmt AS (([dbo].[TruncateAmount](CONVERT([money],[VendorAmt]-[dbo].[TruncateAmount](CONVERT([money],[VendorAmt]*([CommPrct]/(100)),0)),0))))
ALTER TABLE LinePass ADD ProfitAmt AS (([dbo].[TruncateAmount](CONVERT([money],[StoreAmt]-([VendorAmt]-([dbo].[TruncateAmount](CONVERT([money],[VendorAmt]*([CommPrct]/(100)),0))+isnull([OtherCommAmt],(0)))),0))))
GO

-- Execute after recreating columns
CREATE NONCLUSTERED INDEX [IX_Customer_HouseholdIndex] ON [dbo].[Customer]
(
	[HouseholdIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO


-- Get Computed Columns List
SELECT
	TableName = T.name,
	CheckName = C.name,
	Formula = C.[definition]
INTO
	#CheckColumns
FROM
	sys.check_constraints C INNER JOIN sys.tables T ON T.object_id = C.parent_object_id

-- Drop
--SELECT 'ALTER TABLE ' + TableName + ' DROP CONSTRAINT ' + CheckName FROM #CheckColumns
ALTER TABLE Interest DROP CONSTRAINT CK_Interest_Level
ALTER TABLE Report_Email_Execution DROP CONSTRAINT CK_Report_Email_Execution
ALTER TABLE Reservation DROP CONSTRAINT CK_Reservation
ALTER TABLE Email DROP CONSTRAINT CK_Email_StatusCode
ALTER TABLE CE_Activity DROP CONSTRAINT CK_CE_Activity_Type
ALTER TABLE CE_Activity DROP CONSTRAINT CK_CE_Activity_Level
ALTER TABLE CE_Activity_Event DROP CONSTRAINT CK_CE_Activity_Event
ALTER TABLE Appointment DROP CONSTRAINT CK_CeCustomer
ALTER TABLE Mktg_Src DROP CONSTRAINT CK_Mktg_Src_Level

-- Recreate
--SELECT 'ALTER TABLE ' + TableName + ' ADD CONSTRAINT ' + CheckName + ' AS (' + Formula + ')' FROM #CheckColumns

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

DROP TABLE #Tables
DROP TABLE #Columns

-- Rename tables
SELECT
	OldName = TABLE_NAME,
	NewName = CruiseWeb_Rename.dbo.PascalizeString(TABLE_NAME)
INTO
	#Tables
FROM
	INFORMATION_SCHEMA.TABLES
WHERE
	TABLE_SCHEMA = 'dbo'
	
	-- Has underscore
	AND CHARINDEX('_', [TABLE_NAME]) > 0

	-- Exclusions
	AND TABLE_NAME NOT LIKE 'ELMAH_%'
ORDER BY
	[TABLE_NAME]
	

-- Rename columns
SELECT DISTINCT
	OldName = COLUMN_NAME,
	NewName = CruiseWeb_Rename.dbo.PascalizeString(COLUMN_NAME)
INTO
	#Columns
FROM
	INFORMATION_SCHEMA.COLUMNS
WHERE
	TABLE_SCHEMA = 'dbo'
	
	-- Has underscore
	AND CHARINDEX('_', [COLUMN_NAME]) > 0

	-- Exclusions
	AND LEFT(TABLE_NAME, 3) != 'CB_'
	AND TABLE_NAME NOT LIKE 'WinCruise_%'
	AND TABLE_NAME NOT LIKE 'FileMaker_%'
	AND TABLE_NAME NOT LIKE 'IV_%'
	AND TABLE_NAME NOT LIKE 'ELMAH_%'
	AND TABLE_NAME NOT LIKE 'dtproperties'


-- Add renamings
INSERT INTO CruiseWeb_Rename.dbo.Renaming (OldName, NewName, Manual)
SELECT
	OldName, NewName, Manual
FROM (
	SELECT DISTINCT OldName, NewName, Manual = 0 FROM #Tables
	UNION SELECT DISTINCT OldName, NewName, 0 FROM #Columns) AS T
WHERE
	NOT EXISTS (SELECT 1 FROM CruiseWeb_Rename.dbo.Renaming R2 WHERE R2.OldName = T.OldName)
ORDER BY
	OldName

-- Add from query
INSERT INTO CruiseWeb_Rename.dbo.Renaming (OldName, NewName, Manual)
SELECT DISTINCT query_column_name, dbo.PascalizeString(query_column_name), 0 FROM CruiseWeb..Query_Column
WHERE
	NOT EXISTS (
		SELECT 1
		FROM CruiseWeb_Rename.dbo.Renaming R2
		WHERE R2.OldName = query_column_name)

INSERT INTO CruiseWeb_Rename.dbo.Renaming (OldName, NewName, Manual)
SELECT DISTINCT field_name, CruiseWeb_Rename.dbo.PascalizeString(field_name), 0 FROM CruiseWeb..Query_Filter_Field
WHERE
	NOT EXISTS (
		SELECT 1
		FROM CruiseWeb_Rename.dbo.Renaming R2
		WHERE R2.OldName = field_name)

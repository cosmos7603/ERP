-- Rename columns
SELECT
	*
FROM
	CruiseWeb_Rename.dbo.Renaming R
ORDER BY
	LEN(OldName) DESC

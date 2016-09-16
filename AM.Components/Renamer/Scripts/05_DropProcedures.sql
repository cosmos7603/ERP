SELECT
	'DROP PROCEDURE [' + name + ']'
FROM
	sys.procedures
WHERE
	[type] = 'P'
	AND is_ms_shipped = 0
	AND name NOT LIKE 'z%'
	AND name NOT LIKE 'sp%'
	AND name NOT LIKE 'ELMAH%'
ORDER BY
	 name

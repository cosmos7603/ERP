/*
//Generates a list of all default constraints with their table and referenced column. 
//Creates a rename script to standardize constraint name to DF_TableName_ColumnName
//
//If there are incorrectly named constraints that already use the table name and different column name, run twice.
*/

SELECT s.name AS [SchemaName],
	   t.name AS [TableName],
	   c.name AS [ColumnName],
	   dc.name AS [ContraintName],
	   'sp_rename ''' + s.name + '.' + dc.name +'''' + ', ''' + 'DF_' + t.name + '_' + c.name + ''';' + CHAR(10) + 'GO' AS ChangeTSQL
	FROM sys.default_constraints dc
		INNER JOIN sys.columns c
			ON dc.parent_object_id = c.object_id
		   AND dc.parent_column_id = c.column_id
		INNER JOIN sys.tables t
			ON t.object_id = c.object_id
		INNER JOIN sys.schemas s
			ON s.schema_id = t.schema_id
	WHERE dc.name <> 'DF_' + t.name + '_' + c.name
	ORDER BY t.name,c.name
	
/*
//Generates a list of all the foreign keys with their source table and column, and referenced table and column
//Creates a rename script to standardize foreign key name to FK_SourceTable_SourceColumn_ReferencedTable_ReferencedColumn
//
//If there is a foreign key that references two or more columns, they will be duplicated in this list and take only the name of the first referenced column
*/	

SELECT o.name AS [FKName],
	   s.name AS [SchemaName],
	   t1.name AS [TableName],
	   c1.name AS [ColumnName],
	   t2.name AS [RefTableName],
	   c2.name AS [RefColName],
	   'sp_rename ''' + s.name + '.' + o.name + '''' + ',''' + 'FK_' + t1.name + '_' + c1.name + '_' + t2.name + '_' + c2.name + ''';' + CHAR(10) + 'GO' AS ChangeTSQL

	FROM sys.foreign_key_columns fkc
		INNER JOIN sys.objects o
			ON o.object_id = fkc.constraint_object_id
		INNER JOIN sys.tables t1
			ON t1.object_id = fkc.parent_object_id
		INNER JOIN sys.schemas s
			ON t1.schema_id = s.schema_id
		INNER JOIN sys.columns c1
			ON c1.column_id = fkc.parent_column_id
		   AND c1.object_id = t1.object_id
		INNER JOIN sys.tables t2
			ON t2.object_id = fkc.referenced_object_id
		INNER JOIN sys.columns c2
			ON c2.column_id = fkc.referenced_column_id
		   AND c2.object_id = t2.object_id
	WHERE o.name <> 'FK_' + t1.name + '_' + c1.name + '_' + t2.name + '_' + c2.name
/*
//Generates a list of all the primary keys with their source table.
//Creates a rename script to standardize primary key name to PK_SourceTable
*/

SELECT kc.name,
	   s.name,
	   t.name,
	   'sp_rename ''' + s.name + '.' + kc.name + '''' + ',''' + 'PK_' + t.name + ''';' + CHAR(10) + 'GO' AS ChangeTSQL
	    FROM sys.key_constraints kc
	INNER JOIN sys.objects o
		ON o.object_id = kc.object_id
	INNER JOIN sys.tables t
		ON t.object_id = kc.parent_object_id
	INNER JOIN sys.schemas s
		ON s.schema_id = kc.schema_id
	WHERE kc.name <> 'PK_' + t.name
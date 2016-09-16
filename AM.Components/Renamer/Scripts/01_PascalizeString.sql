IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PascalizeString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
EXEC dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[PascalizeString] () RETURNS INT AS BEGIN RETURN 0 END'
GO

/*
PRINT dbo.PascalizeString('my_field')
PRINT dbo.PascalizeString('customer_id')
PRINT dbo.PascalizeString('last_name')
PRINT dbo.PascalizeString('birthday_dt')
PRINT dbo.PascalizeString('allow_access_ind')
PRINT dbo.PascalizeString('pri_address_1')
PRINT dbo.PascalizeString('household_index')
PRINT dbo.PascalizeString('@account_code')
PRINT dbo.PascalizeString('m_account_id')
PRINT dbo.PascalizeString('m_AccountId')
*/
ALTER    FUNCTION [dbo].[PascalizeString] (@f varchar(128)) RETURNS VARCHAR(128) 
AS
BEGIN

	DECLARE @i int 
	DECLARE @member bit

	IF LEFT(@f, 2) = 'm_'
	BEGIN
		SET @member = 1
		SET @f = SUBSTRING(@f, 2, LEN(@f) - 1)
	END
	ELSE
	BEGIN
		SET @f = '_' + @f
	END

	-- Custom changes
	SET @f = REPLACE(@f, 'delete_ind', 'deleted_ind')
	SET @f = REPLACE(@f, 'group_ind', 'grp_ind')
	SET @f = REPLACE(@f, 'add_ind', 'added_ind')
	SET @f = REPLACE(@f, 'default_ind', 'default_item_ind')
	SET @f = REPLACE(@f, 'full_ind', 'full_version_ind')
	SET @f = REPLACE(@f, 'print_ind', 'printed_ind')
	SET @f = REPLACE(@f, 'cancel_ind', 'canceled_ind')
	SET @f = REPLACE(@f, 'lock_ind', 'locked_ind')

	IF (SUBSTRING(@f, LEN(@f) - 3 , 4) = '_ind')
	BEGIN
		SET @f = REPLACE(@f, '_ind', '')
	END

	IF (SUBSTRING(@f, LEN(@f) - 2 , 3) = '_dt')
	BEGIN
		SET @f = REPLACE(@f, '_dt', '_date')
	END

	-- Capitalize letters
	SET @i = 65

	WHILE (@i < 91)
	BEGIN
		SET @f = REPLACE(@f, '_' + CHAR(@i), UPPER(CHAR(@i)))
		SET @f = REPLACE(@f, '.' + CHAR(@i), '.' + UPPER(CHAR(@i)))
		SET @f = REPLACE(@f, '@' + CHAR(@i), '@' + UPPER(CHAR(@i)))
		SET @f = REPLACE(@f, '_@', '@')
		SET @i = @i + 1
	END

	-- Capitalize letters
	SET @i = 49

	WHILE (@i < 58)
	BEGIN
		SET @f = REPLACE(@f, '_' + CHAR(@i), UPPER(CHAR(@i)))
		SET @i = @i + 1
	END

	IF @member = 1
	BEGIN
		SET @f = 'm_' + LOWER(LEFT(@f, 1)) + SUBSTRING(@f, 2, 1000)
	END

	RETURN @f

END
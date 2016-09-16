--DELETE CruiseWeb_Rename..Renaming WHERE Manual = 0
--DELETE CruiseWeb_Rename..Renaming WHERE Keyword = 1 AND Manual = 0
--UPDATE CruiseWeb_Rename..Renaming SET NewName = dbo.PascalizeString(OldName) WHERE Keyword=1

SELECT *
FROM CruiseWeb_Rename..Renaming
--UPDATE CruiseWeb_Rename..Renaming SET manual=1, NewName='Existing'
WHERE OldName LIKE 'exists_ind%'

use cruiseweb_Rename
SELECT CruiseWeb_Rename.dbo.PascalizeString('@t_res_list')
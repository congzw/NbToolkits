SELECT *
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
WHERE TC.TABLE_NAME = 'App_MOOC_Category'
AND TC.CONSTRAINT_TYPE = 'UNIQUE'

--SELECT     col.name
--FROM         sys.objects AS obj INNER JOIN
--                      sys.columns AS col ON col.object_id = obj.object_id INNER JOIN
--                      sys.index_columns AS idx_cols ON idx_cols.column_id = col.column_id AND idx_cols.object_id = col.object_id INNER JOIN
--                      sys.indexes AS idx ON idx_cols.index_id = idx.index_id AND idx.object_id = col.object_id
--WHERE     (obj.name = 'App_MOOC_Category') AND (idx.is_unique = 1)



--SELECT     CCU.CONSTRAINT_NAME, CCU.COLUMN_NAME
--FROM         INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC INNER JOIN
--                      INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU ON TC.CONSTRAINT_CATALOG = CCU.CONSTRAINT_CATALOG AND 
--                      TC.CONSTRAINT_SCHEMA = CCU.CONSTRAINT_SCHEMA AND TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME
--WHERE     (TC.TABLE_NAME = 'App_MOOC_Category')


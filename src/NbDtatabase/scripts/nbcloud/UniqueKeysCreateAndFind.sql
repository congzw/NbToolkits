
--IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'UK_App_MOOC_Category__SiteId_RelationCode' AND object_id = OBJECT_ID('App_MOOC_Category'))
--BEGIN    
--	ALTER TABLE [dbo].[App_MOOC_Category] ADD CONSTRAINT [UK_App_MOOC_Category__SiteId_RelationCode] UNIQUE NONCLUSTERED 
--    (
--	    [SiteId] ASC,
--	    [RelationCode] ASC
--    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--END

--IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'UK_App_MOOC_Category__SiteId_Name' AND object_id = OBJECT_ID('App_MOOC_Category'))
--BEGIN    
--	ALTER TABLE [dbo].[App_MOOC_Category] ADD CONSTRAINT [UK_App_MOOC_Category__SiteId_Name] UNIQUE NONCLUSTERED 
--(
--	[SiteId] ASC,
--	[Name] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

--END


----Find UK Exist without name
--select COUNT(DISTINCT COLUMN_NAME) AS UK_Column_Count from INFORMATION_SCHEMA.KEY_COLUMN_USAGE
--where TABLE_NAME='App_MOOC_Category'
--AND COLUMN_NAME IN('SiteId', 'Name')



--SELECT *
--FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
--WHERE TC.TABLE_NAME = 'App_MOOC_Category'
--AND TC.CONSTRAINT_TYPE = 'UNIQUE'

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

--SELECT     CCU.CONSTRAINT_NAME, COUNT(*)
--FROM         INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC INNER JOIN
--                      INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU ON TC.CONSTRAINT_CATALOG = CCU.CONSTRAINT_CATALOG AND 
--                      TC.CONSTRAINT_SCHEMA = CCU.CONSTRAINT_SCHEMA AND TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME
--WHERE     (TC.TABLE_NAME = 'App_MOOC_Category')
--GROUP BY CCU.CONSTRAINT_NAME

--select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='UNIQUE' and table_name='App_MOOC_Category'
--select * from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where CONSTRAINT_NAME='UK_SiteId_RelationCode' and TABLE_NAME='App_MOOC_Category'
--select CONSTRAINT_NAME,COLUMN_NAME,TABLE_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where CONSTRAINT_NAME='UK_SiteId_RelationCode' and TABLE_NAME='App_MOOC_Category'

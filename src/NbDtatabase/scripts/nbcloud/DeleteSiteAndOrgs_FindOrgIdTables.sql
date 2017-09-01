USE [NbCloudDb_YDJY]
GO

--001 create table
CREATE TABLE [dbo].[Temp_Org](
	[TableName] [nvarchar](500) NOT NULL,
	[OrgIdCount] [int] NOT NULL
) ON [PRIMARY]
--TRUNCATE TABLE Temp_Org

--002 create sql
--查找包含<列名>的所有表.  拼接查询列相关数据的语句
SELECT
'INSERT INTO [dbo].[Temp_Org](TableName,OrgIdCount)
SELECT ''' + OBJECT_NAME(id) + ''' AS TableName, COUNT(OrgId) AS OrgIdCount FROM ' + OBJECT_NAME(id)
+'
;'
 from syscolumns
	where 
[name]= 'OrgId' 
and  OBJECTPROPERTY(id,'IsUserTable')=1

--003 exec sqls !todo!

--004 query data
SELECT * FROM [dbo].[Temp_Org] 
WHERE OrgIdCount > 0
ORDER BY OrgIdCount
USE [NbCloudDb_YDJY]
GO

--001 create table
CREATE TABLE [dbo].[Temp_Site](
	[TableName] [nvarchar](500) NOT NULL,
	[SiteIdCount] [int] NOT NULL
) ON [PRIMARY]
--TRUNCATE TABLE Temp_Site

--002 create sql
--���Ұ���<����>�����б�.  ƴ�Ӳ�ѯ��������ݵ����
SELECT
'INSERT INTO [dbo].[Temp_Site](TableName,SiteIdCount)
SELECT ''' + OBJECT_NAME(id) + ''' AS TableName, COUNT(SiteId) AS SiteIdCount FROM ' + OBJECT_NAME(id)
+'
;'
 from syscolumns
	where 
[name]= 'SiteId' 
and  OBJECTPROPERTY(id,'IsUserTable')=1

--003 exec sqls !todo!
;

--004 query data
SELECT * FROM [dbo].[Temp_Site] 
WHERE SiteIdCount > 0
ORDER BY SiteIdCount
Use PeopleSalesWH
GO
PRINT '<<< USING PeoplesSalesWH >>>'
PRINT ''

DECLARE @SPName VarChar(100)
SET @SPName = 'vw_People'
IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name] = @SPName)
  BEGIN
	DECLARE @SQL VarChar(1000)
	SET @SQL = 'CREATE VIEW ' + @SPName + ' AS select * from sysobjects'
	EXECUTE(@SQL)
  END
PRINT 'Updating View ' + @SPName
GO


ALTER VIEW vw_People AS
SELECT
	p.PersonID
	,p.PersonIDSOR AS PersonIDSystemOfRecord
	,p.FirstName
	,p.LastName
	,p.EffectiveDateID
	,d.CalendarDate AS EffectiveDate
FROM People p
INNER JOIN MasterDates d
	ON p.EffectiveDateID = d.DateID



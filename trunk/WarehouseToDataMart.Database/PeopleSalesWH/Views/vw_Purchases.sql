Use PeopleSalesWH
GO
PRINT '<<< USING PeoplesSalesWH >>>'
PRINT ''

DECLARE @SPName VarChar(100)
SET @SPName = 'vw_Purchases'
IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name] = @SPName)
  BEGIN
	DECLARE @SQL VarChar(1000)
	SET @SQL = 'CREATE VIEW ' + @SPName + ' AS select * from sysobjects'
	EXECUTE(@SQL)
  END
PRINT 'Updating View ' + @SPName
GO


ALTER VIEW vw_Purchases AS
SELECT
	p.PurchaseID
	,p.PurchaseAmount
	,p.NumberItems
	,p.PurchaseDateID
	,d.CalendarDate AS PurchaseDate
	,p.PersonID
	,pe.PersonIDSOR AS PersonIDSystemOfRecord
	,pe.FirstName
	,pe.LastName
FROM Purchases p
INNER JOIN MasterDates d
	ON p.PurchaseDateID = d.DateID
INNER JOIN People pe
	ON p.PersonID = pe.PersonID


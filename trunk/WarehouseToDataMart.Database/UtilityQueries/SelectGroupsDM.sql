USE PeopleSalesDM 
GO


SELECT 
	SUM(PurchaseAmount) as TotalPurchases
	,PurchaseDateID
	,PurchaseDate
FROM PeopleSalesDM.dbo.PurchaseFacts
GROUP BY PurchaseDateID, PurchaseDate
ORDER BY PurchaseDateID


SELECT 
	SUM(p.PurchaseAmount) as TotalPurchases
	,p.PersonID
	,m.MonthInYearNum
	,m.MonthName
FROM PeopleSalesDM.dbo.PurchaseFacts p
INNER JOIN MasterDatesDim m
	ON p.PurchaseDateID = m.DateID
GROUP BY p.PersonID, m.MonthInYearNum,m.MonthName
ORDER BY m.MonthInYearNum,p.PersonID
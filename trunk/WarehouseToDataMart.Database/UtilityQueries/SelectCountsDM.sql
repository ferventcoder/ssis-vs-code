USE PeopleSalesDM
GO

SELECT COUNT(*)
FROM dbo.MasterDatesDim
--71,588

SELECT COUNT(*)
FROM dbo.PeopleDim
--72,630 SSIS || 72,645 in actual - code gets it, SSIS misses it somehow

SELECT COUNT(*)
FROM dbo.PurchaseFacts
--1,834,991

SELECT COUNT(*)
FROM dbo.PurchaseAggregateFacts
--29,976

SELECT COUNT(*)
FROM dbo.PurchasesPersonMonthAggregateFacts
--362,366
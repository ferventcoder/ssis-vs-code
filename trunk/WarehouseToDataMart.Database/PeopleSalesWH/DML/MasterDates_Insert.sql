Use PeopleSalesWH
GO
PRINT '<<< USING PeoplesSalesWH >>>'
PRINT ''

set nocount ON
go

DECLARE @SPName VarChar(100) 
SET @SPName = 'ufn_GetValidBusinessDateID'
IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = @SPName)
 BEGIN
	 DECLARE @SQL VarChar(1000)
	 SET @SQL = 'CREATE FUNCTION ' + @SPName + '() returns DateTime
		AS
		begin 
			return(0) 
		end'
	 EXECUTE(@SQL)
 END
PRINT 'Updating Function ' + @SPName
GO

ALTER FUNCTION dbo.ufn_GetValidBusinessDateID(@DateID AS Int) RETURNS Int
AS 
BEGIN
	DECLARE @BusinessDateID Int
		
	SELECT @BusinessDateID = MAX(TD.DateID)
	FROM MasterDates TD
	WHERE TD.IsUSHoliday = 0
		AND TD.IsWeekend =0
		AND TD.DateID <= @DateID
	

	RETURN @BusinessDateID
END
GO

PRINT '<<< Finished Updating Function >>>'

DECLARE @SystemDate DATETIME
DECLARE @Days INT



IF EXISTS (SELECT * FROM MasterDates)
  BEGIN

	PRINT '<<< Delete Data if it Exists >>>' 
	
	DELETE FROM MasterDates

	DBCC CHECKIDENT (MasterDates, RESEED, 0)

	PRINT '<<< Finished Deleting Data >>>' 

  END



DECLARE @WDate datetime
SET @WDate = '1/1/1900'

PRINT '<<< Starting Insert of Date Information >>>' 

WHILE @WDate <= '12/31/2095'
    BEGIN

	INSERT INTO [MasterDates]( 
		[CalendarDate]
		, [DayOfWeekNum]
		, [DayOfWeekName]
		, [DayOfMonthNum]
		, [DayOfYearNum]
		, [WeekInYearNum]
		, [MonthInYearNum]
		, [MonthName]
		, [YearNum]
		, [IsFiscalFirstQuarter]
		, [IsFiscalSecondQuarter]
		, [IsFiscalThirdQuarter]
		, [IsFiscalFourthQuarter]
	--	, [IsUSHoliday]
	--	, [IsLondonHoliday]
	--	, [IsWeekend]
	--	, [IsEOM]
		, [IsEOQ]
		, [IsEOY] 
	)
	SELECT	
		CalendarDate = @WDate 
		,DayOfWeekNum = DATEPART(dw, @WDate)
	 	,[DayOfWeekName] = DATENAME(dw, @WDate)
		,[DayOfMonthNum] = DAY(@WDate)
	 	,[DayOfYearNum] = DATEPART(dy, @WDate)
	 	,[WeekInYearNum] = DATEPART(wk, @WDate)
	 	,[MonthInYearNum] = MONTH(@WDate)
	 	,[MonthName] = DATENAME(mm, @WDate) 
		,[YearNum] = YEAR(@WDate)
	 	,[IsFiscalFirstQuarter] = 0 
	 	,[IsFiscalSecondQuarter] = 0
	 	,[IsFiscalThirdQuarter] = 0
	 	,[IsFiscalFourthQuarter] = 0 
--	 	,[IsUSHoliday] = (
--			CASE 
--				WHEN Enterprise.dbo.ufn_Flames_IsUSHoliday_DMBuildOnly(@WDate) > '' THEN 1
--				ELSE 0
--			END) 
--	 	,[IsLondonHoliday] = (
--			CASE 
--				WHEN Enterprise.dbo.ufn_FHLB_IsUKHoliday(@WDate) > '' THEN 1
--				ELSE 0
--			END ) 
--	 	,[IsWeekend] = (
--			CASE DATEPART(dw, @WDate)
--				WHEN 1 THEN 1
--				WHEN 7 THEN 1
--				ELSE 0
--			END )
--	 	,[IsEOM] = (
--			CASE 
--				WHEN @WDate = Enterprise.dbo.ufn_FHLB_GetEOM(@WDate) THEN 1
--				ELSE 0
--			END )
	 	,[IsEOQ] = 0
	 	,[IsEOY] = 0


	SET @WDate = @WDate + 1

    END

PRINT '<<< Finishing Insert of Date Information >>>' 

DECLARE @Records BigInt
SELECT @Records = COUNT(*) FROM MasterDates
Print 'MasterDates - Total Records: ' + CAST(@Records AS VARCHAR)
PRINT ''


--  End Load the DMTradeDates table

--  ***********************************************************************************************************************
--  START Load - The Business EOM, EOQ, and EOY  Updates
--  ****************************************************************************************************

PRINT '<<< Starting Update of Business Date Information >>>' 

--1 Update all dates where the Weekend & USHoliday are both 0
UPDATE MasterDates
SET IsBusinessEOM = 1
WHERE
	IsEOM = 1
	AND (
		IsWeekend = 0
		AND
		IsUSHoliday = 0
	)

UPDATE MasterDates
SET IsBusinessEOQ = 1
WHERE
	IsEOQ = 1
	AND (
		IsWeekend = 0
		AND
		IsUSHoliday = 0
	)

UPDATE MasterDates
SET IsBusinessEOY = 1
WHERE
	IsEOY = 1
	AND (
		IsWeekend = 0
		AND
		IsUSHoliday = 0
	)



--2 Get all of the dates where the EOM is on a Weekend or Holiday (into a temp table)
CREATE TABLE #DateBusinessTemp
(
  DateID					Int
  ,CalendarDate				DateTime
  ,[MonthName]				VarChar(20)
  ,DayOfMonthNum			TinyInt
  ,IsUSHoliday			Bit
  ,IsWeekend				Bit
  ,IsEOM					Bit
  ,IsEOQ					Bit
  ,IsEOY					Bit
  ,PreviousBusinessDateID	Int
)

INSERT INTO #DateBusinessTemp
(
	DateID
	,CalendarDate
	,[MonthName]
	,DayOfMonthNum
	,IsUSHoliday
	,IsWeekend
	,IsEOM
	,IsEOQ
	,IsEOY
)
SELECT 
	DateID
	,CalendarDate
	,[MonthName]
	,DayOfMonthNum
	,IsUSHoliday
	,IsWeekend
	,IsEOM
	,IsEOQ
	,IsEOY 
FROM MasterDates TD
WHERE 
	TD.IsEOM = 1
	AND (
		TD.IsWeekend = 1
		OR
		TD.IsUSHoliday = 1
	)


--3 Insert the closest prior business date into the PreviousBusinessDateID column for that Temp table
UPDATE #DateBusinessTemp
SET PreviousBusinessDateID = dbo.ufn_GetValidBusinessDateID(DateID)


--4 Update the actual table with the EOMBusinessDate
UPDATE MasterDates
SET IsBusinessEOM = 1
WHERE
	DateID IN (
		Select 	PreviousBusinessDateID
		From	#DateBusinessTemp
	)

UPDATE MasterDates
SET IsBusinessEOQ = 1
WHERE
	DateID IN (
		Select 	PreviousBusinessDateID
		From	#DateBusinessTemp
		Where	IsEOQ = 1
	)

UPDATE MasterDates
SET IsBusinessEOY = 1
WHERE
	DateID IN (
		Select 	PreviousBusinessDateID
		From	#DateBusinessTemp
		Where	IsEOY = 1
	)


PRINT '<<< Finished Update of Business Date Information >>>' 

--5 Delete the TempTable

DROP TABLE #DateBusinessTemp 
DROP FUNCTION dbo.ufn_GetValidBusinessDateID

GO
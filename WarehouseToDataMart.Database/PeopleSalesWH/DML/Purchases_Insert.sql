Use PeopleSalesWH
GO
PRINT '<<< USING PeoplesSalesWH >>>'
PRINT ''

IF EXISTS (SELECT * FROM Purchases)
  BEGIN
	
	PRINT '<<< Delete Data if it Exists >>>' 
	
	DELETE FROM Purchases

	DBCC CHECKIDENT (Purchases, RESEED, 0)

	PRINT '<<< Finished Deleting Data >>>' 
	PRINT ''
	
  END

DECLARE @PersonID			BigInt
		,@PersonIDSOR		Int
		,@EffectiveDateID	BigInt
		,@EndEffectiveDateID BigInt
		,@PurchaseAmount	Money
		,@NumberOfItems		Int
		,@WDateID			BigInt
		,@DateStep			BigInt

SET @PersonID = 1

SET @PurchaseAmount = 300.23
SET @NumberOfItems = 5

DECLARE @MaxPersonID BigInt

SELECT 
	@MaxPersonID = MAX(PersonID)
FROM People


SET @DateStep = 5

WHILE @PersonID <= @MaxPersonID
  BEGIN

	SELECT @PersonIDSOR = PersonIDSOR 
	FROM People 
	WHERE PersonID = @PersonID
	
	/*
		Get the starting date to make purchases.
	*/

	SELECT @EffectiveDateID = EffectiveDateID 
	FROM People 
	WHERE PersonID = @PersonID

	/*
		Get the ending date to make purchases.
	*/

	SELECT @EndEffectiveDateID = MIN(EffectiveDateID) 
	FROM People 
	WHERE 
		PersonIDSOR = @PersonIDSOR 
		AND PersonID > @PersonID

	IF (@EndEffectiveDateID IS NULL) 
	  BEGIN
	
		SET @EndEffectiveDateID = 60000
	
	  END
	

	PRINT '<<< Start Inserting Rows for PersonID = ' + CAST(@PersonID AS VarChar) + ' >>>' 
	
	SET @WDateID = @EffectiveDateID
	
	--Insert purchases every day for that user during the effective dates
	WHILE @WDateID < @EndEffectiveDateID
	  BEGIN


		INSERT INTO [PeopleSalesWH].[dbo].[Purchases] (
					[PurchaseAmount]
				   ,[NumberItems]
				   ,[PurchaseDateID]
				   ,[PersonID]
			) VALUES (
					@PurchaseAmount + CAST(@PersonID AS Money) + CAST(@WDateID AS Money)
				   ,@NumberOfItems
				   ,@WDateID
				   ,@PersonID
			)

		SET @WDateID = @WDateID + @DateStep

	  END

	PRINT '<<< Finished Inserting Rows for PersonID = ' + CAST(@PersonID AS VarChar) + ' >>>' 
	PRINT ''

	SET @PersonID = @PersonID + 1

  END

DECLARE @Records BigInt
SELECT @Records = COUNT(*) FROM Purchases
Print 'Purchases - Total Records: ' + CAST(@Records AS VARCHAR)
PRINT ''

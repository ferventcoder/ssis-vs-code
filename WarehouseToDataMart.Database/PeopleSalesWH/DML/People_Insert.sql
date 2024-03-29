Use PeopleSalesWH
GO
PRINT '<<< USING PeoplesSalesWH >>>'

IF EXISTS (SELECT * FROM People)
  BEGIN
	
	PRINT '<<< Delete Data if it Exists >>>' 

	DELETE FROM Purchases

	DELETE FROM People

	DBCC CHECKIDENT (People, RESEED, 0)
	DBCC CHECKIDENT (Purchases, RESEED, 0)

	PRINT '<<< Finished Deleting Data >>>'
	PRINT ''  	
	
  END

DECLARE @FirstName VarChar(50)
DECLARE @LastName VarChar(50) 


DECLARE @WDateID BigInt
DECLARE @PersonIDSOR Int

SET @FirstName = 'First'
SET @LastName = 'Last'

SET @PersonIDSOR = 30

SET @FirstName = @FirstName + CAST(@PersonIDSOR As VarChar)
SET @LastName = @LastName + CAST(@PersonIDSOR As VarChar)

WHILE @PersonIDSOR <= 330
  BEGIN

	PRINT '<<< Start Inserting Rows for PersonIDSOR = ' + CAST(@PersonIDSOR AS VarChar) + ' >>>' 
	
	SET @WDateID = 30000

	WHILE @WDateID <= 60000
	  BEGIN

		INSERT INTO [PeopleSalesWH].[dbo].[People] (
				[PersonIDSOR]
			   ,[FirstName]
			   ,[LastName]
			   ,[EffectiveDateID]
		) VALUES (
				@PersonIDSOR
			   ,@FirstName + '_' + CAST(@WDateID AS VarChar)
			   ,@LastName
			   ,@WDateID
		)

		SET @WDateID = @WDateID + @PersonIDSOR

	  END

	PRINT '<<< Finished Inserting Rows for PersonIDSOR = ' + CAST(@PersonIDSOR AS VarChar) + ' >>>' 
	PRINT ''

	SET @PersonIDSOR = @PersonIDSOR + 1
	SET @FirstName = 'First' + CAST(@PersonIDSOR As VarChar)
	SET @LastName = 'Last' + CAST(@PersonIDSOR As VarChar)
	
  END


DECLARE @Records BigInt
SELECT @Records = COUNT(*) FROM People
Print 'People - Total Records: ' + CAST(@Records AS VARCHAR)
PRINT ''
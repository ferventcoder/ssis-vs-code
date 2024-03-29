USE PeopleSalesWH
GO

PRINT '<<< USING PeoplesSalesWH >>>'

/*
	DROP THE TABLES IF THEY EXIST
*/


IF OBJECT_ID('Purchases') IS NOT NULL
BEGIN
    DROP TABLE Purchases
    PRINT '<<< DROPPED TABLE Purchases >>>'
END
go

IF OBJECT_ID('People') IS NOT NULL
BEGIN
    DROP TABLE People
    PRINT '<<< DROPPED TABLE People >>>'
END
go


IF OBJECT_ID('MasterDates') IS NOT NULL
BEGIN
    DROP TABLE MasterDates
    PRINT '<<< DROPPED TABLE MasterDates >>>'
END
go


/****** Object:  Table dbo.MasterDates    Script Date: 01/29/2008 11:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE MasterDates(
    DateID                     bigint         IDENTITY(1,1),
    CalendarDate               datetime       NOT NULL,
    DayOfWeekNum               tinyint        NULL,
    DayOfWeekName              varchar(20)    NULL,
    DayOfMonthNum              tinyint        NULL,
    DayOfYearNum               smallint       NULL,
    WeekInYearNum              tinyint        NULL,
    MonthInYearNum             tinyint        NULL,
    MonthName                  varchar(20)    NULL,
    IsFiscalFirstQuarter       bit            DEFAULT 0 NOT NULL,
    IsFiscalSecondQuarter      bit            DEFAULT 0 NOT NULL,
    IsFiscalThirdQuarter       bit            DEFAULT 0 NOT NULL,
    IsFiscalFourthQuarter      bit            DEFAULT 0 NOT NULL,
    IsUSHoliday                bit            DEFAULT 0 NOT NULL,
    IsLondonHoliday            bit            DEFAULT 0 NOT NULL,
    IsWeekend                  bit            DEFAULT 0 NOT NULL,
    IsEOM                      bit            DEFAULT 0 NOT NULL,
    IsBusinessEOM              bit            DEFAULT 0 NOT NULL,
    IsEOQ                      bit            DEFAULT 0 NOT NULL,
    IsBusinessEOQ              bit            DEFAULT 0 NOT NULL,
    IsEOY                      bit            DEFAULT 0 NOT NULL,
    IsBusinessEOY              bit            DEFAULT 0 NOT NULL,
    YearNum                    int            NULL,
    CONSTRAINT PK_WhDatesMasterDimension_DateID PRIMARY KEY NONCLUSTERED (DateID)
)
go

PRINT '<<< CREATED TABLE MasterDates >>>'

/****** Object:  Table dbo.People    Script Date: 01/29/2008 11:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.People(
	PersonID			bigint IDENTITY(1,1) NOT NULL,
	PersonIDSOR			int NOT NULL,
	FirstName			nvarchar(200) NULL,
	LastName			nvarchar(150) NULL,
	EffectiveDateID		bigint NOT NULL,
 CONSTRAINT PK_People PRIMARY KEY CLUSTERED 
(
	PersonID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

PRINT '<<< CREATED TABLE People >>>'

/****** Object:  Table dbo.Purchases    Script Date: 01/29/2008 11:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.Purchases(
	PurchaseID			bigint IDENTITY(1,1) NOT NULL,
	PurchaseAmount		decimal(18, 4) NOT NULL,
	NumberItems			int NULL,
	PurchaseDateID		bigint NULL,
	PersonID			bigint NULL,
 CONSTRAINT PK_Purchases PRIMARY KEY CLUSTERED 
(
	PurchaseID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

PRINT '<<< CREATED TABLE Purchases >>>'

/****** Object:  ForeignKey FK_People_MasterDates    Script Date: 01/29/2008 11:06:23 ******/
ALTER TABLE dbo.People  WITH CHECK ADD  CONSTRAINT FK_People_MasterDates FOREIGN KEY(EffectiveDateID)
REFERENCES dbo.MasterDates (DateID)
GO
ALTER TABLE dbo.People CHECK CONSTRAINT FK_People_MasterDates
GO
/****** Object:  ForeignKey FK_Purchases_MasterDates1    Script Date: 01/29/2008 11:06:23 ******/
ALTER TABLE dbo.Purchases  WITH CHECK ADD  CONSTRAINT FK_Purchases_MasterDates1 FOREIGN KEY(PurchaseDateID)
REFERENCES dbo.MasterDates (DateID)
GO
ALTER TABLE dbo.Purchases CHECK CONSTRAINT FK_Purchases_MasterDates1
GO
/****** Object:  ForeignKey FK_Purchases_People    Script Date: 01/29/2008 11:06:23 ******/
ALTER TABLE dbo.Purchases  WITH CHECK ADD  CONSTRAINT FK_Purchases_People FOREIGN KEY(PersonID)
REFERENCES dbo.People (PersonID)
GO
ALTER TABLE dbo.Purchases CHECK CONSTRAINT FK_Purchases_People
GO
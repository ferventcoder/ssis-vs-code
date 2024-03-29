USE PeopleSalesDM
GO

PRINT '<<< USING PeoplesSalesDM >>>'
PRINT ''

/*
	DROP THE TABLES IF THEY EXIST
*/


IF OBJECT_ID('PurchaseFacts') IS NOT NULL
BEGIN
    DROP TABLE PurchaseFacts
    PRINT '<<< DROPPED TABLE PurchaseFacts >>>'
END
go

IF OBJECT_ID('PurchasesPersonMonthAggregateFacts') IS NOT NULL
BEGIN
    DROP TABLE PurchasesPersonMonthAggregateFacts
    PRINT '<<< DROPPED TABLE PurchasesPersonMonthAggregateFacts >>>'
END
go

IF OBJECT_ID('PeopleDim') IS NOT NULL
BEGIN
    DROP TABLE PeopleDim
    PRINT '<<< DROPPED TABLE PeopleDim >>>'
END
go


IF OBJECT_ID('PurchaseAggregateFacts') IS NOT NULL
BEGIN
    DROP TABLE PurchaseAggregateFacts
    PRINT '<<< DROPPED TABLE PurchaseAggregateFacts >>>'
END
go


IF OBJECT_ID('MasterDatesDim') IS NOT NULL
BEGIN
    DROP TABLE MasterDatesDim
    PRINT '<<< DROPPED TABLE MasterDatesDim >>>'
END
go

PRINT ''

/****** Object:  Table dbo.MasterDatesDim    Script Date: 01/30/2008 14:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE dbo.MasterDatesDim(
	DateID					bigint NOT NULL,
	CalendarDate			datetime NOT NULL,
	DayOfWeekNum			tinyint NULL,
	DayOfWeekName 			varchar(20) NULL,
	DayOfMonthNum 			tinyint NULL,
	DayOfYearNum 			smallint NULL,
	WeekInYearNum 			tinyint NULL,
	MonthInYearNum 			tinyint NULL,
	MonthName 				varchar(20) NULL,
	IsFiscalFirstQuarter	bit NOT NULL CONSTRAINT DF__MasterDat__IsFis__6477ECF3  DEFAULT ((0)),
	IsFiscalSecondQuarter	bit NOT NULL CONSTRAINT DF__MasterDat__IsFis__656C112C  DEFAULT ((0)),
	IsFiscalThirdQuarter	bit NOT NULL CONSTRAINT DF__MasterDat__IsFis__66603565  DEFAULT ((0)),
	IsFiscalFourthQuarter	bit NOT NULL CONSTRAINT DF__MasterDat__IsFis__6754599E  DEFAULT ((0)),
	IsUSHoliday				bit NOT NULL CONSTRAINT DF__MasterDat__IsUSH__68487DD7  DEFAULT ((0)),
	IsLondonHoliday			bit NOT NULL CONSTRAINT DF__MasterDat__IsLon__693CA210  DEFAULT ((0)),
	IsWeekend				bit NOT NULL CONSTRAINT DF__MasterDat__IsWee__6A30C649  DEFAULT ((0)),
	IsEOM					bit NOT NULL CONSTRAINT DF__MasterDat__IsEOM__6B24EA82  DEFAULT ((0)),
	IsBusinessEOM			bit NOT NULL CONSTRAINT DF__MasterDat__IsBus__6C190EBB  DEFAULT ((0)),
	IsEOQ					bit NOT NULL CONSTRAINT DF__MasterDat__IsEOQ__6D0D32F4  DEFAULT ((0)),
	IsBusinessEOQ			bit NOT NULL CONSTRAINT DF__MasterDat__IsBus__6E01572D  DEFAULT ((0)),
	IsEOY					bit NOT NULL CONSTRAINT DF__MasterDat__IsEOY__6EF57B66  DEFAULT ((0)),
	IsBusinessEOY 			bit NOT NULL CONSTRAINT DF__MasterDat__IsBus__6FE99F9F  DEFAULT ((0)),
	YearNum 				int NULL,
 CONSTRAINT PK_WhDatesMasterDimension_DateID PRIMARY KEY CLUSTERED 
(
	DateID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

PRINT '<<< CREATED TABLE MasterDatesDim >>>'

/****** Object:  Table dbo.PeopleDim    Script Date: 01/30/2008 14:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.PeopleDim(
	PersonID 			bigint NOT NULL,
	PersonIDSOR 		int NOT NULL,
	FirstName 			nvarchar(200) NULL,
	LastName 			nvarchar(150) NULL,
	EffectiveDateID		bigint NOT NULL,
	EffectiveFromDate	datetime NULL,
	EffectiveToDate		datetime NULL,
	IsActive			bit NOT NULL CONSTRAINT DF_PeopleDim_IsActive  DEFAULT ((1)),
 CONSTRAINT PK_PeopleDim PRIMARY KEY CLUSTERED 
(
	PersonID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

PRINT '<<< CREATED TABLE PeopleDim >>>'

/****** Object:  Table dbo.PurchaseAggregateFacts    Script Date: 01/30/2008 14:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.PurchaseAggregateFacts(
	PurchaseAggID 			bigint IDENTITY(1,1) NOT NULL,
	PurchasesTotalAmount	decimal(18,4) NULL,
	PurchasesDateID			bigint NULL,
	PurchasesDate 			datetime NULL,
	IsActive 				bit NOT NULL CONSTRAINT DF_PurchaseAggregateFacts_IsActive  DEFAULT ((1)),
 CONSTRAINT PK_PurchaseAggregateFacts PRIMARY KEY CLUSTERED 
(
	PurchaseAggID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

PRINT '<<< CREATED TABLE PurchaseAggregateFacts >>>'

/****** Object:  Table dbo.PurchaseFacts    Script Date: 01/30/2008 14:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.PurchaseFacts(
	PurchaseID 			bigint NOT NULL,
	PurchaseAmount		decimal(18,4) NOT NULL,
	NumberItems 		int NULL,
	PurchaseDateID		bigint NULL,
	PersonID 			bigint NULL,
	PurchaseDate		datetime NULL,
	IsActive 			bit NOT NULL CONSTRAINT DF_PurchaseFacts_IsActive  DEFAULT ((1)),
 CONSTRAINT PK_PurchaseFacts PRIMARY KEY CLUSTERED 
(
	PurchaseID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

PRINT '<<< CREATED TABLE PurchaseFacts >>>'

/****** Object:  Table dbo.PurchasesPersonMonthAggregateFacts    Script Date: 01/30/2008 14:15:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE dbo.PurchasesPersonMonthAggregateFacts(
	PurchasesTotalAmount	decimal(18,4) NULL,
	PersonID 				bigint NOT NULL,
	MonthNumber 			tinyint NOT NULL,
	MonthName 				varchar(20) NULL,
	PurchasesMonthAggID 	bigint IDENTITY(1,1) NOT NULL,
	IsActive 				bit NOT NULL CONSTRAINT DF_PurchasesPersonMonthAggregateFacts_IsActive  DEFAULT ((1)),
 CONSTRAINT PK_PurchasesPersonMonthAggregateFacts_1 PRIMARY KEY CLUSTERED 
(
	PurchasesMonthAggID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

PRINT '<<< CREATED TABLE PurchasesPersonMonthAggregateFacts >>>'
PRINT ''

PRINT '<<< START CREATE FKs >>>'

/****** Object:  ForeignKey FK_PeopleDim_MasterDatesDim    Script Date: 01/30/2008 14:18:07 ******/
ALTER TABLE dbo.PeopleDim  WITH CHECK ADD  CONSTRAINT FK_PeopleDim_MasterDatesDim FOREIGN KEY(EffectiveDateID)
REFERENCES dbo.MasterDatesDim (DateID)
GO
ALTER TABLE dbo.PeopleDim CHECK CONSTRAINT FK_PeopleDim_MasterDatesDim
GO
/****** Object:  ForeignKey FK_PurchaseAggregateFacts_MasterDatesDim    Script Date: 01/30/2008 14:18:07 ******/
ALTER TABLE dbo.PurchaseAggregateFacts  WITH CHECK ADD  CONSTRAINT FK_PurchaseAggregateFacts_MasterDatesDim FOREIGN KEY(PurchasesDateID)
REFERENCES dbo.MasterDatesDim (DateID)
GO
ALTER TABLE dbo.PurchaseAggregateFacts CHECK CONSTRAINT FK_PurchaseAggregateFacts_MasterDatesDim
GO
/****** Object:  ForeignKey FK_PurchaseFacts_MasterDatesDim1    Script Date: 01/30/2008 14:18:07 ******/
ALTER TABLE dbo.PurchaseFacts  WITH CHECK ADD  CONSTRAINT FK_PurchaseFacts_MasterDatesDim1 FOREIGN KEY(PurchaseDateID)
REFERENCES dbo.MasterDatesDim (DateID)
GO
ALTER TABLE dbo.PurchaseFacts CHECK CONSTRAINT FK_PurchaseFacts_MasterDatesDim1
GO
/****** Object:  ForeignKey FK_PurchaseFacts_PeopleDim    Script Date: 01/30/2008 14:18:07 ******/
ALTER TABLE dbo.PurchaseFacts  WITH CHECK ADD  CONSTRAINT FK_PurchaseFacts_PeopleDim FOREIGN KEY(PersonID)
REFERENCES dbo.PeopleDim (PersonID)
GO
ALTER TABLE dbo.PurchaseFacts CHECK CONSTRAINT FK_PurchaseFacts_PeopleDim
GO
/****** Object:  ForeignKey FK_PurchasesPersonMonthAggregateFacts_MasterDatesDim    Script Date: 01/30/2008 14:18:07 ******/
--ALTER TABLE dbo.PurchasesPersonMonthAggregateFacts  WITH CHECK ADD  CONSTRAINT FK_PurchasesPersonMonthAggregateFacts_MasterDatesDim FOREIGN KEY(MonthDateID)
--REFERENCES dbo.MasterDatesDim (DateID)
--GO
--ALTER TABLE dbo.PurchasesPersonMonthAggregateFacts CHECK CONSTRAINT FK_PurchasesPersonMonthAggregateFacts_MasterDatesDim
--GO
/****** Object:  ForeignKey FK_PurchasesPersonMonthAggregateFacts_PeopleDim    Script Date: 01/30/2008 14:18:07 ******/
ALTER TABLE dbo.PurchasesPersonMonthAggregateFacts  WITH CHECK ADD  CONSTRAINT FK_PurchasesPersonMonthAggregateFacts_PeopleDim FOREIGN KEY(PersonID)
REFERENCES dbo.PeopleDim (PersonID)
GO
ALTER TABLE dbo.PurchasesPersonMonthAggregateFacts CHECK CONSTRAINT FK_PurchasesPersonMonthAggregateFacts_PeopleDim
GO

PRINT '<<< END CREATE FKs >>>'
PRINT ''
PRINT '<<< START CREATE Unique Indexes >>>'

/****** Object:  Index IX_MasterDatesDim    Script Date: 01/30/2008 13:58:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX IX_MasterDatesDim ON dbo.MasterDatesDim 
(
	CalendarDate ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index IX_PeopleDim    Script Date: 01/30/2008 13:58:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX IX_PeopleDim ON dbo.PeopleDim 
(
	PersonIDSOR ASC,
	EffectiveDateID ASC,
	IsActive ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index IX_PurchaseAggregateFacts    Script Date: 01/30/2008 13:58:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX IX_PurchaseAggregateFacts ON dbo.PurchaseAggregateFacts 
(
	PurchasesDateID ASC,
	IsActive ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index IX_PurchaseFacts    Script Date: 01/30/2008 13:58:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX IX_PurchaseFacts ON dbo.PurchaseFacts 
(
	IsActive ASC,
	PurchaseDateID ASC,
	PurchaseID ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index IX_PurchasesPersonMonthAggregateFacts    Script Date: 01/30/2008 13:03:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX IX_PurchasesPersonMonthAggregateFacts ON dbo.PurchasesPersonMonthAggregateFacts 
(
	IsActive ASC,
	PersonID ASC,
	MonthName ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

PRINT '<<< END CREATE Unique Indexes >>>'
SET DIR=%~d0%~p0%
SET INSTANCE=(local)

osql -S %INSTANCE% -d master -E -i %DIR%PeopleSalesWH\DDL\CreateWHDatabase_DDL.sql
osql -S %INSTANCE% -d PeopleSalesWH -E -i %DIR%PeopleSalesWH\DDL\CreateWHTableStructuresDDL.sql
osql -S %INSTANCE% -d PeopleSalesWH -E -i %DIR%PeopleSalesWH\DML\MasterDates_Insert.sql
osql -S %INSTANCE% -d PeopleSalesWH -E -i %DIR%PeopleSalesWH\DML\People_Insert.sql
osql -S %INSTANCE% -d PeopleSalesWH -E -i %DIR%PeopleSalesWH\DML\Purchases_Insert.sql
osql -S %INSTANCE% -d PeopleSalesWH -E -i %DIR%PeopleSalesWH\DML\vw_People.sql
osql -S %INSTANCE% -d PeopleSalesWH -E -i %DIR%PeopleSalesWH\DML\vw_Purchases.sql
osql -S %INSTANCE% -d master -E -i %DIR%PeopleSalesDM\DDL\CreateDMDatabase_DDL.sql
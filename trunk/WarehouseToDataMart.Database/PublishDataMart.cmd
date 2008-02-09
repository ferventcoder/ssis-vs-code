SET DIR=%~d0%~p0%
SET INSTANCE=(local)

osql -S %INSTANCE% -d PeopleSalesDM -E -i %DIR%PeopleSalesDM\DDL\CreateDMTableStructuresDDL.sql
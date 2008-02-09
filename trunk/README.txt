Summary: 
 -This tests the performance differences between SSIS and 
	Code [using NHibernate with List(Of Objects)] on a move from a Warehouse to a Data Mart.

Prerequisites:
 -VS2005
 -SQLServer 2005 installed on your local system (or edit database locations)
 -MbUnit Installed on the system
 
Setup: 
 -To get started, locate the file called "InstallPeopleSales.cmd" in 
	WarehouseToDataMart.Database and run the file.  
	If you you open it with the run command in Visual Studio, leave everything blank and hit OK.

Known Issues:
 -The NHibernate mapping files are mapped to a specific directory.  
	They are not embedded as resources because their is a conflict.


Findings: 
 -What I am finding is that if you get enough data, you will find a memory leak in SSIS.
	In other words, SSIS doesn't scale very well and NHibernate does.
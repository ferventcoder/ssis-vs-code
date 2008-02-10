Imports System.Collections.Generic
Imports System
Imports System.Configuration
Imports System.Diagnostics
Imports WarehouseToDataMart.Common
Imports WarehouseToDataMart.Warehouse
Imports WarehouseToDataMart.DataMart
Imports Microsoft.VisualBasic
Imports WarehouseToDataMart.Repositories

Public Class WarehouseMoveProgram

    Private _projectDirectory As String

    Public Sub New()
        _projectDirectory = ConfigurationManager.AppSettings("ProjectDirectoryPath")
    End Sub

    Public Sub Run()
        Trace.WriteLine(String.Format("*").PadRight(35, CChar("*")))
        Trace.WriteLine(String.Format("-").PadRight(35, CChar("-")))
        Trace.WriteLine(String.Format("Starting the process at {0}", DateTime.Now().ToLongTimeString()))
        Trace.WriteLine("")

        Trace.WriteLine(String.Format("-").PadRight(25, CChar("-")))
        Trace.WriteLine(String.Format("Running SSIS at {0}", DateTime.Now().ToLongTimeString()))
        Dim startTicks As Long = DateTime.Now.Ticks()
        MoveDataWithSSIS()
        Dim endTicks As Long = DateTime.Now.Ticks()
        Trace.WriteLine(String.Format("Finished Running SSIS at {0}", DateTime.Now().ToLongTimeString()))

        Trace.WriteLine(String.Format("Running the SSIS Package to move data from the warehouse to the data mart took {0} ticks", (endTicks - startTicks)))
        Trace.WriteLine(String.Format("That is a total of {0} seconds.", TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds))
        Trace.WriteLine(String.Format("A total of {0}:{1} minutes.", _
            CLng(TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds) \ 60, _
            (CLng(TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds) Mod 60).ToString().PadLeft(2, CChar("0"))))
        Trace.WriteLine(String.Format("-").PadRight(25, CChar("-")))

        Trace.WriteLine(String.Format(""))
        Trace.Flush()

        Trace.WriteLine(String.Format("-").PadRight(25, CChar("-")))
        Trace.WriteLine(String.Format("Running Code at {0}", DateTime.Now().ToLongTimeString()))
        startTicks = DateTime.Now.Ticks()
        MoveDataWithCode()
        endTicks = DateTime.Now.Ticks()
        Trace.WriteLine(String.Format("Finished Running Code at {0}", DateTime.Now().ToLongTimeString()))

        Trace.WriteLine(String.Format("Running code to move objects from the warehouse to the data mart took {0} ticks.", (endTicks - startTicks)))
        Trace.WriteLine(String.Format("That is a total of {0} seconds.", TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds))
        Trace.WriteLine(String.Format("A total of {0}:{1} minutes.", _
            CLng(TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds) \ 60, _
            (CLng(TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds) Mod 60).ToString().PadLeft(2, CChar("0"))))
        Trace.WriteLine(String.Format("-").PadRight(25, CChar("-")))

        Trace.WriteLine("")
        Trace.WriteLine(String.Format("Finished the process at {0}", DateTime.Now().ToLongTimeString()))
        Trace.WriteLine(String.Format("-").PadRight(35, CChar("-")))
        Trace.WriteLine(String.Format("*").PadRight(35, CChar("*")))

        Trace.Close()
    End Sub

    Private Sub MoveDataWithSSIS()
        Dim path As String = _projectDirectory & ConfigurationManager.AppSettings("SSISPackageFilePath")
        Dim ssisFileName As String = ConfigurationManager.AppSettings("SSISPackageName")
        Dim configFileName As String = ConfigurationManager.AppSettings("SSISConfigName")
        Dim cmdToExecute As String = "dtexec.exe"
        Dim parameters As String = String.Format(" /FILE ""{0}"" /CONFIGFILE ""{1}""", path & ssisFileName, path & configFileName)

        Shell(cmdToExecute & parameters, AppWinStyle.Hide, True)
    End Sub

    Private Sub MoveDataWithCode()
        Trace.WriteLine(String.Format("Starting Republish at {0}.", DateTime.Now().ToLongTimeString()))
        RepublishDataMartStructure()
        Trace.WriteLine(String.Format("Finished Republish at {0}.", DateTime.Now().ToLongTimeString()))

        ' set date range to move = 50 years
        ' NOT worried about this for this example - gonna have both move the high amounts of data

        ' date objects
        Trace.WriteLine(String.Format("Getting date objects at {0}.", DateTime.Now().ToLongTimeString()))
        Dim dateInfo As IList(Of DateInformation) = New Repository(Of DateInformation)(HibernateWarehouseMapperFactory.SessionFactory).GetAll()
        Trace.WriteLine(String.Format("Finished Getting date objects at {0}.", DateTime.Now().ToLongTimeString()))

        Trace.WriteLine(String.Format("Saving date dimensions at {0}.", DateTime.Now().ToLongTimeString()))
        Dim dateInfoDimRepository As IRepository(Of DateInformation) = New Repository(Of DateInformation)(HibernateDataMartMapperFactory.SessionFactory)
        dateInfoDimRepository.Save(dateInfo)
        Trace.WriteLine(String.Format("Finished Saving date dimensions at {0}.", DateTime.Now().ToLongTimeString()))

        dateInfo = Nothing
        dateInfoDimRepository = Nothing

        ' person objects
        Trace.WriteLine(String.Format("Getting person objects at {0}.", DateTime.Now().ToLongTimeString()))
        Dim people As IList(Of Person) = New Repository(Of Person)(HibernateWarehouseMapperFactory.SessionFactory).GetAll()
        Trace.WriteLine(String.Format("Finished Getting person objects at {0}.", DateTime.Now().ToLongTimeString()))

        Trace.WriteLine(String.Format("Pushing person objects into dimensions at {0}.", DateTime.Now().ToLongTimeString()))
        Dim peopleDimension As IList(Of PersonDimension) = SetUpPersonDimensions(people)
        Trace.WriteLine(String.Format("Finished Pushing person objects into dimensions at {0}.", DateTime.Now().ToLongTimeString()))

        people = Nothing

        Trace.WriteLine(String.Format("Saving person dimensions at {0}.", DateTime.Now().ToLongTimeString()))
        Dim peopleDimRepository As IRepository(Of PersonDimension) = New Repository(Of PersonDimension)(HibernateDataMartMapperFactory.SessionFactory)
        peopleDimRepository.Save(peopleDimension)
        Trace.WriteLine(String.Format("Finished Saving person dimensions at {0}.", DateTime.Now().ToLongTimeString()))

        peopleDimension = Nothing
        peopleDimRepository = Nothing

        ' move purchase objects
        Trace.WriteLine(String.Format("Getting purchase objects at {0}.", DateTime.Now().ToLongTimeString()))
        Dim purchases As IList(Of Purchase) = New Repository(Of Purchase)(HibernateWarehouseMapperFactory.SessionFactory).GetAll()
        Trace.WriteLine(String.Format("Finished Getting purchase objects at {0}.", DateTime.Now().ToLongTimeString()))

        Trace.WriteLine(String.Format("Pushing purchase objects into facts at {0}.", DateTime.Now().ToLongTimeString()))
        Dim purchaseFacts As IList(Of PurchaseFact) = SetUpPurchaseFacts(purchases)
        Trace.WriteLine(String.Format("Finished Pushing purchase objects into facts at {0}.", DateTime.Now().ToLongTimeString()))

        purchases = Nothing

        Trace.WriteLine(String.Format("Saving purchase facts at {0}.", DateTime.Now().ToLongTimeString()))
        Dim purchaseFactsRepository As IRepository(Of PurchaseFact) = New Repository(Of PurchaseFact)(HibernateDataMartMapperFactory.SessionFactory)
        purchaseFactsRepository.Save(purchaseFacts)
        Trace.WriteLine(String.Format("Finished Saving purchase facts at {0}.", DateTime.Now().ToLongTimeString()))

        purchaseFacts = Nothing
        purchaseFactsRepository = Nothing

        ' run aggregation
        Trace.WriteLine(String.Format("Aggregating Purchases Per Day Facts at {0}.", DateTime.Now().ToLongTimeString()))
        AggregatePurchasesPerDay()
        Trace.WriteLine(String.Format("Finished Aggregating Purchases Per Day Facts at {0}.", DateTime.Now().ToLongTimeString()))

        'Trace.WriteLine(String.Format("Aggregating Purchases Per Person Per Month Facts at {0}.", DateTime.Now().ToLongTimeString()))
        'AggregatePurchasesPerPersonPerMonth()
        'Trace.WriteLine(String.Format("Finished Aggregating Purchases Per Person Per Month Facts at {0}.", DateTime.Now().ToLongTimeString()))

        Trace.WriteLine(String.Format(""))
    End Sub

    Private Sub RepublishDataMartStructure()
        ' delete what is in the data mart by running a script
        Dim batchFile As String = _projectDirectory & ConfigurationManager.AppSettings("DataMartPublishCommandFilePath")
        Shell(batchFile, AppWinStyle.Hide, True)
    End Sub

    Private Function SetUpPersonDimensions(ByVal peopleList As IList(Of Person)) As IList(Of PersonDimension)
        Dim list As IList(Of PersonDimension) = New List(Of PersonDimension)

        For Each item As Person In peopleList
            Dim personDimension As New PersonDimension(item.Id, item.SystemOfRecordId, item.FirstName, item.LastName, item.EffectiveDate.Id, item.EffectiveDate.CalendarDate)
            list.Add(personDimension)
        Next

        Return list
    End Function

    Private Function SetUpPurchaseFacts(ByVal purchaseList As IList(Of Purchase)) As IList(Of PurchaseFact)
        Dim list As IList(Of PurchaseFact) = New List(Of PurchaseFact)

        For Each item As Purchase In purchaseList
            Dim purchaseFact As New PurchaseFact(item.Id, item.Amount, item.NumberOfItems, item.PurchaseDate, item.PersonId)
            list.Add(purchaseFact)
        Next

        Return list
    End Function

    Private Sub AggregatePurchasesPerDay()
        Dim dataMartAgg As New DataMartAggregator()
        Dim purchasesPerDay As IList(Of PurchasesPerDayAggregateFact) = dataMartAgg.AggregatePurchasesPerDay()
        Dim repository As IRepository(Of PurchasesPerDayAggregateFact) = New Repository(Of PurchasesPerDayAggregateFact)(HibernateDataMartMapperFactory.SessionFactory)

        repository.Save(purchasesPerDay)
    End Sub

    Private Sub AggregatePurchasesPerPersonPerMonth()
        Dim dataMartAgg As New DataMartAggregator()

        Dim purchasesPerPersonPerMonthAggregateFacts As IList(Of PurchasesPerPersonPerMonthAggregateFact) = dataMartAgg.AggregatePurchasesPerPersonPerMonth()
        Dim repository As IRepository(Of PurchasesPerPersonPerMonthAggregateFact) = New Repository(Of PurchasesPerPersonPerMonthAggregateFact)(HibernateDataMartMapperFactory.SessionFactory)

        repository.Save(purchasesPerPersonPerMonthAggregateFacts)
    End Sub

End Class
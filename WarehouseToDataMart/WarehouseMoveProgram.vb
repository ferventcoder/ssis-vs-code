Imports System.Collections.Generic
Imports System
Imports System.Configuration
Imports WarehouseToDataMart.Common
Imports WarehouseToDataMart.Warehouse
Imports WarehouseToDataMart.DataMart
Imports Microsoft.VisualBasic
Imports WarehouseToDataMart.Repositories

Public Class WarehouseMoveProgram

    'TODO: Ask for TraceListeners on New
    'TODO: Change Console.WriteLine to Trace.WriteLine

    Private _projectDirectory As String = ConfigurationManager.AppSettings("ProjectDirectoryPath")

    Sub Run()
        Console.WriteLine("Starting the process at {0}", DateTime.Now().ToLongTimeString())


        Console.WriteLine("Running SSIS at {0}", DateTime.Now().ToLongTimeString())
        Dim startTicks As Long = DateTime.Now.Ticks()
        'MoveDataWithSSIS()
        Dim endTicks As Long = DateTime.Now.Ticks()

        Console.WriteLine("Running the SSIS Package to move data from the warehouse to the data mart took {0} ticks", (endTicks - startTicks))
        '_logger.Debug("Running the SSIS Package took a total of {0} ticks.", (endTicks - startTicks))
        Console.WriteLine("That is a total of {0} seconds.", TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds)
        Console.WriteLine("Time is {0}", DateTime.Now().ToLongTimeString())
        Console.WriteLine("")

        Console.WriteLine("Running Code at {0}", DateTime.Now().ToLongTimeString())
        startTicks = DateTime.Now.Ticks()
        MoveDataWithCode()
        endTicks = DateTime.Now.Ticks()

        Console.WriteLine("Running code to move objects from the warehouse to the data mart took {0} ticks.", (endTicks - startTicks))
        '_logger.Debug("Running the code took a total of {0} ticks.", (endTicks - startTicks))
        Console.WriteLine("That is a total of {0} seconds.", TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds)
        Console.WriteLine("Time is {0}", DateTime.Now().ToLongTimeString())
        Console.WriteLine("")
        Console.WriteLine("Please hit enter to continue...")
        Console.ReadLine()
    End Sub

    Private Sub MoveDataWithSSIS()
        Dim filePath As String = _projectDirectory & ConfigurationManager.AppSettings("SSISPackageFilePath")
        Dim fileName As String = ConfigurationManager.AppSettings("SSISPackageName")
        Dim configFileName As String = ConfigurationManager.AppSettings("SSISConfigName")
        Dim cmdToExecute As String = "dtexec.exe"
        Dim parameters As String = String.Format(" /FILE ""{0}"" /CONFIGFILE ""{1}""", filePath & fileName, filePath & configFileName)

        Shell(cmdToExecute & parameters, AppWinStyle.Hide, True)
    End Sub

    Private Sub MoveDataWithCode()
        Console.WriteLine("Starting Republish at {0}.", DateTime.Now().ToLongTimeString())
        RepublishDataMartStructure()
        Console.WriteLine("Finished Republish at {0}.", DateTime.Now().ToLongTimeString())

        ' set date range to move = 50 years
        ' NOT worried about the top for this example - gonna have both move the high amounts of data

        ' move date Info objects
        Console.WriteLine("Getting date objects at {0}.", DateTime.Now().ToLongTimeString())
        Dim dateInfo As IList(Of DateInformation) = New Repository(Of DateInformation)(HibernateWarehouseMapperFactory.SessionFactory).GetAll()
        Dim dateInfoDimRepository As IRepository(Of DateInformation) = New Repository(Of DateInformation)(HibernateDataMartMapperFactory.SessionFactory)
        Console.WriteLine("Saving date dimensions at {0}.", DateTime.Now().ToLongTimeString())
        dateInfoDimRepository.Save(dateInfo)

        dateInfo = Nothing

        ' move person objects
        Console.WriteLine("Getting person objects at {0}.", DateTime.Now().ToLongTimeString())
        Dim people As IList(Of Person) = New Repository(Of Person)(HibernateWarehouseMapperFactory.SessionFactory).GetAll()
        Console.WriteLine("Pushing person objects into dimensions at {0}.", DateTime.Now().ToLongTimeString())
        Dim peopleDimension As IList(Of PersonDimension) = SetUpPersonDimensions(people)

        people = Nothing

        Dim peopleDimRepository As IRepository(Of PersonDimension) = New Repository(Of PersonDimension)(HibernateDataMartMapperFactory.SessionFactory)
        Console.WriteLine("Saving person dimensions at {0}.", DateTime.Now().ToLongTimeString())
        peopleDimRepository.Save(peopleDimension)

        peopleDimension = Nothing

        ' move purchase objects
        Console.WriteLine("Getting purchase objects at {0}.", DateTime.Now().ToLongTimeString())
        Dim purchases As IList(Of Purchase) = New Repository(Of Purchase)(HibernateWarehouseMapperFactory.SessionFactory).GetAll()
        Console.WriteLine("Pushing purchase objects into facts at {0}.", DateTime.Now().ToLongTimeString())
        Dim purchaseFacts As IList(Of PurchaseFact) = SetUpPurchaseFacts(purchases)

        purchases = Nothing

        Dim purchaseFactsRepository As IRepository(Of PurchaseFact) = New Repository(Of PurchaseFact)(HibernateDataMartMapperFactory.SessionFactory)
        Console.WriteLine("Saving purchase facts at {0}.", DateTime.Now().ToLongTimeString())
        purchaseFactsRepository.Save(purchaseFacts)

        purchaseFacts = Nothing


        ' run aggregation
        Console.WriteLine("Aggregating Purchases Per Day Facts at {0}.", DateTime.Now().ToLongTimeString())
        AggregatePurchasesPerDay()
        'Console.WriteLine("Aggregating Purchases Per Person Per Month Facts at {0}.", DateTime.Now().ToLongTimeString())
        'AggregatePurchasesPerPersonPerMonth()
        Console.WriteLine("")
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